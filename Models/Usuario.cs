using API_de_Ventas.Models.Enums;

namespace API_de_Ventas.Models
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public RolUsuario Rol {  get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
