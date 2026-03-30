using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sistema_gestion_pedidos.Models
{
    public class Producto
    {
        //Propiedades
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;//Para evitar el error de valor nulo
        public Decimal Stock { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; } = string.Empty;//Para evitar el error de valor nulo

        //Relaciones
        [JsonIgnore]
        public List<DetallePedido> DetallesPedido { get; set; } = new List<DetallePedido>();//Para evitar el error de valor nulo, inicializo la lista vacia
    }
}
