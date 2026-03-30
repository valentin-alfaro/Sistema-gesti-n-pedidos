using System.ComponentModel.DataAnnotations;

namespace Sistema_gestion_pedidos.Models
{
    public class Cliente
    {
        //Propiedades
        [Key]
        public int IdClientes { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; } = null!;//Para evitar el error de valor nulo
        public string Apellido { get; set; } = null!;//Para evitar el error de valor nulo   
        public string Email { get; set; } = string.Empty;//Para evitar el error de valor nulo

        //Relaciones
        public List<Pedido> Pedido { get; set; } = new List<Pedido>();
    }
}
