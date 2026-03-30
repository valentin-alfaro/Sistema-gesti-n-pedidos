using System.ComponentModel.DataAnnotations;

namespace Sistema_gestion_pedidos.Dto
{
    public class ClienteDto
    {
        //Pequeña validación para el DNI, que solo permita números.
        [RegularExpression(@"^\d+$", ErrorMessage = "El DNI solo puede contener números.")]
        public string DNI { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
