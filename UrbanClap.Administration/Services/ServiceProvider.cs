
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace UrbanClap.AdministrationService.Services
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public ServiceProvider(HttpClient client, IConfiguration configuration)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// </inheritdoc>
        public async Task<List<int>> GetAvailableProviders(string serviceType, string pincode)
        {
            var response = await _client.GetAsync($"{_configuration["ServiceProvider:HostAddress"]}/api/serviceprovider/{serviceType}/{pincode}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<int>>(content);
            }
            else
            {
                throw new Exception("Servcie Unavailable. Please try after sometime");
            }
        }
    }
}
