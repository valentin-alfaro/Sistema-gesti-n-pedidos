using Sistema_gestion_pedidos.Dto;
using Sistema_gestion_pedidos.Models;

namespace Sistema_gestion_pedidos.Service
{
    public interface IPedidoService
    {
        Task<List<Pedido>> GetPedidos();
        Task<Pedido> GetPedidoById(int id);
        Task<Pedido> CreatePedido(PedidoDto dto);
        //Ambos devuelven bool porque si el pedido a actualizar o eliminar no existe, se devuelve false para indicar que la operación no se pudo realizar.
        Task<bool> UpdatePedido(int id, PedidoDto dto);
        Task<bool> DeletePedido(int id);
    }
}
