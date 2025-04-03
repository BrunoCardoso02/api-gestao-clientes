using Microsoft.AspNetCore.Mvc;
using ClientAPI.Application.Interfaces;
using ClientAPI.DTOs;
using System.Collections.Generic;

namespace ClientAPI.API.Controllers
{
    [Route("cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("listar")]
        public ActionResult<IEnumerable<ClienteDTO>> GetClients([FromQuery] string? nome, [FromQuery] string? email, [FromQuery] string? cpf)
        {
            var clients = _clienteService.GetClients(nome, email, cpf);

            if (!clients.Any())
                return NoContent();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> GetClientById(int id)
        {
            var client = _clienteService.GetClientById(id);
            if (client == null)
                return NotFound("Cliente não encontrado.");

            return Ok(client);
        }

        [HttpPost("criar")]
        public ActionResult<ClienteDTO> CreateClient([FromBody] ClienteDTO clienteDTO)
        {
            var client = _clienteService.CreateClient(clienteDTO);
            return CreatedAtAction(nameof(GetClientById), new { id = client.CPF }, client);
        }

        [HttpPut("atualizar/{id}")]
        public ActionResult<ClienteDTO> UpdateClient(int id, [FromBody] ClienteDTO clienteDTO)
        {
            var updatedClient = _clienteService.UpdateClient(id, clienteDTO);
            if (updatedClient == null)
                return NotFound("Cliente não encontrado.");

            return Ok(updatedClient);
        }

        [HttpDelete("deletar/{id}")]
        public ActionResult DeleteClient(int id)
        {
            var existingClient = _clienteService.GetClientById(id);
            if (existingClient == null)
                return NotFound("Cliente não encontrado.");

            _clienteService.DeleteClient(id);
            return Ok("Cliente removido com sucesso.");
        }
    }
}
