using API_de_Ventas.Models;

namespace API_de_Ventas.DALs.PedidoRepositoryCarpeta
{
    public interface IPedidoRepository
    {
        public Task<Pedido> CrearPedido(Pedido pedido);
        public Task<Pedido?> ObtenerPedidoDetallesPorId(int pedidoId);
        public Task<List<Pedido>> ObtenerPedidosDetallesPorClienteId(int clienteId);

    }
}
