using SistemaInventario.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SistemaInventario.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public LoginResponse Login(string nombreUsuario, string passUsuario)
        {
            try
            {
               
                var request = new LoginRequest
                {
                    NombreUsuario = nombreUsuario,
                    PassUsuario = passUsuario
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync("https://localhost:7237/api/Login", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var responseContent = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch
            {
                return null;
            }
        }

        public List<Producto> ObtenerProductos()
        {
            try
            {

                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTTOKEN");
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                else
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7237/api/Productos");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


                    var response = _httpClient.SendAsync(request).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        return new List<Producto>();
                    }

                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    return JsonSerializer.Deserialize<List<Producto>>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                
            }
            catch
            {
                return new List<Producto>();
            }
        }

        public async Task<string?>  CrearProducto(Producto producto)
        {
            try
            {

                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTTOKEN");
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                else
                {
                    var json = JsonSerializer.Serialize(producto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7237/api/Productos")
                    {
                        Content = content
                    };

                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                        

                    return await response.Content.ReadAsStringAsync();
                }




               
            }
            catch
            {
                return null;
            }
        }

        public async Task<string?> EditarProducto(Producto producto)
        {
            try
            {

                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTTOKEN");
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                else
                {
                    var json = JsonSerializer.Serialize(producto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7237/api/Productos")
                    {
                        Content = content
                    };

                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }


                    return await response.Content.ReadAsStringAsync();
                }



            }
            catch
            {
                return null;
            }
        }

        public async Task<string?> EliminarProducto(int id, int estado)
        {
            try
            {
                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTTOKEN");
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                else
                {
                    var requestObj = new
                    {
                        estado = estado,
                        idProducto = id
                    };


                    var json = JsonSerializer.Serialize(requestObj);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");


                    var request = new HttpRequestMessage(HttpMethod.Delete, "https://localhost:7237/api/Productos")
                    {
                        Content = content
                    };


                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


                    var response = await _httpClient.SendAsync(request);


                    return await response.Content.ReadAsStringAsync();
                }
            }
            
            catch
            {
                return null;
            }
        }

        public async Task<string?> CrearVenta(VentaRequest venta)
        {
            try
            {


                var token = _httpContextAccessor.HttpContext.Session.GetString("JWTTOKEN");
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                else
                {
                    var json = JsonSerializer.Serialize(venta);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7237/api/Productos")
                    {
                        Content = content
                    };

                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


                    var response = await _httpClient.SendAsync(request);

                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }


                    return await response.Content.ReadAsStringAsync();
                }


                
            }
            catch
            {
                return null;
            }
        }
    }
}
