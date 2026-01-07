using API_de_Ventas.DTOs.PedidoDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.PedidoServiceCarpeta
{
    public interface IPedidoService
    {
        public Task<Result<PedidoDto>> CrearPedidoAsync(PedidoCrearDto pedidoCrear);
        public Task<Result<PedidoDto>> ObtenerPedidoDetallesPorIdAsync(int pedidoId);
        public Task<Result<List<PedidoDto>>> ObtenerPedidoDetallesPorClienteIdAsync(int clienteId, DateTime? fechaInicio, DateTime? fechaFinal, int page, int pageSize);
    }
}
