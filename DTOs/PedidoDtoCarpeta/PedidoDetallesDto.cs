using System.ComponentModel.DataAnnotations;

namespace API_de_Ventas.DTOs.PedidoDtoCarpeta
{
    public class PedidoDetallesDto
    {
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
