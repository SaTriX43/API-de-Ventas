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

        public async Task<Cliente?> ObtenerClientePorEmailAsync(string email)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Cliente?> ObtenerClientePorIdAsync(int id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Cliente CrearCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            return cliente;
        }
    }

}
