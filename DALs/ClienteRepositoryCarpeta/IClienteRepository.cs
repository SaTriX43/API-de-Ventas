using API_de_Ventas.Models;

namespace API_de_Ventas.DALs.ClienteRepositoryCarpeta
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObtenerPorEmailAsync(string email);
        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<Cliente> CrearAsync(Cliente cliente);
    }
}

