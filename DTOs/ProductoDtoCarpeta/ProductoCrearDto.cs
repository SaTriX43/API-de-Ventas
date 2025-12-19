using System.ComponentModel.DataAnnotations;

namespace API_de_Ventas.DTOs.ProductoDtoCarpeta
{
    public class ProductoCrearDto
    {
        [Required]
        public string Nombre { get; set; } = null!;

        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }
    }
}
