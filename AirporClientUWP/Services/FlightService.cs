using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using AirporClientUWP.Models;
using System.Collections.ObjectModel;
using System.Text;

namespace AirporClientUWP.Services
{
    public class FlightService
    {
        const string SERVER_NAME = "http://localhost:57338";

        public async Task<ObservableCollection<Flight>> GetAllAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(SERVER_NAME + "/api/Flights").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<Flight>>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t get items from server");
        }

        public async Task<Flight> GetAsync(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(SERVER_NAME + $"/api/Flights/{id}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Flight>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t get item from server");
        }

        public async Task<Flight> AddAsync(Flight Flight)
        {
            var jsonBody = JsonConvert.SerializeObject(Flight);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(SERVER_NAME + $"/api/Flights/", content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Flight>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t add items to server");
        }

        public async Task<Flight> UpdateAsync(Flight Flight)
        {
            var jsonBody = JsonConvert.SerializeObject(Flight);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsync(SERVER_NAME + $"/api/Flights/{Flight.Id}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Flight>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t update items on the server");
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(SERVER_NAME + $"/api/Flights/{id}").ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Can`t delete item from server");
                }
            }
        }
    }
}
