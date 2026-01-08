using API_de_Ventas.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace API_de_Ventas.Service.PedidoServiceCarpeta
{
    public class PedidoPdfService : IPedidoPdfService
    {
        private readonly IWebHostEnvironment _env;

        public PedidoPdfService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public byte[] GenerarPedidoPdf(Pedido pedido)
        {
            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido));
            
            var detalles = pedido.Detalles?.ToList() ?? new List<PedidoDetalle>();

            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(col =>
                    {
                        col.Item().Text("FACTURA / PEDIDO").FontSize(18).Bold();
                        col.Item().Text($"Pedido ID: {pedido.Id}");
                        col.Item().Text($"Fecha: {pedido.FechaPedido:yyyy-MM-dd HH:mm}");
                        col.Item().Text($"Cliente ID: {pedido.ClienteId}");
                    });

                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        col.Item().Text("Detalles").Bold();

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3); // ProductoId 
                                columns.RelativeColumn(2); // Cantidad
                                columns.RelativeColumn(2); // Precio
                                columns.RelativeColumn(2); // Subtotal
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Producto").Bold();
                                header.Cell().Text("Cantidad").Bold();
                                header.Cell().Text("Precio").Bold();
                                header.Cell().Text("Subtotal").Bold();
                            });

                            foreach (var d in detalles)
                            {
                                table.Cell().Text(d.ProductoId.ToString());
                                table.Cell().Text(d.Cantidad.ToString());
                                table.Cell().Text(d.PrecioUnitario.ToString("0.00"));
                                table.Cell().Text(d.Subtotal.ToString("0.00"));
                            }
                        });

                        col.Item().PaddingTop(10).AlignRight().Text($"TOTAL: {pedido.Total:0.00}").FontSize(14).Bold();
                    });

                    page.Footer().AlignCenter().Text("Generado por API de Ventas");
                });
            }).GeneratePdf();
            
            return pdfBytes;
        }
    }
}
