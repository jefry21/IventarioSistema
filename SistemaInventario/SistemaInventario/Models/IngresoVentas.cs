using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaInventario.Models
{
    public class IngresoVentas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVenta { get; set; }

        [Required]
        public string ClienteVenta { get; set; }

        [Required]
        public decimal SubtotalVenta { get; set; }

        [Required]
        public decimal TotalVenta { get; set; }

        [Required]
        public DateTime FechaVenta { get; set; } = DateTime.Now;

        [Required]
        public List<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();

    }

}

public class VentaDetalle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdVentaDetalle { get; set; }

    [Required]
    public int ProductoId { get; set; }

    [Required]
    public decimal CantidadProducto { get; set; }

    [Required]
    public decimal PrecioUnitario { get; set; }

    [Required]
    public int IdVenta { get; set; }


}


