using API_de_Ventas.DTOs.AutenticacionDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.AutenticacionServiceCarpeta
{
    public interface IAutenticacionService
    {
        Task<Result<AutenticacionRespuestaDto>> LoginAsync(LoginDto dto);
        Task<Result<AutenticacionRespuestaDto>> RegistroAsync(RegistroDto dto);
    }

}
