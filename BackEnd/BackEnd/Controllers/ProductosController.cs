using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;



namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private readonly BaseContext _context;

        public ProductosController(BaseContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Producto()
        {

            try
            {
                var productos = _context.Productos.ToList();

                if (productos.Count == 0)
                {
                    var Producto = new List<Productos> {

                    new Productos
                    {
                        Id = 0,
                        Nombre = "N/A",
                        Precio = 0,
                        Stock = 0,
                        Activo = 0
                    }


                };

                    return StatusCode(200, Producto);
                }



                return StatusCode(200, productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la venta", error = ex.Message });
            }

           
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Productos Producto)
        {

            try
            {

                if (Producto.Nombre == string.Empty || Producto.Precio == 0 || Producto.Stock == 0 || Producto.MInimoStock == 0 || Producto.Precio < 1 || Producto.MInimoStock < 1 || Producto.Stock < 1)
                {
                    return StatusCode(400, "Campos en blanco");
                }
                else
                {
                    _context.Add(Producto);
                    _context.SaveChanges();
                }

                return StatusCode(201, "Producto Creado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la venta", error = ex.Message });
            }


        }

        [Authorize]
        [HttpPut]
        public IActionResult Put([FromBody] Productos Producto)
        {

            try
            {
                if (Producto.Nombre == string.Empty || Producto.Precio == 0 || Producto.Stock == 0 || Producto.MInimoStock == 0 || Producto.Precio < 1 || Producto.MInimoStock < 1 || Producto.Stock < 1)
                {
                    return StatusCode(400, "Campos en blanco");
                }
                else
                {
                    var Existencia = _context.Productos.Find(Producto.Id);

                    if (Existencia == null)
                    {
                        return StatusCode(404, "Producto no encontrado");
                    }
                    else
                    {
                        Existencia.Nombre = Producto.Nombre;
                        Existencia.Precio = Producto.Precio;
                        Existencia.Stock = Producto.Stock;
                        Existencia.MInimoStock = Producto.MInimoStock;
                        _context.SaveChanges();

                        return StatusCode(200, "Producto Modificado");
                    }

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la venta", error = ex.Message });
            }

          

           

        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromBody] Estados Producto)
        {
            try
            {
                var Existencia = _context.Productos.Find(Producto.IdProducto);

                if (Existencia == null)
                {
                    return StatusCode(404, "Producto no encontrado");
                }
                else
                {
                    Existencia.Activo = Producto.Estado;
                    _context.SaveChanges();

                    return StatusCode(200, "Producto " + (Producto.Estado == 0 ? "Desativado" : "Activado"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la venta", error = ex.Message });
            }

           

        }

        public class Estados
        {
            [Required]
            public int Estado { get; set; }

            [Required]
            public int IdProducto { get; set; }
        }

    }
}
