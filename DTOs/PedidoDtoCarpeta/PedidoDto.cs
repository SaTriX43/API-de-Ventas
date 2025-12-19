using API_de_Ventas.Models;

namespace API_de_Ventas.DTOs.PedidoDtoCarpeta
{
    public class PedidoDto
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

    }
}
