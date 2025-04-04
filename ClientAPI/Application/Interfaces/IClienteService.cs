using System.Collections.Generic;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<Cliente> ListarClientes(string nome = null, string email = null, string cpf = null);
        Cliente AdicionarCliente(Cliente cliente);
        Cliente AtualizarCliente(int id, Cliente cliente);
        void RemoverCliente(int id);
    }
}
