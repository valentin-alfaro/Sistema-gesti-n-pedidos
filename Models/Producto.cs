using System.ComponentModel.DataAnnotations;

namespace Sistema_gestion_pedidos.Models
{
    public class Producto
    {
        //Propiedades
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;//Para evitar el error de valor nulo
        public Decimal Stock { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; } = null!;//Para evitar el error de valor nulo

        //Relaciones
        public List<DetallePedido> DetallePedido { get; set; } = new List<DetallePedido>();//Para evitar el error de valor nulo, inicializo la lista vacia
    }
}
