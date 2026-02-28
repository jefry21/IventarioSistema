namespace SistemaInventario.Models
{
    public class VentaDetalle
    {
        public int IdVentaDetalle { get; set; }
        public int ProductoId { get; set; }
        public int CantidadProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IdVenta { get; set; }
    }
}
