using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_gestion_pedidos.Models
{
    public class Pedido
    {
        //Propiedades
        [Key]
        public int IdPedido { get; set; }
        public DateTime FechaPedido { get; set; }

        //Relaciones
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public List<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();
        public decimal TotalPedido { get; set; }
    }
}
