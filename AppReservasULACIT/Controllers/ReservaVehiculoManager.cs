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
    public class ReservaVehiculoManager
    {
        string Url = "http://localhost:49220/api/reservavehiculo/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<ReservaVehiculo>> ObtenerReservasVehiculos(string token)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(Url);

            return JsonConvert.DeserializeObject<IEnumerable<ReservaVehiculo>>(resultado);
        }

        public async Task<ReservaVehiculo> ObtenerReservaVehiculo(string token, string codigo)
        {
            HttpClient httpClient = GetClient(token);

            string resultado = await httpClient.GetStringAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<ReservaVehiculo>(resultado);
        }

        public async Task<ReservaVehiculo> Ingresar(ReservaVehiculo reservaVehiculo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PostAsync(Url,
                new StringContent(JsonConvert.SerializeObject(reservaVehiculo),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<ReservaVehiculo>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ReservaVehiculo> Actualizar(ReservaVehiculo reservaVehiculo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.PutAsync(Url,
                new StringContent(JsonConvert.SerializeObject(reservaVehiculo),
                Encoding.UTF8, "application/json"));

            return JsonConvert.DeserializeObject<ReservaVehiculo>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient httpClient = GetClient(token);

            var response = await httpClient.DeleteAsync(string.Concat(Url, codigo));

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}