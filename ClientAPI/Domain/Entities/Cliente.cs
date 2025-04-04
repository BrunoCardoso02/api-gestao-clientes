using System.Collections.Generic;

namespace Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public List<Contato> Contatos { get; set; }
        public List<Endereco> Enderecos { get; set; }

        public Cliente()
        {
            Contatos = new List<Contato>();
            Enderecos = new List<Endereco>();
        }
    }
}
