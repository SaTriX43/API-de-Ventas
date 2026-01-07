using API_de_Ventas.DTOs.AutenticacionDtoCarpeta;
using API_de_Ventas.Service.AutenticacionServiceCarpeta;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/autenticacion")]
public class AutenticacionController : ControllerBase
{
    private readonly IAutenticacionService _autenticacionService;

    public AutenticacionController(IAutenticacionService autenticacionService)
    {
        _autenticacionService = autenticacionService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _autenticacionService.LoginAsync(dto);

        if (result.IsFailure)
        {
            return Unauthorized(new
            {
                success = false,
                error = result.Error
            });
        }

        return Ok(new
        {
            success = true,
            accessToken = result.Value
        });
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registro([FromBody] RegistroDto dto)
    {
        var result = await _autenticacionService.RegistroAsync(dto);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                success = false,
                error = result.Error
            });
        }

        return Ok(new
        {
            success = true,
            accessToken = result.Value
        });
    }
}
