using API_de_Ventas.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Ventas.DALs.ProductoRepositoryCarpeta
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Producto?> ObtenerProductoPorIdAsync(int id)
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto?> ObtenerProductoPorNombreAsync(string nombre)
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Nombre == nombre);
        }

        public async Task<List<Producto>> ObtenerTodosProductosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public Producto CrearProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            return producto;
        }
    }

}
