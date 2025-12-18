using API_de_Ventas.DTOs.ClienteDtoCarpeta;
using API_de_Ventas.Service.ClienteServiceCarpeta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Ventas.Controllers.Cliente
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ClienteCrearDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var result = await _service.CrearAsync(dto);

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
                value = result.Value
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    error = "Id inválido"
                });
            }

            var result = await _service.ObtenerPorIdAsync(id);

            if (result.IsFailure)
            {
                return NotFound(new
                {
                    success = false,
                    error = result.Error
                });
            }

            return Ok(new
            {
                success = true,
                value = result.Value
            });
        }
    }

}
