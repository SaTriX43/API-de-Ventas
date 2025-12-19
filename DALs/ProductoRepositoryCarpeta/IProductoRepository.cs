using API_de_Ventas.Models;

namespace API_de_Ventas.DALs.ProductoRepositoryCarpeta
{
    public interface IProductoRepository
    {
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task<Producto?> ObtenerPorNombreAsync(string nombre);
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto> CrearAsync(Producto producto);
        Task<Producto> ActualizarAsync(Producto producto);
    }

}
