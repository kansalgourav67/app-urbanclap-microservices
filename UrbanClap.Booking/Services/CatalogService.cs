using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace UrbanClap.BookingService.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public CatalogService(HttpClient client, IConfiguration configuration)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// </inheritdoc>
        public async Task<float> GetServiceCost(int serviceId)
        {
           var response = await _client.GetAsync($"{_configuration["ServiceCatalog:HostAddress"]}/api/servicecatalog/price/{serviceId}");
           var serviceCost = await HandleHttpResponse<float>(response);
           return serviceCost;
        }

        /// <summary>
        /// Deserialized http response and returns generic type object.
        /// </summary>
        private static async Task<T> HandleHttpResponse<T>(HttpResponseMessage httpResponse)
        {
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                throw new Exception("Someting wrng happens. Please try again");
            }
        }
    }
}
