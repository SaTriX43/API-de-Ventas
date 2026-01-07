using API_de_Ventas.Models;

namespace API_de_Ventas.DALs.IUsuarioRepositoryCarpeta
{
    public interface IUsuarioRepository
    {
        public Task<Usuario?> ObtenerUsuarioPorEmailAsync(string email);
        public Task<Usuario?> ObtenerUsuarioPorIdAsync(int usuarioId);
        public void CrearUsuario(Usuario usuario);
    }
}
