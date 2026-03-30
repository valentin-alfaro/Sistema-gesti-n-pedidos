using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_gestion_pedidos.Models
{
    public class DetallePedido
    {
        //Propiedade
        [Key]
        public int IdDetallePedido { get; set; }
        public int Cantidad { get; set; } 
        //Relaciones
        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;
    }
}
