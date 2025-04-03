using ClientAPI.DTOs;
using System.Collections.Generic;

namespace ClientAPI.Application.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<ClienteDTO> GetClients(string? nome, string? email, string? cpf);
        ClienteDTO GetClientById(int id);
        ClienteDTO CreateClient(ClienteDTO clienteDTO);
        ClienteDTO UpdateClient(int id, ClienteDTO clienteDTO);
        void DeleteClient(int id);
    }
}
