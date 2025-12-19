using API_de_Ventas.DTOs.ProductoDtoCarpeta;
using API_de_Ventas.Service.ProductoServiceCarpeta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Ventas.Controllers.Producto
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCrearDto dto)
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

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = result.Value.Id },
                result.Value
                );
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

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var result = await _service.ObtenerTodosAsync();

            return Ok(new
            {
                success = true,
                value = result.Value
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoActualizarDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var result = await _service.ActualizarAsync(id, dto);

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
    }

}
