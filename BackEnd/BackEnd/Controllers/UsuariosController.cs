using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BaseContext _context;

        public UsuariosController(BaseContext context)
        {
            _context = context;
        }



        [Authorize]
        [HttpGet]
        public IActionResult Seleccionar()
        {
            try
            {
                var UsuariosFind = _context.Usuario.ToList();

                if (UsuariosFind.Count == 0)
                {
                    var Usuario= new List<Usuarios> {

                    new Usuarios
                    {
                        Id = 0,
                        Nombre = "N/A",
                        Rol = 0,
                        Activo = 0
                    }


                };

                    return StatusCode(200, Usuario);
                }



                return StatusCode(200, UsuariosFind);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al Extraer usuarios", error = ex.Message });
            }
        }


        [Authorize]
        [HttpPost]
        public IActionResult Agregar([FromBody] Usuarios Usuario)
        {
            try
            {
                if (Usuario.Nombre == string.Empty || Usuario.Rol == 0)
                {
                    return StatusCode(400, "No se permiten campos en blanco");
                }
                else
                {
                    _context.Add(Usuario);
                    _context.SaveChanges();

                    return StatusCode(200, "Usuario Creado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear usuario", error = ex.Message });
            }

           

        }

        [Authorize]
        [HttpPut]
        public IActionResult Actualizar([FromBody] Actualizacion Ac)
        {
            try
            {
                if (Ac.IdUsuario == 0 || Ac.Estado == 0 || Ac.IdUSuaarioActual == 0)
                {
                    return StatusCode(400, "No se permiten campos en blanco");
                }
                else
                {
                    var Permiso = _context.Usuario.Find(Ac.IdUSuaarioActual);

                    if (Convert.ToInt32(Permiso.Rol) == 1 || Permiso == null)
                    {
                        var Existencia = _context.Usuario.Find(Ac.IdUsuario);

                        if (Existencia == null)
                        {
                            return StatusCode(404, "Usuario no encontrado");
                        }
                        else
                        {
                            Existencia.Nombre = Ac.Nombre;
                            Existencia.Rol = Ac.Rol;
                            Existencia.Activo = Ac.Estado;
                            _context.SaveChanges();


                            return StatusCode(200, "Usuario Modificado");
                        }
                    }
                    else
                    {
                        return StatusCode(200, "Usuario sin permisos");
                    }

                    
                    
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al modificar usuario", error = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Deshabilitar([FromBody] EstadosUsers Estado)
        {
            try
            {
                if (Estado.IdUsaurio == 0 || Estado.Estado == 0 || Estado.IdUsaurioActual == 0)
                {
                    return StatusCode(400, "No se permiten campos en blanco");
                }
                else
                {

                    var Permiso = _context.Usuario.Find(Estado.IdUsaurioActual);

                    if (Convert.ToInt32(Permiso.Rol) == 1 || Permiso == null)
                    {
                        var Existencia = _context.Usuario.Find(Estado.IdUsaurio);

                        if (Existencia == null)
                        {
                            return StatusCode(404, "Usuario no encontrado");
                        }
                        else
                        {
                            
                            Existencia.Activo = Estado.Estado;
                            _context.SaveChanges();


                            return StatusCode(200, "Usuario Modificado");
                        }
                    }
                    else
                    {
                        return StatusCode(200, "Usuario sin permisos");
                    }


                   

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al desactivar usuario", error = ex.Message });
            }
        }


        public class Actualizacion
        {
            [Required]
            public int Estado { get; set; }

            [Required]
            public int IdUsuario { get; set; }

            [Required]
            public string Nombre { get; set; }

            [Required]
            public int Rol { get; set; }

            [Required]
            public int IdUSuaarioActual { get; set; }
        }

        public class EstadosUsers
        {
            [Required]
            public int Estado { get; set; }

            [Required]
            public int IdUsaurio { get; set; }

            [Required]
            public int IdUsaurioActual { get; set; }
        }

        public class SeleccionUser
        {
            [Required]
            public string UsuarioSeleccion { get; set; }
            [Required]
            public string Pass { get; set; }
        }
    }
}
