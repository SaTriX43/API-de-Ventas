using API_de_Ventas.DTOs.PedidoDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.PedidoServiceCarpeta
{
    public interface IPedidoService
    {
        public Task<Result<PedidoDto>> CrearPedidoAsync(PedidoCrearDto pedidoCrear,int usuarioId);
        public Task<Result<PedidoDto>> ObtenerPedidoDetallesPorIdAsync(int pedidoId,bool esAdmin, int usuarioId);
        public Task<Result<List<PedidoDto>>> ObtenerPedidoDetallesPorClienteIdAsync(int clienteId,int usuarioId,bool esAdmin ,DateTime? fechaInicio, DateTime? fechaFinal, int page, int pageSize);
        public Task<Result<List<PedidoDto>>> ObtenerPedidosAsync(int usuarioId, bool esAdmin,DateTime? fechaInicio, DateTime? fechaFinal, int page, int pageSize);
        //public Task<Result<>> ExportarPdfAsync(int usuarioId,bool esAdmin);
    }
}
