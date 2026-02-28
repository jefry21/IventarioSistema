using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {

        private readonly BaseContext _context;

        public VentasController(BaseContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ConsultaVentas()
        {
            try
            {
                var Ventas = _context.Ventas.ToList();

                if (Ventas.Count == 0)
                {
                    return StatusCode(200, "No hay Ventas");
                }
                else
                {
                    return StatusCode(200, Ventas);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la venta", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult CrearVenta([FromBody] Ventas Ventas)
        {
            try
            {
                if (Ventas == null || Ventas.Productos.Count == 0)
                {
                    return BadRequest(new { mensaje = "Debe enviar al menos un producto" });
                }

                var venta = new Venta
                {
                    ClienteVenta = Ventas.ClienteVenta,
                    SubtotalVenta = Ventas.SubtotalVenta,
                    TotalVenta = Ventas.TotalVenta,
                    FechaVenta = DateTime.Now
                };

                foreach (var VentaItem in Ventas.Productos)
                {
                    var producto = _context.Productos.FirstOrDefault(x => x.Id == VentaItem.ProductoId);

                    if (producto.Stock < VentaItem.CantidadProducto)
                    {
                        return StatusCode(400, "No hay suficiente Stock para el producto con codigo: "+ VentaItem.ProductoId);
                    }
                    else
                    {
                        producto.Stock -= (int)VentaItem.CantidadProducto;

                        venta.Detalles.Add(new VentaDetalle
                        {
                            ProductoId = VentaItem.ProductoId,
                            CantidadProducto = VentaItem.CantidadProducto,
                            PrecioUnitario = VentaItem.PrecioUnitario

                        });
                    }

                    
                }

              
                _context.Ventas.Add(venta);
                _context.SaveChanges();  

                return StatusCode(201, new { mensaje = "Venta creada exitosamente"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la venta", error = ex.Message });
            }
        }

        public class Ventas
        {
            [Required]
            public string ClienteVenta { get; set; }
            [Required]
            public decimal SubtotalVenta { get; set; }
            [Required]
            public decimal TotalVenta { get; set; }
            [Required]
            public List<VentaDetalle> Productos { get; set; } = new List<VentaDetalle>();
        }

    }
}
