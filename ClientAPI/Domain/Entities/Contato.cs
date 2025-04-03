
namespace ClientAPI.Domain.Entities
{
    public class Contato
    {
        public int Id { get; private set; }
        public string Tipo { get; private set; }
        public int DDD { get; private set; }
        public decimal Telefone { get; private set; }

        public Contato(int id, string tipo, int ddd, decimal telefone)
        {
            if (string.IsNullOrWhiteSpace(tipo)) throw new ArgumentException("Tipo de contato é obrigatório.");
            if (ddd < 11 || ddd > 99) throw new ArgumentException("DDD inválido.");
            if (telefone <= 0) throw new ArgumentException("Número de telefone inválido.");

            Id = id;
            Tipo = tipo;
            DDD = ddd;
            Telefone = telefone;
        }
    }

}
