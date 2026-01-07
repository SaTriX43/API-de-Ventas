using System.ComponentModel.DataAnnotations;

namespace API_de_Ventas.DTOs.AutenticacionDtoCarpeta
{
    public class RegistroDto
    {
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }

}
