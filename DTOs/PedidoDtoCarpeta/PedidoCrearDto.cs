using System.ComponentModel.DataAnnotations;

namespace API_de_Ventas.DTOs.PedidoDtoCarpeta
{
    public class PedidoCrearDto
    {
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public ICollection<PedidosDetallesCrearDto> Detalles { get; set; }
    }
}
