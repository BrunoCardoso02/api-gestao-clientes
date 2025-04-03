using System.Text.RegularExpressions;

namespace ClientAPI.Application.Validators
{
    public static class ClienteValidator
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidCPF(string cpf)
        {
            return Regex.IsMatch(cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        }

        public static bool IsValidRG(string rg)
        {
            return Regex.IsMatch(rg, @"^\d{2}\.\d{3}\.\d{3}-\d{1}$");
        }

        public static bool IsValidCEP(string cep)
        {
            return Regex.IsMatch(cep, @"^\d{5}-\d{3}$");
        }
    }
}
