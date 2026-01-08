using API_de_Ventas.Models;

namespace API_de_Ventas.Service.PedidoServiceCarpeta
{
    public interface IPedidoPdfService
    {
        public byte[] GenerarPedidoPdf(Pedido pedido);
    }
}
