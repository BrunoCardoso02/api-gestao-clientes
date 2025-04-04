using Domain.Entities;
using System;

namespace Validators
{
    public class ContatoValidator
    {
        public static bool ValidarTipo(string tipo)
        {
            var tiposValidos = new[] { "Residencial", "Comercial", "Celular" };
            return Array.Exists(tiposValidos, t => t.Equals(tipo, StringComparison.OrdinalIgnoreCase));
        }

        public static bool ValidarDDD(int ddd)
        {
            return ddd >= 11 && ddd <= 99;
        }

        public static bool ValidarTelefone(decimal telefone)
        {
            return telefone.ToString().Length >= 8 && telefone.ToString().Length <= 9;
        }

        public static bool ValidarContato(Contato contato)
        {
            return ValidarTipo(contato.Tipo) &&
                   ValidarDDD(contato.DDD) &&
                   ValidarTelefone(contato.Telefone);
        }
    }
}
