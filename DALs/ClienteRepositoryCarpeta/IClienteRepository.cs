using API_de_Ventas.Models;

namespace API_de_Ventas.DALs.ClienteRepositoryCarpeta
{
    public interface IClienteRepository
    {
        public Task<Cliente?> ObtenerClientePorEmailAsync(string email);
        public Task<Cliente?> ObtenerClientePorIdAsync(int id);
        public Cliente CrearCliente(Cliente cliente);
    }
}

