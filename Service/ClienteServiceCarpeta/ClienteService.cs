using API_de_Ventas.DALs.ClienteRepositoryCarpeta;
using API_de_Ventas.DTOs.ClienteDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.ClienteServiceCarpeta
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<ClienteDto>> CrearAsync(ClienteCrearDto dto)
        {
            var emailNormalizado = dto.Email.Trim().ToLower();

            var existe = await _repository.ObtenerPorEmailAsync(emailNormalizado);
            if (existe != null)
            {
                return Result<ClienteDto>.Failure("Ya existe un cliente con ese email");
            }

            var cliente = new Cliente
            {
                Nombre = dto.Nombre.Trim(),
                Email = emailNormalizado,
                Telefono = dto.Telefono
            };

            var creado = await _repository.CrearAsync(cliente);

            return Result<ClienteDto>.Success(new ClienteDto
            {
                Id = creado.Id,
                Nombre = creado.Nombre,
                Email = creado.Email,
                Telefono = creado.Telefono,
                FechaCreacion = creado.FechaCreacion
            });
        }

        public async Task<Result<ClienteDto>> ObtenerPorIdAsync(int id)
        {
            var cliente = await _repository.ObtenerPorIdAsync(id);
            if (cliente == null)
            {
                return Result<ClienteDto>.Failure("Cliente no encontrado");
            }

            return Result<ClienteDto>.Success(new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                FechaCreacion = cliente.FechaCreacion
            });
        }
    }

}
