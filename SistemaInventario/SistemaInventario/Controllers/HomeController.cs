using Microsoft.AspNetCore.Mvc;
using SistemaInventario.Models;
using SistemaInventario.Services;
using System.Diagnostics;

namespace SistemaInventario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;

        public HomeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string password)
        {
            try
            {
                var result = _apiService.Login(usuario, password);

                if (result == null)
                {
                    ViewBag.Error = "Usuario o contraseña incorrectos";
                    return View();
                }

                HttpContext.Session.SetString("Nombre", result.Nombre);
                HttpContext.Session.SetInt32("Rol", result.Rol);
                HttpContext.Session.SetString("JWTTOKEN", result.Token);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Index()
        {
            try
            {

                if (HttpContext.Session.GetInt32("Rol") == null)
                    return RedirectToAction("Login");

                return View();
            }
            catch
            {
                return RedirectToAction("Error");
            }

        }

        public IActionResult Venta()
        {
            try
            {

                if (HttpContext.Session.GetInt32("Rol") == null)
                    return RedirectToAction("Login");

                var productos = _apiService.ObtenerProductos();
                return View(productos);
            }
            catch
            {
                return RedirectToAction("Error");
            }


        }

        [HttpPost]
        public IActionResult ProcesarVenta(string clienteVenta, string SubtotalVentat, string TotalVentat, string productosJson)
        {
            try
            {
                if (HttpContext.Session.GetInt32("Rol") == null)
                    return RedirectToAction("Login");

                var detalles = System.Text.Json.JsonSerializer.Deserialize<List<VentaDetalleRequest>>(productosJson);

                var venta = new VentaRequest
                {
                    ClienteVenta = clienteVenta,
                    SubtotalVenta = Convert.ToDecimal(SubtotalVentat),
                    TotalVenta = Convert.ToDecimal(TotalVentat),
                    Productos = detalles
                };

                var resultado = _apiService.CrearVenta(venta);

                if (resultado != null)
                {
                    ViewBag.Mensaje = "Venta registrada exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al registrar la venta";
                }

                var productos = _apiService.ObtenerProductos();
                return View("Venta", productos);
            }
            catch
            {
                return RedirectToAction("Error");
            }


        }

        public IActionResult Productos()
        {
            try
            {

                if (HttpContext.Session.GetInt32("Rol") == null)
                    return RedirectToAction("Login");

                var productos = _apiService.ObtenerProductos();
                return View(productos);
            }
            catch
            {
                return RedirectToAction("Error");
            }


        }

        [HttpPost]
        public IActionResult CrearProducto(string nombre, decimal precio, int stock, int minimoStock, int activo)
        {
            try
            {
                if (HttpContext.Session.GetInt32("Rol") == null)
                    return RedirectToAction("Login");

                var producto = new Producto
                {
                    Id = 0,
                    Nombre = nombre,
                    Precio = precio,
                    Stock = stock,
                    MInimoStock = minimoStock,
                    Activo = activo
                };

                var resultado = _apiService.CrearProducto(producto);
                if (resultado != null)
                {
                    ViewBag.Mensaje = "Producto creado exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al crear producto";
                }
                   

                var productos = _apiService.ObtenerProductos();
                return View("Productos", productos);
            }

            catch
            {
                return RedirectToAction("Error");
            }

        }


            [HttpPost]
            public IActionResult EditarProducto(int id, string nombre, decimal precio, int stock, int minimoStock, int activo)
            {
                try
                {
                    if (HttpContext.Session.GetInt32("Rol") == null)
                        return RedirectToAction("Login");

                    var producto = new Producto
                    {
                        Id = id,
                        Nombre = nombre,
                        Precio = precio,
                        Stock = stock,
                        MInimoStock = minimoStock,
                        Activo = activo
                    };

                    var resultado = _apiService.EditarProducto(producto);

                if (resultado != null)
                {
                    ViewBag.Mensaje = "Producto modificado exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Error al modificado producto";
                }

                    var productos = _apiService.ObtenerProductos();
                    return View("Productos", productos);
                }
                catch
                {
                    return RedirectToAction("Error");
                }


            } 

        public IActionResult EliminarProducto(int id, int estado)
        {
            try
            {
                if (HttpContext.Session.GetInt32("Rol") == null)
                    return RedirectToAction("Login");

                var resultado = _apiService.EliminarProducto(id, estado);

                if (resultado != null)
                {
                    ViewBag.Mensaje = "Producto actualizado";
                }
                else
                {
                    ViewBag.Mensaje = "Error al actualizar producto";
                }

                return RedirectToAction("Productos");
            }
            catch
            {
                return RedirectToAction("Error");
            }




        }


        public IActionResult ReporteBajoStock()
        {
            var productos = _apiService.ObtenerProductos();
            return View("ReporteBajoStock", productos);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
