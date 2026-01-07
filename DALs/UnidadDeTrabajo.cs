using API_de_Ventas.Models;

namespace API_de_Ventas.DALs
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly ApplicationDbContext _context;

        public UnidadDeTrabajo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
