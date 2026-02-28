namespace SistemaInventario.Models
{
    public class VentaRequest
    {
        public string ClienteVenta { get; set; }
        public decimal SubtotalVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public List<VentaDetalleRequest> Productos { get; set; } = new List<VentaDetalleRequest>();
    }

    public class VentaDetalleRequest
    {
        public int IdVentaDetalle { get; set; }
        public int ProductoId { get; set; }
        public int CantidadProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdVenta { get; set; }
    }
}
