using Microsoft.AspNetCore.Mvc;
using Sistema_gestion_pedidos.Data;
using Sistema_gestion_pedidos.Dto;
using Sistema_gestion_pedidos.Models;


namespace Sistema_gestion_pedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductoController(AppDbContext context)
        {
            _context = context;
        }
        //Traer todos los productos
        [HttpGet]
        public IActionResult GetProducto()
        {
            var producto = _context.Producto.ToList();
            return Ok(producto);
        }
        //Traer productos por id
        [HttpGet("{id}")]
        public IActionResult GetProductoById(int id)
        {
            var producto = _context.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }
        //crear producto
        [HttpPost]
        public IActionResult CreateProducto(ProductoDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Stock = dto.Stock,
                Precio = dto.Precio,
            };
            _context.Producto.Add(producto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, producto);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProducto(int id, ProductoDto dto)
        {            
            var existeProducto = _context.Producto.Find(id);
            if (existeProducto == null)
            {
                return NotFound();
            }
            existeProducto.Nombre = dto.Nombre;
            existeProducto.Stock = dto.Stock;
            existeProducto.Precio = dto.Precio;
            existeProducto.Descripcion = dto.Descripcion;
            _context.SaveChanges();
            return NoContent();
        }
        //Eliminar producto
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id) 
        {
            var producto = _context.Producto.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Producto.Remove(producto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
