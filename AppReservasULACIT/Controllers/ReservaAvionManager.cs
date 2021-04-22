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
    public class ReservaAvionManager
    {
        string Url = "http://localhost:49220/api/reservaavion/";
        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<ReservaAvion>> ObtenerReservasAvion(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<ReservaAvion>>(resultado);
        }
        public async Task<ReservaAvion> ObtenerReservaAvion(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<ReservaAvion>(resultado);
        }
        public async Task<ReservaAvion> Ingresar(ReservaAvion reservaAvion, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(reservaAvion),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<ReservaAvion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ReservaAvion> Actualizar(ReservaAvion reservaAvion, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(reservaAvion),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<ReservaAvion>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }

    }
}