using API_de_Ventas.DALs.ProductoRepositoryCarpeta;
using API_de_Ventas.DTOs.ProductoDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.ProductoServiceCarpeta
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ProductoDto>> CrearAsync(ProductoCrearDto dto)
        {
            var nombreNormalizado = dto.Nombre.Trim().ToLower();

            var existe = await _repository.ObtenerPorNombreAsync(nombreNormalizado);
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

            var creado = await _repository.CrearAsync(producto);

            return Result<ProductoDto>.Success(Mapear(creado));
        }

        public async Task<Result<ProductoDto>> ObtenerPorIdAsync(int id)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);
            if (producto == null)
            {
                return Result<ProductoDto>.Failure("Producto no encontrado");
            }

            return Result<ProductoDto>.Success(Mapear(producto));
        }

        public async Task<Result<List<ProductoDto>>> ObtenerTodosAsync()
        {
            var productos = await _repository.ObtenerTodosAsync();

            var dto = productos
                .Select(Mapear)
                .ToList();

            return Result<List<ProductoDto>>.Success(dto);
        }

        public async Task<Result<ProductoDto>> ActualizarAsync(int id, ProductoActualizarDto dto)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);
            if (producto == null)
            {
                return Result<ProductoDto>.Failure("Producto no encontrado");
            }

            var nombreNormalizado = dto.Nombre.Trim().ToLower();

            var existe = await _repository.ObtenerPorNombreAsync(nombreNormalizado);
            if (existe != null && existe.Id != id)
            {
                return Result<ProductoDto>.Failure("Ya existe otro producto con ese nombre");
            }

            producto.Nombre = nombreNormalizado;
            producto.Precio = dto.Precio;
            producto.Activo = dto.Activo;

            var actualizado = await _repository.ActualizarAsync(producto);

            return Result<ProductoDto>.Success(Mapear(actualizado));
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
