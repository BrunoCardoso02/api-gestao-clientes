using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("listar")]
        public ActionResult<IEnumerable<Cliente>> ListarClientes(
            [FromQuery] string nome = null,
            [FromQuery] string email = null,
            [FromQuery] string cpf = null)
        {
            var clientes = _clienteService.ListarClientes(nome, email, cpf);

            if (clientes == null || !clientes.Any())
                return NotFound("Nenhum cliente encontrado.");

            return Ok(clientes);
        }

        [HttpPost("criar")]
        public ActionResult<Cliente> AdicionarCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Dados do cliente inválidos.");

            var clienteAdicionado = _clienteService.AdicionarCliente(cliente);
            return CreatedAtAction(nameof(ListarClientes), new { id = clienteAdicionado.Id }, clienteAdicionado);
        }

        [HttpPut("atualizar/{id}")]
        public ActionResult<Cliente> AtualizarCliente(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Dados do cliente inválidos.");

            var clienteAtualizado = _clienteService.AtualizarCliente(id, cliente);
            if (clienteAtualizado == null)
                return NotFound($"Cliente com ID {id} não encontrado.");

            return Ok(clienteAtualizado);
        }

        [HttpDelete("remover/{id}")]
        public IActionResult RemoverCliente(int id)
        {
            try
            {
                _clienteService.RemoverCliente(id);
                return Ok("Dados excluídos com sucesso");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
