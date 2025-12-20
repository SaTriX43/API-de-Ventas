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

        [HttpGet("obtener-pedido-detalles/{pedidoId}")]
        public async Task<IActionResult> ObtenerPedidoDetallesPorId(int pedidoId)
        {
            if(pedidoId <= 0)
            {
                return BadRequest( new
                {
                    success = false,
                    error = "Su pedidoId no puede ser menor o igual a 0"
                });
            }

            var pedidoDetalle = await _pedidoService.ObtenerPedidoDetallesPorId(pedidoId);

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
                success = false,
                valor = pedidoDetalle.Value
            });
        }

        [HttpGet("obtener-pedido-detalles-cliente/{clienteId}")]
        public async Task<IActionResult> ObtenerPedidoDetallesPorClienteId(int clienteId)
        {
            if (clienteId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    error = "Su clienteId no puede ser menor o igual a 0"
                });
            }

            var pedidoDetalle = await _pedidoService.ObtenerPedidoDetallesPorClienteId(clienteId);

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
                success = false,
                valor = pedidoDetalle.Value
            });
        }

        [HttpPost("crear-pedido")]
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

            var pedidoCreado = await _pedidoService.CrearPedido(pedidoCrear);

            if(pedidoCreado.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = pedidoCreado.Error
                });
            }

            return Ok(pedidoCreado.Value);
        }
    }
}
