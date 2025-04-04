using System;
using System.Text.RegularExpressions;
using Domain.Entities;

namespace Validators
{
    public class ClienteValidator
    {
        public static bool ValidarNome(string nome)
        {
            return !string.IsNullOrWhiteSpace(nome);
        }

        public static bool ValidarEmail(string email)
        {
            // Validação simples de formato de e-mail
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com$");
            return regex.IsMatch(email);
        }

        public static bool ValidarCPF(string cpf)
        {
            // Verificação de formato de CPF (somente números)
            var regex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
            return regex.IsMatch(cpf);
        }

        public static bool ValidarRG(string rg)
        {
            // Verificação de formato de RG (somente números)
            var regex = new Regex(@"^\d{2}\.\d{3}\.\d{3}-\d{1}$");
            return regex.IsMatch(rg);
        }

        public static bool ValidarCliente(Cliente cliente)
        {
            return ValidarNome(cliente.Nome) &&
                   ValidarEmail(cliente.Email) &&
                   ValidarCPF(cliente.CPF) &&
                   ValidarRG(cliente.RG);
        }
    }
}
