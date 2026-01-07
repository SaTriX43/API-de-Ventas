using API_de_Ventas.Models;

namespace API_de_Ventas.DALs.ProductoRepositoryCarpeta
{
    public interface IProductoRepository
    {
        public Task<Producto?> ObtenerProductoPorIdAsync(int id);
        public Task<Producto?> ObtenerProductoPorNombreAsync(string nombre);
        public Task<List<Producto>> ObtenerTodosProductosAsync();
        public Producto CrearProducto(Producto producto);
    }

}
