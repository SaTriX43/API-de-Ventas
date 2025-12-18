using API_de_Ventas.DTOs.ClienteDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.ClienteServiceCarpeta
{
    public interface IClienteService
    {
        Task<Result<ClienteDto>> CrearAsync(ClienteCrearDto dto);
        Task<Result<ClienteDto>> ObtenerPorIdAsync(int id);
    }

}
