using ClientAPI.Application.Interfaces;
using ClientAPI.DTOs;
using ClientAPI.Domain.Entities;
using ClientAPI.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            var clients = _clienteRepository.GetAllClients();
            int newId = clients.Any() ? clients.Max(c => c.Id) + 1 : 1;

            var cliente = new Cliente(newId, clienteDTO.Nome, clienteDTO.Email, clienteDTO.CPF, clienteDTO.RG)
            {
                Enderecos = clienteDTO.Enderecos?.Select(e => new Endereco(0, e.Tipo, e.CEP, e.Logradouro, e.Numero, e.Bairro, e.Cidade, e.Estado)).ToList() ?? new List<Endereco>(),
                Contatos = clienteDTO.Contatos?.Select(c => new Contato(0, c.Tipo, c.DDD, c.Telefone)).ToList() ?? new List<Contato>()
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

            client.AtualizarDados(clienteDTO.Nome, clienteDTO.Email);

            _clienteRepository.UpdateClient(client);

            return new ClienteDTO
            {
                Nome = client.Nome,
                Email = client.Email,
                CPF = client.CPF
            };
        }

        public void DeleteClient(int id)
        {
            _clienteRepository.DeleteClient(id);
        }
    }
}
