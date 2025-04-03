
namespace ClientAPI.Domain.Entities
{
    public class Endereco
    {
        public int Id { get; private set; }
        public string Tipo { get; private set; }
        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public int Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Complemento { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Referencia { get; private set; }

        public Endereco(int id, string tipo, string cep, string logradouro, int numero, string bairro, string cidade, string estado)
        {
            if (string.IsNullOrWhiteSpace(tipo)) throw new ArgumentException("Tipo de endereço é obrigatório.");
            if (string.IsNullOrWhiteSpace(cep)) throw new ArgumentException("CEP é obrigatório.");
            if (string.IsNullOrWhiteSpace(logradouro)) throw new ArgumentException("Logradouro é obrigatório.");
            if (numero <= 0) throw new ArgumentException("Número do endereço inválido.");
            if (string.IsNullOrWhiteSpace(bairro)) throw new ArgumentException("Bairro é obrigatório.");
            if (string.IsNullOrWhiteSpace(cidade)) throw new ArgumentException("Cidade é obrigatória.");
            if (string.IsNullOrWhiteSpace(estado)) throw new ArgumentException("Estado é obrigatório.");

            Id = id;
            Tipo = tipo;
            CEP = cep;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Complemento = string.Empty;
            Referencia = string.Empty;
        }
    }

}
