using API_de_Ventas.DALs.ClienteRepositoryCarpeta;
using API_de_Ventas.DALs.PedidoRepositoryCarpeta;
using API_de_Ventas.DALs.ProductoRepositoryCarpeta;
using API_de_Ventas.DTOs.PedidoDtoCarpeta;
using API_de_Ventas.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace API_de_Ventas.Service.PedidoServiceCarpeta
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProductoRepository _productoRepository;


        public PedidoService(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository, IProductoRepository productoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _productoRepository = productoRepository;
        }

        public async Task<Result<PedidoDto>> CrearPedido(PedidoCrearDto pedidoCrearDto)
        {
            var clienteExiste = await _clienteRepository.ObtenerPorIdAsync(pedidoCrearDto.ClienteId);

            if (clienteExiste == null) {
                return Result<PedidoDto>.Failure($"El cliente con id = {pedidoCrearDto.ClienteId} no existe");
            }

            if(pedidoCrearDto.Detalles == null || pedidoCrearDto.Detalles.Count == 0)
            {
                return Result<PedidoDto>.Failure($"Los detalles del pedido no pueden ser null o estar vacios");
            }

            if(pedidoCrearDto.Detalles.Count > 100)
            {
                return Result<PedidoDto>.Failure("No se permiten mas de 100 productos por pedido");
            }

            decimal total = 0;

            var detallesModel = new List<PedidoDetalle>();
            var productosEnlistados = new List<int>();

            foreach (var detalle in pedidoCrearDto.Detalles)
            {
                if (detalle.Cantidad <= 0)
                {
                    return Result<PedidoDto>.Failure("La cantidad no puede ser menor o igual a 0");
                }

                var productoExiste = await _productoRepository.ObtenerPorIdAsync(detalle.ProductoId);

                if (productoExiste == null) {
                    return Result<PedidoDto>.Failure($"Su producto con id = {detalle.ProductoId} no existe");
                }

                if(!productoExiste.Activo)
                {
                    return Result<PedidoDto>.Failure($"No se pudo crear su pedido porque su producto con id = {productoExiste.Id} esta inactivo");
                }


                if(productosEnlistados.Contains(detalle.ProductoId))
                {
                    return Result<PedidoDto>.Failure($"El producto con id = {detalle.ProductoId} está repetido en el pedido");
                }



                var precioUnitario = productoExiste.Precio;
                var subtotal = precioUnitario * detalle.Cantidad;
                total += subtotal;

                detallesModel.Add(new PedidoDetalle
                {
                    ProductoId = detalle.ProductoId,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = precioUnitario,
                    Subtotal = subtotal
                });



                productosEnlistados.Add(detalle.ProductoId);

            }

            var pedidoModel = new Pedido
            {
                ClienteId = pedidoCrearDto.ClienteId,
                FechaPedido = DateTime.UtcNow,
                Total = total,
                Detalles = detallesModel
            };


            var pedidoCreado = await _pedidoRepository.CrearPedido(pedidoModel);

            var pedidoCreadoDto = new PedidoDto
            {
                ClienteId = pedidoCreado.ClienteId,
                FechaPedido = pedidoCreado.FechaPedido,
                Id = pedidoCreado.Id,
                Total = pedidoCreado.Total
            };

            return Result<PedidoDto>.Success(pedidoCreadoDto);
        }
        public async Task<Result<PedidoDto>> ObtenerPedidoDetallesPorId(int pedidoId)
        {
            var pedidoExiste = await _pedidoRepository.ObtenerPedidoDetallesPorId(pedidoId);

            if(pedidoExiste == null)
            {
                return Result<PedidoDto>.Failure($"Su pedido con id = {pedidoId} no existe");
            }

            var pedidoDto = new PedidoDto
            {
                Id = pedidoExiste.Id,
                ClienteId = pedidoExiste.ClienteId,
                FechaPedido = pedidoExiste.FechaPedido,
                Total = pedidoExiste.Total,
                DetallesDtos = pedidoExiste.Detalles.Select(d => new PedidoDetallesDto
                {
                    PedidoId = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    ProductoId = d.ProductoId,
                    Subtotal = d.Subtotal
                }).ToList()
            };

            return Result<PedidoDto>.Success(pedidoDto);
        }
        public async Task<Result<List<PedidoDto>>> ObtenerPedidoDetallesPorClienteId(int clienteId)
        {
            var clienteExiste = await _clienteRepository.ObtenerPorIdAsync(clienteId);

            if(clienteExiste == null)
            {
                return Result<List<PedidoDto>>.Failure($"Su cliente con id = {clienteId} no existe");
            }

            var pedidos = await _pedidoRepository.ObtenerPedidosDetallesPorClienteId(clienteId);


            var pedidosDto = pedidos.Select(p => new PedidoDto
            {
                Id = p.Id,
                ClienteId = p.ClienteId,
                FechaPedido = p.FechaPedido,
                Total = p.Total,
                DetallesDtos = p.Detalles.Select(d => new PedidoDetallesDto
                {
                    PedidoId = d.Id,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    ProductoId = d.ProductoId,
                    Subtotal = d.Subtotal
                }).ToList()
            }).ToList(); 

            return Result<List<PedidoDto>>.Success(pedidosDto);
        }
    }
}
