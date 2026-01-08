using API_de_Ventas.Models;

namespace API_de_Ventas.DALs.PedidoRepositoryCarpeta
{
    public interface IPedidoRepository
    {
        public Pedido CrearPedido(Pedido pedido);
        public Task<Pedido?> ObtenerPedidoDetallesPorIdAsync(int pedidoId);
        public Task<List<Pedido>> ObtenerPedidosDetallesPorClienteIdAsync(int clienteId, DateTime? fechaInicio, DateTime? fechaFinal, int page, int pageSize);
        public Task<List<Pedido>> ObtenerPedidosDetallesPorClienteIdYUsuarioIdAsync(int clienteId,int usuarioId ,DateTime? fechaInicio, DateTime? fechaFinal, int page, int pageSize);
        public Task<List<Pedido>> ObtenerPedidosAsync(int? usuarioId, DateTime? fechaInicio, DateTime? fechaFinal, int page, int pageSize);
    }
}
