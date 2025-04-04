using Domain.Entities;
using System.Text.RegularExpressions;

namespace Validators
{
    public class EnderecoValidator
    {
        public static bool ValidarCEP(string cep)
        {
            var regex = new Regex(@"^\d{5}-\d{3}$");
            return regex.IsMatch(cep);
        }

        public static bool ValidarEndereco(Endereco endereco)
        {
            return ValidarCEP(endereco.CEP) &&
                   !string.IsNullOrWhiteSpace(endereco.Logradouro) &&
                   endereco.Numero > 0 &&
                   !string.IsNullOrWhiteSpace(endereco.Bairro) &&
                   !string.IsNullOrWhiteSpace(endereco.Cidade) &&
                   !string.IsNullOrWhiteSpace(endereco.Estado);
        }
    }
}
