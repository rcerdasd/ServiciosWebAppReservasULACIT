using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AppReservasULACIT.Models;
using Newtonsoft.Json;

namespace AppReservasULACIT.Controllers
{
    public class UsuarioManager
    {
        string UrlAuthenticate = "http://localhost:49220/api/login/authenticate/";
        string UrlRegister = "http://localhost:49220/api/login/register/";
        string Url = "http://localhost:49220/api/usuario/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<Usuario> Validar(string username, string password)
        {
            LoginRequest loginRequest = new LoginRequest() { Username = username, Password = password };

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(UrlAuthenticate,
                new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json")
                );

            return JsonConvert.DeserializeObject<Usuario>(await response.Content.ReadAsStringAsync());
        } 

        public async Task<Usuario> Registrar(Usuario usuario)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.PostAsync(UrlRegister,
                new StringContent(JsonConvert.SerializeObject(usuario),Encoding.UTF8,"application/json"));

            return JsonConvert.DeserializeObject<Usuario>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Usuario>>(resultado);
        }

        public async Task<Usuario> ObtenerUsuario(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<Usuario>(resultado);
        }
    }
}