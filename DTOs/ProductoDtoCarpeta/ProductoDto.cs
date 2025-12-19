namespace API_de_Ventas.DTOs.ProductoDtoCarpeta
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
