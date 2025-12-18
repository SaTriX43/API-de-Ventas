using System.ComponentModel.DataAnnotations;

namespace API_de_Ventas.DTOs.ClienteDtoCarpeta
{
    public class ClienteCrearDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Telefono { get; set; }
    }
}
