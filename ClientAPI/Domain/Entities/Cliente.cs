using System;
using System.Collections.Generic;

namespace ClientAPI.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public List<Contato> Contatos { get; private set; }
        public List<Endereco> Enderecos { get; private set; }

        public Cliente(int id, string nome, string email, string cpf, string rg)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email inválido");
            if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("CPF inválido");

            Id = id;
            Nome = nome;
            Email = email;
            CPF = cpf;
            RG = rg;
            Contatos = new List<Contato>();
            Enderecos = new List<Endereco>();
        }

        public void AtualizarDados(string nome, string email)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email inválido");

            Nome = nome;
            Email = email;
        }
    }
}
