using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using Domain.Entities;
using Infra.Repositories;
using Validators;

namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IEnumerable<Cliente> ListarClientes(string nome = null, string email = null, string cpf = null)
        {
            var clientes = _clienteRepository.ListarClientes();

            if (!string.IsNullOrWhiteSpace(nome))
                clientes = clientes.Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(email))
                clientes = clientes.Where(c => c.Email.Contains(email, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(cpf))
                clientes = clientes.Where(c => c.CPF.Contains(cpf));

            return clientes.ToList();
        }

        public Cliente AdicionarCliente(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente), "Cliente não pode ser nulo.");

            foreach (var endereco in cliente.Enderecos)
            {
                if (!EnderecoValidator.ValidarCEP(endereco.CEP))
                    throw new ArgumentException($"CEP inválido: {endereco.CEP}");
            }

            if (!ClienteValidator.ValidarCliente(cliente))
                throw new ArgumentException("Dados do cliente inválidos.");

            return _clienteRepository.AdicionarCliente(cliente);
        }

        public Cliente AtualizarCliente(int id, Cliente cliente)
        {
            if (!ClienteValidator.ValidarCliente(cliente))
                throw new ArgumentException("Dados do cliente inválidos.");

            // Atualizar o cliente
            var clienteAtualizado = _clienteRepository.AtualizarCliente(id, cliente);

            // Retornar o cliente atualizado
            return clienteAtualizado;
        }


        public void RemoverCliente(int id)
        {
            var clienteExistente = _clienteRepository.ListarClientes().FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
            {
                throw new ArgumentException($"Cliente com ID {id} não encontrado.");
            }

            _clienteRepository.RemoverCliente(id);
        }

    }
}
