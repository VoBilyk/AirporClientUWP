using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using AirporClientUWP.Models;
using System.Collections.ObjectModel;
using System.Text;

namespace AirporClientUWP.Services
{
    public class StewardessService
    {
        const string SERVER_NAME = "http://localhost:57338";

        public async Task<ObservableCollection<Stewardess>> GetAllAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(SERVER_NAME + "/api/Stewardesses").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<Stewardess>>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t get items from server");
        }

        public async Task<Stewardess> GetAsync(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(SERVER_NAME + $"/api/Stewardesses/{id}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Stewardess>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t get item from server");
        }

        public async Task<Stewardess> AddAsync(Stewardess Stewardess)
        {
            var jsonBody = JsonConvert.SerializeObject(Stewardess);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(SERVER_NAME + $"/api/Stewardesses/", content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Stewardess>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t add items to server");
        }

        public async Task<Stewardess> UpdateAsync(Stewardess Stewardess)
        {
            var jsonBody = JsonConvert.SerializeObject(Stewardess);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsync(SERVER_NAME + $"/api/Stewardesses/{Stewardess.Id}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Stewardess>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t update items on the server");
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(SERVER_NAME + $"/api/Stewardesses/{id}").ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Can`t delete item from server");
                }
            }
        }
    }
}
