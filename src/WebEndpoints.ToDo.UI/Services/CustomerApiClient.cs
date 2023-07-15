using ToDo.Domain.CustomerAggregate.Dto;
using System.Net.Http.Json;

namespace WebEndpoints.ToDo.UI.Services
{
    public class CustomerApiClient
    {
        private readonly HttpClient httpClient;

        public CustomerApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<CustomerItemDto>?> GetCustomersAsync()
        {
            
                var response = await httpClient.GetAsync("/api/customer");

                if (response.IsSuccessStatusCode)
                {
                    var customers = await response.Content.ReadFromJsonAsync<List<CustomerItemDto>>();
                    return customers;
                }

            return new List<CustomerItemDto>();
        }
    }
}
