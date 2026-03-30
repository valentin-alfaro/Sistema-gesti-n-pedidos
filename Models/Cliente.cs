using System.ComponentModel.DataAnnotations;

namespace Sistema_gestion_pedidos.Models
{
    public class Cliente
    {
        //Propiedades
        [Key]
        public int IdCliente { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; } = null!;//Para evitar el error de valor nulo
        public string Apellido { get; set; } = null!;//Para evitar el error de valor nulo   
        public string Email { get; set; } = string.Empty;//Para evitar el error de valor nulo

        //Relaciones
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
