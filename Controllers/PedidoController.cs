using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_gestion_pedidos.Data;
using Sistema_gestion_pedidos.Dto;
using Sistema_gestion_pedidos.Models;
using System.Xml;

namespace Sistema_gestion_pedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PedidoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetPedido()
        {
            var pedido = _context.Pedido
                //Incluye la información del cliente asociado al pedido, tambien 
                //Incluye los detalles del pedido y la información del producto asociado a cada detalle del pedido, luego lo lista
                .Include(p => p.Cliente)
                .Include(P => P.DetallesPedido)
                .ThenInclude(pedido => pedido.Producto)
                .ToList();
            return Ok(pedido);
        }
        [HttpGet("{id}")]
        public IActionResult GetPedidoById(int id)
        {
            var pedido = _context.Pedido
                //Incluye la información del cliente asociado al pedido, tambien
                //Incluye los detalles del pedido y la información del producto asociado a cada detalle del pedido, luego lo busca por id
                .Include(p => p.Cliente)
                .Include(p=> p.DetallesPedido)
                .ThenInclude(p=>p.Producto)
                .FirstOrDefault(p => p.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
        [HttpPost]
        public IActionResult CreatePedido(PedidoDto dto)
        {
            //primero verifico si el cliente existe
            var cliente = _context.Cliente.Find(dto.IdCliente);
            if(cliente == null)
            {
                return BadRequest($"El cliente con identificador Numero {dto.IdCliente} no existe");
            }
            var pedido = new Pedido
            {
                IdCliente = dto.IdCliente,
                FechaPedido = DateTime.Now,
                DetallesPedido = new List<DetallePedido>()
            };
            //Ahora agregamos cada detalle del pedido con un foreach, para recorrer cada detalle del pedido que viene en el dto
            foreach (var i in dto.Detalle)
            {
              var producto = _context.Producto.Find(i.ProductoId);
                if (producto == null)
                {
                    return BadRequest($"El producto con identificador Numero {i.ProductoId} no existe");
                }
                var detallePedido = new DetallePedido
                {
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad,
                };
                pedido.DetallesPedido.Add(detallePedido);
            }
            _context.Pedido.Add(pedido);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.IdPedido }, pedido);
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePedido(int id, PedidoDto dto)
        {
            var existePedido = _context.Pedido
                .Include(p => p.DetallesPedido)
                .FirstOrDefault(p => p.IdPedido == id);

            if (existePedido == null) return NotFound();

            existePedido.IdCliente = dto.IdCliente;
            existePedido.DetallesPedido.Clear();

            foreach (var item in dto.Detalle)
            {
                existePedido.DetallesPedido.Add(new DetallePedido
                {
                    ProductoId = item.ProductoId,
                    Cantidad = item.Cantidad
                });
            }

            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePedido(int id)
        {
            var existePedido = _context.Pedido
                .Include(p => p.DetallesPedido)
                .FirstOrDefault(p => p.IdPedido == id);
            if (existePedido == null)
            {
                return NotFound();
            }
            _context.Pedido.Remove(existePedido);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
