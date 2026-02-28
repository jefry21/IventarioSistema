using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Servicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly BaseContext _context;

        private readonly JWTService _jwtService;
        public LoginController(BaseContext context, JWTService jwtservice)
        {
            _context = context;
            _jwtService = jwtservice;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SeleccionUsuario([FromBody] DatosUser User)
        {
            try
            {
                if (User.PassUsuario == string.Empty || User.NombreUsuario == string.Empty || User.PassUsuario == null || User.NombreUsuario == null)
                {
                    return StatusCode(400, "Datos en blanco ");
                }
                else
                {
                    var existencia = _context.Usuario.FirstOrDefault(x => x.Nombre == User.NombreUsuario && x.Contrasena == User.PassUsuario);

                    if (existencia == null)
                    {
                        return StatusCode(400, "Usuario no encontrado ");
                    }
                    else
                    {
                        if (existencia.Rol == 1)
                        {
                            string[] roles = new[] { "1", "2" };
                            var token = _jwtService.GenerarToken(User.NombreUsuario, roles);

                            var Resultado = (new Respuestas
                            {
                                Rol = existencia.Rol,
                                Nombre = existencia.Nombre,
                                Token = token
                            });

                            return StatusCode(200, Resultado);
                        }
                        else
                        {
                            string[] roles = new[] { "2" };
                            var token = _jwtService.GenerarToken(User.NombreUsuario, roles);

                            var Resultado = (new Respuestas
                            {
                                Rol = existencia.Rol,
                                Nombre = existencia.Nombre,
                                Token = token
                            });

                            return StatusCode(200, Resultado);
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al selecionar usuario", error = ex.Message });
            }
        }

      
        public class DatosUser
        {
            [Required]
            public string NombreUsuario { get; set; }
            [Required]
            public string PassUsuario { get; set; }
        }

        public class Respuestas
        {
            public int Rol { get; set; }
            public string Nombre { get; set; }

            public string Token { get; set; }
        }
    }
}
