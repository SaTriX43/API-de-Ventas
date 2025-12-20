using API_de_Ventas.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Ventas.DALs.PedidoRepositoryCarpeta
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CrearPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido?> ObtenerPedidoDetallesPorId(int pedidoId)
        {
            var pedidoEcontrado = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            return pedidoEcontrado;
        }
        public async Task<List<Pedido>> ObtenerPedidosDetallesPorClienteId(int clienteId, DateTime? fechaInicio, DateTime? fechaFinal, int page, int pageSize)
        {
            var query = _context.Pedidos.Include(p => p.Detalles).Where(p => p.ClienteId == clienteId).AsQueryable();

            if(fechaInicio.HasValue)
            {
                query = query.Where(p => p.FechaPedido >=  fechaInicio.Value);
            }

            if (fechaFinal.HasValue)
            {
                query = query.Where(p => p.FechaPedido <= fechaFinal.Value);
            }

            query = query.OrderByDescending(p => p.FechaPedido);

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}
