using API_de_Ventas.DTOs.ProductoDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.ProductoServiceCarpeta
{
    public interface IProductoService
    {
        Task<Result<ProductoDto>> CrearProductoAsync(ProductoCrearDto dto);
        Task<Result<ProductoDto>> ObtenerProductoPorIdAsync(int productoId);
        Task<Result<List<ProductoDto>>> ObtenerTodosProductosAsync();
        Task<Result> ActualizarProductoAsync(int productoId, ProductoActualizarDto dto);
    }
}
