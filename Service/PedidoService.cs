using Microsoft.EntityFrameworkCore;
using Sistema_gestion_pedidos.Data;
using Sistema_gestion_pedidos.Dto;
using Sistema_gestion_pedidos.Models;

namespace Sistema_gestion_pedidos.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _context;
        public PedidoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Pedido>> GetPedidos()
        {
            return await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.DetallesPedido)
                .ThenInclude(p => p.Producto)
                .ToListAsync();
        }
        public async Task<Pedido> GetPedidoById(int id)
        {
            return await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.DetallesPedido)
                .ThenInclude(p => p.Producto)
                .FirstOrDefaultAsync(p => p.IdPedido == id);
        }
        public async Task<Pedido> CreatePedido(PedidoDto dto)
        {
            var cliente = _context.Cliente.Find(dto.IdCliente);
            if (cliente == null)
            {
                //KeyNotFoundException es una excepción que se lanza cuando no se encuentra una clave en una colección
                //Al lanzar esta excepción, se indica que el cliente no existe y se proporciona un mensaje de error específico.
                throw new KeyNotFoundException($"El cliente con identificador Numero {dto.IdCliente} no existe");
            }
            var pedido = new Pedido
            {
                IdCliente = dto.IdCliente,
                FechaPedido = DateTime.Now,
                DetallesPedido = new List<DetallePedido>(),
                TotalPedido = 0
            };
            //Ahora agregamos cada detalle del pedido con un foreach, para recorrer cada detalle del pedido que viene en el dto
            foreach (var i in dto.Detalle)
            {
                var producto = _context.Producto.Find(i.ProductoId);
                if (producto == null)
                {
                    throw new KeyNotFoundException($"El producto con identificador Numero {i.ProductoId} no existe");
                }
                if (producto.Stock < i.Cantidad)
                {
                    throw new KeyNotFoundException($"Stock insuficiente para el producto: {producto.Nombre}, Stock actual: {producto.Stock}");
                }
                //el -= se utiliza para restar la cantidad solicitada del stock actual del producto.
                //Esto asegura que el stock se actualice correctamente a medida que se realizan pedidos.
                producto.Stock -= i.Cantidad;
                var detallePedido = new DetallePedido
                {
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = producto.Precio
                };
                //uso += para acumular el total del pedido, multiplicando el precio del producto por la cantidad solicitada
                //Esto asegura que el total del pedido se actualice correctamente a medida que se agregan los detalles.
                pedido.TotalPedido += producto.Precio * i.Cantidad;
                pedido.DetallesPedido.Add(detallePedido);
            }
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;

        }

        public async Task<bool> DeletePedido(int id)
        {
            var pedido = _context.Pedido
                .Include(p => p.DetallesPedido)
                .FirstOrDefault(p => p.IdPedido == id);
            if (pedido == null) return false;
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePedido(int id, PedidoDto dto)
        {
            var pedido = await _context.Pedido
                .Include(p => p.DetallesPedido)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null) return false;

            pedido.IdCliente = dto.IdCliente;
            pedido.DetallesPedido.Clear();

            foreach (var i in dto.Detalle)
            {
                pedido.DetallesPedido.Add(new DetallePedido
                {
                    ProductoId = i.ProductoId,
                    Cantidad = i.Cantidad
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
