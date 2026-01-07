using API_de_Ventas.DALs;
using API_de_Ventas.DALs.ClienteRepositoryCarpeta;
using API_de_Ventas.DTOs.ClienteDtoCarpeta;
using API_de_Ventas.Models;

namespace API_de_Ventas.Service.ClienteServiceCarpeta
{
    public class ClienteService : IClienteService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository,IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _clienteRepository = clienteRepository;
        }

        public async Task<Result<ClienteDto>> CrearClienteAsync(ClienteCrearDto dto)
        {
            var emailNormalizado = dto.Email.Trim().ToLower();

            var existe = await _clienteRepository.ObtenerClientePorEmailAsync(emailNormalizado);
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

            var creado = _clienteRepository.CrearCliente(cliente);
            await _unidadDeTrabajo.GuardarCambiosAsync();


            return Result<ClienteDto>.Success(new ClienteDto
            {
                Id = creado.Id,
                Nombre = creado.Nombre,
                Email = creado.Email,
                Telefono = creado.Telefono,
                FechaCreacion = creado.FechaCreacion
            });
        }

        public async Task<Result<ClienteDto>> ObtenerClientePorIdAsync(int clienteId)
        {
            if(clienteId <= 0)
            {
                return Result<ClienteDto>.Failure("Su clienteId no puede ser menor o igual a 0");
            }

            var cliente = await _clienteRepository.ObtenerClientePorIdAsync(clienteId);
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
