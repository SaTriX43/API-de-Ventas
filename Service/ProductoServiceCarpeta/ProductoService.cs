using API_de_Ventas.DALs;
using API_de_Ventas.DALs.ProductoRepositoryCarpeta;
using API_de_Ventas.DTOs.ProductoDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.ProductoServiceCarpeta
{
    public class ProductoService : IProductoService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _productoRepository = productoRepository;
        }

        public async Task<Result<ProductoDto>> CrearProductoAsync(ProductoCrearDto dto)
        {
            var nombreNormalizado = dto.Nombre.Trim().ToLower();

            var existe = await _productoRepository.ObtenerProductoPorNombreAsync(nombreNormalizado);
            if (existe != null)
            {
                return Result<ProductoDto>.Failure("Ya existe un producto con ese nombre");
            }

            var producto = new Producto
            {
                Nombre = nombreNormalizado,
                Precio = dto.Precio,
                Activo = true
            };

            var creado = _productoRepository.CrearProducto(producto);

            await _unidadDeTrabajo.GuardarCambiosAsync();

            return Result<ProductoDto>.Success(Mapear(creado));
        }

        public async Task<Result<ProductoDto>> ObtenerProductoPorIdAsync(int productoId)
        {
            if(productoId <= 0)
            {
                return Result<ProductoDto>.Failure("Su productoId no puede ser menor o igual a 0");
            }

            var producto = await _productoRepository.ObtenerProductoPorIdAsync(productoId);
            if (producto == null)
            {
                return Result<ProductoDto>.Failure("Producto no encontrado");
            }

            return Result<ProductoDto>.Success(Mapear(producto));
        }

        public async Task<Result<List<ProductoDto>>> ObtenerTodosProductosAsync()
        {
            var productos = await _productoRepository.ObtenerTodosProductosAsync();

            var dto = productos
                .Select(Mapear)
                .ToList();

            return Result<List<ProductoDto>>.Success(dto);
        }

        public async Task<Result> ActualizarProductoAsync(int productoId, ProductoActualizarDto dto)
        {
            if (productoId <= 0)
            {
                return Result.Failure("Su productoId no puede ser menor o igual a 0");
            }

            var producto = await _productoRepository.ObtenerProductoPorIdAsync(productoId);
            if (producto == null)
            {
                return Result.Failure("Producto no encontrado");
            }

            var nombreNormalizado = dto.Nombre.Trim().ToLower();

            var existe = await _productoRepository.ObtenerProductoPorNombreAsync(nombreNormalizado);
            if (existe != null && existe.Id != productoId)
            {
                return Result.Failure("Ya existe otro producto con ese nombre");
            }

            producto.Nombre = nombreNormalizado;
            producto.Precio = dto.Precio;
            producto.Activo = dto.Activo;

            await _unidadDeTrabajo.GuardarCambiosAsync();

            return Result.Success();
        }

        private static ProductoDto Mapear(Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Activo = producto.Activo,
                FechaCreacion = producto.FechaCreacion
            };
        }
    }


}
