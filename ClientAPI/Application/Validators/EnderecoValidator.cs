using ClientAPI.DTOs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ClientAPI.Application.Validators
{
    public static class EnderecoValidator
    {
        public static List<string> Validate(List<EnderecoDTO> enderecos)
        {
            var errors = new List<string>();

            if (enderecos == null) return errors;

            foreach (var endereco in enderecos)
            {
                if (string.IsNullOrWhiteSpace(endereco.CEP) || !IsValidCEP(endereco.CEP))
                    errors.Add("CEP inválido. Formato correto: 12345-678");

                if (string.IsNullOrWhiteSpace(endereco.Logradouro))
                    errors.Add("O logradouro é obrigatório.");

                if (endereco.Numero <= 0)
                    errors.Add("O número deve ser maior que zero.");
            }

            return errors;
        }

        private static bool IsValidCEP(string cep) =>
            Regex.IsMatch(cep, @"^\d{5}-\d{3}$");
    }
}
