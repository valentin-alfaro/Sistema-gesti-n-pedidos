using Microsoft.AspNetCore.Mvc;
using Sistema_gestion_pedidos.Data;
using Sistema_gestion_pedidos.Dto;
using Sistema_gestion_pedidos.Models;

namespace Sistema_gestion_pedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ClientesController(AppDbContext context)
        {
            _context = context;
        }
        //Traer los clientes
        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _context.Cliente.ToList();
            return Ok(clientes);
        }
        //Traer por id
        [HttpGet("{id}")]
        public IActionResult GetClientesById(int id)
        {
            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
        //Crear cliente
        [HttpPost]
        public IActionResult CreateCliente(ClienteDto dto)
        {
            var cliente = new Cliente 
            {
                DNI = dto.DNI,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email
            };
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClientes), new { id = cliente.IdCliente }, cliente);//Retorna el cliente creado con su id
        }
        //Actualizar Cliente
        [HttpPut("{id}")]
        public IActionResult UpdateClienteById(int id, ClienteDto dto)
        {            
            var existingCliente = _context.Cliente.Find(id);
            if (existingCliente == null)
            {
                return NotFound();
            }
            existingCliente.DNI = dto.DNI;
            existingCliente.Nombre = dto.Nombre;
            existingCliente.Apellido = dto.Apellido;
            existingCliente.Email = dto.Email;
            _context.SaveChanges();
            return NoContent();
        }
        //Eliminar cliente
        [HttpDelete("{id}")]
        public IActionResult DeleteClienteById(int id)
        {
            var cliente = _context.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _context.Cliente.Remove(cliente);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
