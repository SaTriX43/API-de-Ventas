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
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCrearDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var result = await _clienteService.CrearClienteAsync(dto);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = result.Error
                });
            }

            return CreatedAtAction(nameof(ObtenerClientePorId), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> ObtenerClientePorId(int clienteId)
        {

            var result = await _clienteService.ObtenerClientePorIdAsync(clienteId);

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
