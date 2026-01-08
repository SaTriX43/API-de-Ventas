using API_de_Ventas.DTOs.PedidoDtoCarpeta;
using API_de_Ventas.Models;
using API_de_Ventas.Service.PedidoServiceCarpeta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("{pedidoId}")]
        public async Task<IActionResult> ObtenerPedidoDetallesPorId(int pedidoId)
        {

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "usuarioId invalido"
                });
            }

            var esAdmin = User.IsInRole("Admin");

            var pedidoDetalle = await _pedidoService.ObtenerPedidoDetallesPorIdAsync(pedidoId,esAdmin,usuarioId);

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

        [Authorize]
        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> ObtenerPedidoDetallesPorClienteId(
            int clienteId,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFinal,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
            )
        {

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "usuarioId invalido"
                });
            }

            var esAdmin = User.IsInRole("Admin");

            var pedidoDetalle = await _pedidoService.ObtenerPedidoDetallesPorClienteIdAsync(clienteId,usuarioId,esAdmin ,fechaInicio, fechaFinal, page, pageSize);

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ObtenerPedidos(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFinal,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "usuarioId invalido"
                });
            }

            var esAdmin = User.IsInRole("Admin");

            var pedidos = await _pedidoService.ObtenerPedidosAsync(usuarioId,esAdmin,fechaInicio,fechaFinal,page,pageSize);

            return Ok(new
            {
                success = true,
                valor = pedidos.Value
            });
        }

        [Authorize]
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

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "usuarioId invalido"
                });
            }

            var pedidoCreado = await _pedidoService.CrearPedidoAsync(pedidoCrear,usuarioId);

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

        [Authorize]
        [HttpGet("{pedidoId}/pdf")]
        public async Task<IActionResult> ExportarPedidoIdPdf(int pedidoId)
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return BadRequest(new
                {
                    success = false,
                    error = "usuarioId invalido"
                });
            }

            var esAdmin = User.IsInRole("Admin");

            var pedidoPdf = await _pedidoService.ExportarPedidoIdPdfAsync(pedidoId ,usuarioId, esAdmin);

            if (pedidoPdf.IsFailure)
            {
                return BadRequest(new
                {
                    success = false,
                    error = pedidoPdf.Error
                });
            }

            return File(
                pedidoPdf.Value,
                "application/pdf",
                $"pedido_{pedidoId}.pdf");
            }
    }
}
