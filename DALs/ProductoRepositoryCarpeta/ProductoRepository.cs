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

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto?> ObtenerPorNombreAsync(string nombre)
        {
            return await _context.Productos.FirstOrDefaultAsync(p => p.Nombre == nombre);
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> CrearAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto> ActualizarAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
            return producto;
        }
    }

}
