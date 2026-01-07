using API_de_Ventas.DTOs.ClienteDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.ClienteServiceCarpeta
{
    public interface IClienteService
    {
        Task<Result<ClienteDto>> CrearClienteAsync(ClienteCrearDto dto);
        Task<Result<ClienteDto>> ObtenerClientePorIdAsync(int clienteId);
    }

}
