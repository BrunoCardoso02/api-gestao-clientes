using ClientAPI.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace ClientAPI.Application.Validators
{
    public static class ContatoValidator
    {
        private static readonly string[] TiposValidos = { "Residencial", "Comercial", "Celular" };

        public static List<string> Validate(List<ContatoDTO> contatos)
        {
            var errors = new List<string>();

            if (contatos == null) return errors;

            foreach (var contato in contatos)
            {
                if (string.IsNullOrWhiteSpace(contato.Tipo) || !TiposValidos.Contains(contato.Tipo))
                    errors.Add("Tipo de contato inválido. Use: Residencial, Comercial ou Celular.");

                if (contato.DDD < 11 || contato.DDD > 99)
                    errors.Add("O DDD deve ter 2 dígitos.");

                if (contato.Telefone < 10000000)
                    errors.Add("O telefone deve ter pelo menos 8 dígitos.");
            }

            return errors;
        }
    }
}
