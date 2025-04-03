using ClientAPI.Domain.Entities;
using ClientAPI.Infra.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ClientAPI.Infra.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infra", "Data", "clients.json");

    public void AddClient(Cliente cliente)
    {
        var clients = GetAllClients().ToList();
        clients.Add(cliente);
        SaveToFile(clients);
    }

    public Cliente? GetClientById(int id)
    {
        return GetAllClients().FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Cliente> GetAllClients()
    {
        if (!File.Exists(_filePath)) return new List<Cliente>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
    }

    public void UpdateClient(Cliente cliente)
    {
        var clients = GetAllClients().ToList();
        var index = clients.FindIndex(c => c.Id == cliente.Id);

        if (index >= 0)
        {
            clients[index] = cliente;
            SaveToFile(clients);
        }
    }

    public void DeleteClient(int id)
    {
        var clients = GetAllClients().ToList();
        clients.RemoveAll(c => c.Id == id);
        SaveToFile(clients);
    }

    private void SaveToFile(List<Cliente> clients)
    {
        var json = JsonSerializer.Serialize(clients, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
