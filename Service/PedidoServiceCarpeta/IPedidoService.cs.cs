using API_de_Ventas.DTOs.PedidoDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.PedidoServiceCarpeta
{
    public interface IPedidoService
    {
        public Task<Result<PedidoDto>> CrearPedido(PedidoCrearDto pedidoCrear);
        public Task<Result<PedidoDto>> ObtenerPedidoDetallesPorId(int pedidoId);
        public Task<Result<List<PedidoDto>>> ObtenerPedidoDetallesPorClienteId(int clienteId);
    }
}
