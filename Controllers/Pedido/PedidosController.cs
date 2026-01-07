using API_de_Ventas.DTOs.PedidoDtoCarpeta;
using API_de_Ventas.Service.PedidoServiceCarpeta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_de_Ventas.Controllers.Pedido
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService) { 
            _pedidoService = pedidoService;
        }

        [HttpGet("{pedidoId}")]
        public async Task<IActionResult> ObtenerPedidoDetallesPorId(int pedidoId)
        {
           
            var pedidoDetalle = await _pedidoService.ObtenerPedidoDetallesPorIdAsync(pedidoId);

            if(pedidoDetalle.IsFailure)
            {
                return NotFound(new
                {
                    success = false,
                    error = pedidoDetalle.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = pedidoDetalle.Value
            });
        }

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> ObtenerPedidoDetallesPorClienteId(
            int clienteId,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFinal,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
            )
        {
            var pedidoDetalle = await _pedidoService.ObtenerPedidoDetallesPorClienteIdAsync(clienteId, fechaInicio, fechaFinal, page, pageSize);

            if (pedidoDetalle.IsFailure)
            {
                return NotFound(new
                {
                    success = false,
                    error = pedidoDetalle.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = pedidoDetalle.Value
            });
        }

        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoCrearDto pedidoCrear)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ModelState
                });
            }

            var pedidoCreado = await _pedidoService.CrearPedidoAsync(pedidoCrear);

            if(pedidoCreado.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = pedidoCreado.Error
                });
            }

            return Ok(new
            {
                success = true,
                valor = pedidoCreado.Value
            });
        }
    }
}
