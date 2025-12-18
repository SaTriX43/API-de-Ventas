namespace API_de_Ventas.Models
{
    public class PedidoDetalle
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }

        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }

        public Pedido Pedido { get; set; } = null!;

        public Producto Producto { get; set; } = null!;
    }

}
