using API_de_Ventas.Models;
using Microsoft.EntityFrameworkCore;

namespace API_de_Ventas.DALs.IUsuarioRepositoryCarpeta
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerUsuarioPorEmailAsync(string email)
        {
            return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int usuarioId)
        {
            return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Id == usuarioId);
        }

        public void CrearUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

    }
}
