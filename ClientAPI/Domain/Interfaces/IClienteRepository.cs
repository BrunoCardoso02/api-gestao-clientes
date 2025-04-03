using System.Collections.Generic;
using ClientAPI.Domain.Entities;

namespace ClientAPI.Domain.Interfaces
{
    public interface IClienteRepository
    {
        void AddClient(Cliente cliente);
        Cliente? GetClientById(int id);
        IEnumerable<Cliente> GetAllClients();
        void UpdateClient(Cliente cliente);
        void DeleteClient(int id);
    }
}
