using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml;
using Domain.Entities;
using Infra.Interfaces;
using Newtonsoft.Json;

namespace Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private const string FilePath = "clientes.json";

        private List<Cliente> CarregarClientes()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Cliente>();
            }

            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<Cliente>>(json) ?? new List<Cliente>();
        }

        private void SalvarClientes(List<Cliente> clientes)
        {
            var json = JsonConvert.SerializeObject(clientes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public IEnumerable<Cliente> ListarClientes(string nome = null, string email = null, string cpf = null)
        {
            var clientes = CarregarClientes();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                clientes = clientes.Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                clientes = clientes.Where(c => c.Email.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(cpf))
            {
                clientes = clientes.Where(c => c.CPF.Contains(cpf)).ToList();
            }

            return clientes;
        }

        public Cliente AdicionarCliente(Cliente cliente)
        {
            var clientes = CarregarClientes();
            cliente.Id = clientes.Count + 1;

            int contatoId = 1;
            foreach (var contato in cliente.Contatos)
            {
                contato.Id = contatoId++;
            }

            int enderecoId = 1;
            foreach (var endereco in cliente.Enderecos)
            {
                endereco.Id = enderecoId++;
            }

            clientes.Add(cliente);
            SalvarClientes(clientes);
            return cliente;
        }

        public Cliente AtualizarCliente(int id, Cliente cliente)
        {
            var clientes = CarregarClientes();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == id);
            if (clienteExistente != null)
            {
                clienteExistente.Nome = cliente.Nome;
                clienteExistente.Email = cliente.Email;
                clienteExistente.CPF = cliente.CPF;
                clienteExistente.Contatos = cliente.Contatos;
                clienteExistente.Enderecos = cliente.Enderecos;
                SalvarClientes(clientes);
            }
            return clienteExistente;
        }

        public void RemoverCliente(int id)
        {
            var clientes = CarregarClientes();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == id);
            if (clienteExistente != null)
            {
                clientes.Remove(clienteExistente);
                SalvarClientes(clientes);
            }
        }
    }

}
