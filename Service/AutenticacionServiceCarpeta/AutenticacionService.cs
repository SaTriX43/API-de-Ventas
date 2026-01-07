using API_de_Ventas.DALs;
using API_de_Ventas.DALs.IUsuarioRepositoryCarpeta;
using API_de_Ventas.DTOs.AutenticacionDtoCarpeta;
using API_de_Ventas.Models;
using API_de_Ventas.Models.Enums;
using API_de_Ventas.Service.AutenticacionServiceCarpeta;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AutenticacionService : IAutenticacionService
{
    private readonly IUnidadDeTrabajo _unidadDeTrabajo;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AutenticacionService(
        IUsuarioRepository usuarioRepository,
        IConfiguration configuration,
        IUnidadDeTrabajo unidadDeTrabajo
        )
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
        _unidadDeTrabajo = unidadDeTrabajo;
    }

    public async Task<Result<AutenticacionRespuestaDto>> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(dto.Email);

        if (usuario == null ||
            !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
        {
            return Result<AutenticacionRespuestaDto>.Failure("Credenciales inválidas");
        }

        var token = GenerarJwt(usuario);
        return Result<AutenticacionRespuestaDto>.Success(new AutenticacionRespuestaDto
        {
            Token = token
        });
    }

    public async Task<Result<AutenticacionRespuestaDto>> RegistroAsync(RegistroDto dto)
    {
        var existe = await _usuarioRepository.ObtenerUsuarioPorEmailAsync(dto.Email);

        if (existe != null)
        {
            return Result<AutenticacionRespuestaDto>.Failure("El email ya está registrado");
        }

        var usuario = new Usuario
        {
            Name = dto.Nombre,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Rol = RolUsuario.Vendedor, // 👈 default
            CreatedAt = DateTime.UtcNow
        };

        _usuarioRepository.CrearUsuario(usuario);
        await _unidadDeTrabajo.GuardarCambiosAsync();

        var token = GenerarJwt(usuario);
        return Result<AutenticacionRespuestaDto>.Success(new AutenticacionRespuestaDto
        {
            Token = token
        });
    }

    private string GenerarJwt(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["Jwt:ExpireMinutes"]!)
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
