using System.Collections.Generic;
using Domain.Entities;

namespace Infra.Interfaces
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> ListarClientes(string nome = null, string email = null, string cpf = null);

        Cliente AdicionarCliente(Cliente cliente);
        Cliente AtualizarCliente(int id, Cliente cliente);
        void RemoverCliente(int id);
    }
}
