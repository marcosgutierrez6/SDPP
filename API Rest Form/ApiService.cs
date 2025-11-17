using System;
using System.Collections.Generic; // Necesario para List<>
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_Rest_Form
{
    // --- MODELOS DE DATOS ---
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }

    // Nuevo modelo para los productos
    public class Producto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }

    // --- SERVICIO ---
    public class ApiService
    {
        private static readonly HttpClient _client = new HttpClient();

        private static readonly JsonSerializerOptions _opciones = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Método 1: Login
        public async Task<string> LoginAsync(string usuario, string password)
        {
            try
            {
                string url = "https://fakestoreapi.com/auth/login";
                var loginData = new LoginRequest { Username = usuario, Password = password };

                string json = JsonSerializer.Serialize(loginData, _opciones);
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");

                var respuesta = await _client.PostAsync(url, contenido);

                if (respuesta.IsSuccessStatusCode)
                {
                    string jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
                    var tokenObj = JsonSerializer.Deserialize<LoginResponse>(jsonRespuesta, _opciones);
                    return tokenObj.Token;
                }
                return null;
            }
            catch (Exception) { return null; }
        }

        // Método 2: Obtener Productos (NUEVO)
        public async Task<List<Producto>> GetProductosAsync()
        {
            try
            {
                string url = "https://fakestoreapi.com/products";

                // Hacemos la petición GET
                var respuesta = await _client.GetAsync(url);

                if (respuesta.IsSuccessStatusCode)
                {
                    string json = await respuesta.Content.ReadAsStringAsync();
                    // Deserializamos la lista de productos
                    return JsonSerializer.Deserialize<List<Producto>>(json, _opciones);
                }
                return null;
            }
            catch (Exception) { return null; }
        }
    }
}