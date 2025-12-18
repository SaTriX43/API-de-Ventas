using API_de_Ventas.Models;

namespace API_de_Ventas.DTOs.ClienteDtoCarpeta
{
    public class ClienteDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } 

        public string Email { get; set; } 

        public string? Telefono { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
