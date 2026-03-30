using Microsoft.EntityFrameworkCore;
using Sistema_gestion_pedidos.Models;

namespace Sistema_gestion_pedidos.Data
{
    public class AppDbContext : DbContext
    {
        //Constructor que recibe las opciones de configuración para la base de datos
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
    }
}
