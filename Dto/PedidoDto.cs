namespace Sistema_gestion_pedidos.Dto
{
    public class PedidoDto
    {
        public int IdCliente { get; set; }
        public List<DetallePedidoDto> Detalle { get; set; } = new List<DetallePedidoDto>();
    }
}
