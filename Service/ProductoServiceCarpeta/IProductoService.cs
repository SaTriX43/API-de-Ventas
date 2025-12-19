using API_de_Ventas.DTOs.ProductoDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.ProductoServiceCarpeta
{
    public interface IProductoService
    {
        Task<Result<ProductoDto>> CrearAsync(ProductoCrearDto dto);
        Task<Result<ProductoDto>> ObtenerPorIdAsync(int id);
        Task<Result<List<ProductoDto>>> ObtenerTodosAsync();
        Task<Result<ProductoDto>> ActualizarAsync(int id, ProductoActualizarDto dto);
    }
}
