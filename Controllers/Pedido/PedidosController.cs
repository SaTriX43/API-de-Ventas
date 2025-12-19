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
