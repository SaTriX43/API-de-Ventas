using System.ComponentModel.DataAnnotations;

namespace API_de_Ventas.DTOs.PedidoDtoCarpeta
{
    public class PedidosDetallesCrearDto
    {
        [Required]
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
