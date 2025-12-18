using API_de_Ventas.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Ventas.DALs.ClienteRepositoryCarpeta
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> ObtenerPorEmailAsync(string email)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> CrearAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }

}
