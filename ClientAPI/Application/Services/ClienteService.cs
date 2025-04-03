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
                Nome = c.Nome,
                Email = c.Email,
                CPF = c.CPF
            });
        }

        public ClienteDTO CreateClient(ClienteDTO clienteDTO)
        {
            var clients = _clienteRepository.GetAllClients();

            int newId = clients.Any() ? clients.Max(c => c.Id) + 1 : 1;

            var cliente = new Cliente(newId, clienteDTO.Nome, clienteDTO.Email, clienteDTO.CPF, clienteDTO.RG);

            _clienteRepository.AddClient(cliente);

            return new ClienteDTO
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                CPF = cliente.CPF
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
