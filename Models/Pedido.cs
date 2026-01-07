namespace API_de_Ventas.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;

        public decimal Total { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<PedidoDetalle> Detalles { get; set; } = new List<PedidoDetalle>();
    }

}
