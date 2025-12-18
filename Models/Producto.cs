namespace API_de_Ventas.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal Precio { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
    }

}
