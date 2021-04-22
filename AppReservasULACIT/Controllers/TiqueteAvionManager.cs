using AppReservasULACIT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AppReservasULACIT.Controllers
{
    public class TiqueteAvionManager
    {
        string Url = "http://localhost:49220/api/tiqueteavion/";
        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Tiquete_Avion>> ObtenerTiquetesAvion(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<Tiquete_Avion>>(resultado);
        }
        public async Task<Tiquete_Avion> ObtenerTiqueteAvion(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<Tiquete_Avion>(resultado);
        }
        public async Task<Tiquete_Avion> Ingresar(Tiquete_Avion tiquete_Avion, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(tiquete_Avion),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Tiquete_Avion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Tiquete_Avion> Actualizar(Tiquete_Avion tiquete_Avion, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(tiquete_Avion),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<Tiquete_Avion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }

    }
}