using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_gestion_pedidos.Data;
using Sistema_gestion_pedidos.Dto;
using Sistema_gestion_pedidos.Models;
using Sistema_gestion_pedidos.Service;
using System.Security;
using System.Xml;

namespace Sistema_gestion_pedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoervice;
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoervice = pedidoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPedidos()
        {
            var pedidos = await _pedidoervice.GetPedidos();
            return Ok(pedidos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            var pedido = await _pedidoervice.GetPedidoById(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePedido(PedidoDto dto)
        {
            //uso try catch para manejar las excepciones que puedan ocurrir durante la creación del pedido,
            //por ejemplo si el cliente o el producto no existen. Si ocurre una excepción, se captura y se puede manejar de manera adecuada
            //como devolver un mensaje de error al cliente o registrar la excepción para su posterior análisis.
            try
            {
                var nuevoPedido = await _pedidoervice.CreatePedido(dto);
                return CreatedAtAction(nameof(GetPedidoById), new { id = nuevoPedido.IdPedido }, nuevoPedido);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, PedidoDto dto)
        {
            var resultado = await _pedidoervice.UpdatePedido(id, dto);
            if (!resultado) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var resultado = await _pedidoervice.DeletePedido(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}
