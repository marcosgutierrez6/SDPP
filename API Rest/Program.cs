using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_Rest
{

    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        private static readonly JsonSerializerOptions opcionesJson = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        static async Task Main(string[] args)
        {
            Console.WriteLine("--- Tienda FakeStoreAPI ---");

            try
            {
                // EJEMPLO 1
                await ObtenerProducto(1);
                Console.WriteLine("\n--------------------------------\n");

                // EJEMPLO 2
                await CrearProducto();
                Console.WriteLine("\n--------------------------------\n");

                // EJEMPLO 3
                await GetUsers();
                Console.WriteLine("\n--------------------------------\n");

                await Login();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico: {ex.Message}");
            }

            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        static async Task Login()
        {
            Console.WriteLine("4. Iniciando sesión...");

            // Requerimiento específico:
            Console.WriteLine("username(david_r) password(3478*#54)");

            string url = "https://fakestoreapi.com/auth/login";

            // Preparamos los datos
            var loginData = new Auth
            {
                Username = "david_r",
                Password = "3478*#54"
            };

            // Serializamos (C# -> JSON)
            string json = JsonSerializer.Serialize(loginData, opcionesJson);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            // Enviamos POST
            var respuesta = await client.PostAsync(url, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                string respuestaJson = await respuesta.Content.ReadAsStringAsync();

                // Deserializamos (JSON -> C#)
                var tokenObj = JsonSerializer.Deserialize<AuthResponse>(respuestaJson, opcionesJson);

                Console.WriteLine($"[LOGIN EXITOSO]");
                Console.WriteLine($"Token recibido: {tokenObj.Token}");
            }
            else
            {
                Console.WriteLine($"[Error Login] Código: {respuesta.StatusCode}");
            }
        }

        // ... Tus otros métodos (ObtenerProducto, CrearProducto, GetUsers) se mantienen igual abajo ...
        static async Task ObtenerProducto(int id)
        {
            Console.WriteLine($"1. Obteniendo producto ID: {id}...");
            string url = $"https://fakestoreapi.com/products/{id}";
            var respuesta = await client.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            string json = await respuesta.Content.ReadAsStringAsync();
            var producto = JsonSerializer.Deserialize<Producto>(json, opcionesJson);
            Console.WriteLine($"[GET OK] {producto.Title} - ${producto.Price}");
        }

        static async Task CrearProducto()
        {
            Console.WriteLine("2. Creando nuevo producto...");
            string url = "https://fakestoreapi.com/products";
            var nuevoProducto = new Producto
            {
                Title = "Zapatillas de Prueba",
                Price = 29.99m,
                Description = "Zapatillas para correr",
                Image = "https://i.pravatar.cc",
                Category = "electronic"
            };
            string json = JsonSerializer.Serialize(nuevoProducto, opcionesJson);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");
            var respuesta = await client.PostAsync(url, contenido);
            if (respuesta.IsSuccessStatusCode)
            {
                string respuestaJson = await respuesta.Content.ReadAsStringAsync();
                var productoCreado = JsonSerializer.Deserialize<Producto>(respuestaJson, opcionesJson);
                Console.WriteLine($"[POST OK] ID nuevo: {productoCreado.Id}");
            }
        }

        static async Task GetUsers()
        {
            Console.WriteLine($"1. Obteniendo Usuarios...");
            string url = $"https://fakestoreapi.com/users";
            var respuesta = await client.GetAsync(url);
            respuesta.EnsureSuccessStatusCode();
            string json = await respuesta.Content.ReadAsStringAsync();
            var listaUsuarios = JsonSerializer.Deserialize<List<Users>>(json, opcionesJson);
            Console.WriteLine($"[GET OK] Se recibieron {listaUsuarios.Count} usuarios.");

            // Solo muestro el primero para no llenar la pantalla en este ejemplo
            if (listaUsuarios.Count > 0)
            {
                Console.WriteLine($"Usuario 1: {listaUsuarios[0].username}");
            }
        }
    }
}