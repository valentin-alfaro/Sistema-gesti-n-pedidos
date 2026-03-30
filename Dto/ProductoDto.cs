namespace Sistema_gestion_pedidos.Dto
{
    public class ProductoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public Decimal Stock { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
