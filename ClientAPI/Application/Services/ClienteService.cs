using ClientAPI.Application.Interfaces;
using ClientAPI.DTOs;
using ClientAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using ClientAPI.Application.Validators;
using ClientAPI.Infra.Interfaces;

namespace ClientAPI.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<ClienteDTO> GetClients(string? nome, string? email, string? cpf)
        {
            var clients = _clienteRepository.GetAllClients();

            if (!string.IsNullOrEmpty(nome))
                clients = clients.Where(c => c.Nome.Contains(nome)).ToList();

            if (!string.IsNullOrEmpty(email))
                clients = clients.Where(c => c.Email == email).ToList();

            if (!string.IsNullOrEmpty(cpf))
                clients = clients.Where(c => c.CPF == cpf).ToList();

            return clients.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
                CPF = c.CPF,
                RG = c.RG,
                Contatos = c.Contatos?.Select(ct => new ContatoDTO
                {
                    Id = ct.Id,
                    Tipo = ct.Tipo,
                    DDD = ct.DDD,
                    Telefone = ct.Telefone
                }).ToList(),
                Enderecos = c.Enderecos?.Select(e => new EnderecoDTO
                {
                    Id = e.Id,
                    Tipo = e.Tipo,
                    CEP = e.CEP,
                    Logradouro = e.Logradouro,
                    Numero = e.Numero,
                    Bairro = e.Bairro,
                    Complemento = e.Complemento,
                    Cidade = e.Cidade,
                    Estado = e.Estado,
                    Referencia = e.Referencia
                }).ToList()
            });
        }

        public ClienteDTO CreateClient(ClienteDTO clienteDTO)
        {
         var existingClient = _clienteRepository.GetAllClients()
            .FirstOrDefault(c => c.Email == clienteDTO.Email || c.RG == clienteDTO.RG || c.CPF == clienteDTO.CPF);
            if (existingClient != null)
                return null;

            if (!ValidarCliente(clienteDTO))
                return null;

            var enderecoErrors = EnderecoValidator.Validate(clienteDTO.Enderecos);
            if (enderecoErrors.Any())
            {
                return null;
            }

            var clients = _clienteRepository.GetAllClients();

            int newId = clients.Any() ? clients.Max(c => c.Id) + 1 : 1;

            int maxContatoId = clients.SelectMany(c => c.Contatos).DefaultIfEmpty().Max(c => c?.Id ?? 0);
            int maxEnderecoId = clients.SelectMany(c => c.Enderecos).DefaultIfEmpty().Max(e => e?.Id ?? 0);

            var enderecos = clienteDTO.Enderecos?.Select((e, index) => new Endereco(
                maxEnderecoId + index + 1,
                e.Tipo, e.CEP, e.Logradouro, e.Numero, e.Bairro, e.Cidade, e.Estado
            )).ToList() ?? new List<Endereco>();

            var contatos = clienteDTO.Contatos?.Select((c, index) => new Contato(
                maxContatoId + index + 1,
                c.Tipo, c.DDD, c.Telefone
            )).ToList() ?? new List<Contato>();

            var cliente = new Cliente(newId, clienteDTO.Nome, clienteDTO.Email, clienteDTO.CPF, clienteDTO.RG)
            {
                Enderecos = enderecos,
                Contatos = contatos
            };

            _clienteRepository.AddClient(cliente);

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                CPF = cliente.CPF,
                RG = cliente.RG,
                Contatos = cliente.Contatos.Select(c => new ContatoDTO { Id = c.Id, Tipo = c.Tipo, DDD = c.DDD, Telefone = c.Telefone }).ToList(),
                Enderecos = cliente.Enderecos.Select(e => new EnderecoDTO { Id = e.Id, Tipo = e.Tipo, CEP = e.CEP, Logradouro = e.Logradouro, Numero = e.Numero, Bairro = e.Bairro, Complemento = e.Complemento, Cidade = e.Cidade, Estado = e.Estado, Referencia = e.Referencia }).ToList()
            };
        }

        private bool ValidarCliente(ClienteDTO clienteDTO)
        {
            return ClienteValidator.IsValidEmail(clienteDTO.Email) &&
                   ClienteValidator.IsValidCPF(clienteDTO.CPF) &&
                   ClienteValidator.IsValidRG(clienteDTO.RG);
        }

        public ClienteDTO? GetClientById(int id)
        {
            var client = _clienteRepository.GetClientById(id);
            if (client == null) return null;

            return new ClienteDTO
            {
                Nome = client.Nome,
                Email = client.Email,
                CPF = client.CPF
            };
        }

        public ClienteDTO? UpdateClient(int id, ClienteDTO clienteDTO)
        {
            var client = _clienteRepository.GetClientById(id);
            if (client == null) return null;

            if (!ValidarCliente(clienteDTO))
                return null;

            var enderecoErrors = EnderecoValidator.Validate(clienteDTO.Enderecos);
            if (enderecoErrors.Any())
            {
                return null; 
            }

            client.AtualizarDados(clienteDTO.Nome, clienteDTO.Email);

            _clienteRepository.UpdateClient(client);

            return new ClienteDTO
            {
                Id = client.Id,
                Nome = client.Nome,
                Email = client.Email,
                CPF = client.CPF,
                RG = client.RG,
                Contatos = client.Contatos.Select(c => new ContatoDTO { Id = c.Id, Tipo = c.Tipo, DDD = c.DDD, Telefone = c.Telefone }).ToList(),
                Enderecos = client.Enderecos.Select(e => new EnderecoDTO { Id = e.Id, Tipo = e.Tipo, CEP = e.CEP, Logradouro = e.Logradouro, Numero = e.Numero, Bairro = e.Bairro, Complemento = e.Complemento, Cidade = e.Cidade, Estado = e.Estado, Referencia = e.Referencia }).ToList()
            };
        }

        public void DeleteClient(int id)
        {
            _clienteRepository.DeleteClient(id);
        }
    }
}
