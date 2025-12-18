namespace API_de_Ventas.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Telefono { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
