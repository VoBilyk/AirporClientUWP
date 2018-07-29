using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using AirporClientUWP.Models;
using System.Collections.ObjectModel;
using System.Text;

namespace AirporClientUWP.Services
{
    public class AeroplaneTypeService
    {
        const string SERVER_NAME = "http://localhost:57338";

        public async Task<ObservableCollection<AeroplaneType>> GetAllAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(SERVER_NAME + "/api/AeroplaneTypes").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ObservableCollection<AeroplaneType>>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t get items from server");
        }

        public async Task<AeroplaneType> GetAsync(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(SERVER_NAME + $"/api/AeroplaneTypes/{id}").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AeroplaneType>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t get item from server");
        }

        public async Task<AeroplaneType> AddAsync(AeroplaneType AeroplaneType)
        {
            var jsonBody = JsonConvert.SerializeObject(AeroplaneType);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(SERVER_NAME + $"/api/AeroplaneTypes/", content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AeroplaneType>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t add items to server");
        }

        public async Task<AeroplaneType> UpdateAsync(AeroplaneType AeroplaneType)
        {
            var jsonBody = JsonConvert.SerializeObject(AeroplaneType);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsync(SERVER_NAME + $"/api/AeroplaneTypes/{AeroplaneType.Id}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string contentResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AeroplaneType>(contentResponse);
                }
            }

            throw new InvalidOperationException("Can`t update items on the server");
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(SERVER_NAME + $"/api/AeroplaneTypes/{id}").ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Can`t delete item from server");
                }
            }
        }
    }
}
