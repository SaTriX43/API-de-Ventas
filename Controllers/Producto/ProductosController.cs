using API_de_Ventas.DTOs.ProductoDtoCarpeta;
using API_de_Ventas.Service.ProductoServiceCarpeta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Ventas.Controllers.Producto
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoCrearDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var result = await _productoService.CrearProductoAsync(dto);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = result.Error
                });
            }

            return CreatedAtAction(
                nameof(ObtenerProductoPorId),
                new { productoId = result.Value.Id },
                result.Value
                );
        }

        [Authorize]
        [HttpGet("{productoId}")]
        public async Task<IActionResult> ObtenerProductoPorId(int productoId)
        {
           
            var result = await _productoService.ObtenerProductoPorIdAsync(productoId);

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosProductos()
        {
            var result = await _productoService.ObtenerTodosProductosAsync();

            return Ok(new
            {
                success = true,
                value = result.Value
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{productoId}")]
        public async Task<IActionResult> ActualizarProducto(int productoId, [FromBody] ProductoActualizarDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var result = await _productoService.ActualizarProductoAsync(productoId, dto);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = result.Error
                });
            }

            return NoContent();
        }
    }

}
