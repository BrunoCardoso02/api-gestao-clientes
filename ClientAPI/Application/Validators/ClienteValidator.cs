using ClientAPI.DTOs;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ClientAPI.Application.Validators
{
    public static class ClienteValidator
    {
        public static List<string> Validate(ClienteDTO clienteDTO)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(clienteDTO.Nome))
                errors.Add("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(clienteDTO.Email) || !IsValidEmail(clienteDTO.Email))
                errors.Add("E-mail inválido.");

            if (string.IsNullOrWhiteSpace(clienteDTO.CPF) || !IsValidCPF(clienteDTO.CPF))
                errors.Add("CPF inválido. Formato correto: 000.000.000-00");

            if (string.IsNullOrWhiteSpace(clienteDTO.RG) || !IsValidRG(clienteDTO.RG))
                errors.Add("RG inválido. Formato correto: 12.345.678-9");

            return errors;
        }

        private static bool IsValidEmail(string email) =>
            Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private static bool IsValidCPF(string cpf) =>
            Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$");

        private static bool IsValidRG(string rg) =>
            Regex.IsMatch(rg, @"^\d{2}\.\d{3}\.\d{3}-\d{1}$");
    }
}
