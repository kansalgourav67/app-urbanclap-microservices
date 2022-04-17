using System;
using System.Collections.Generic;
using System.Linq;

namespace UrbanClap.ServiceProvider.Repositories
{
    public class ServiceProviderRepository : IServiceProviderRepository
    {
        public static List<Provider.ServiceProvider> _providers = new List<Provider.ServiceProvider>
        {
            new Provider.ServiceProvider{ Id = 1, Name="Provider 1", Role="Interior Design", MobileNumber="8180889981", Address="East Delhi", PinCode="101010"},
            new Provider.ServiceProvider{ Id = 2, Name="Provider 2", Role="Electrician", MobileNumber="8280889981", Address="West Delhi", PinCode="202020"},
            new Provider.ServiceProvider{ Id = 3, Name="Provider 3", Role="Interior Design", MobileNumber="8380889981", Address="South Delhi", PinCode="303030"},
            new Provider.ServiceProvider{ Id = 4, Name="Provider 4", Role="Hair Cut", MobileNumber="9840889981", Address="North Delhi", PinCode="4040404"},
            new Provider.ServiceProvider{ Id = 5, Name="Provider 5", Role="Electrician", MobileNumber="9850889981", Address="East Delhi", PinCode="505050"},
            new Provider.ServiceProvider{ Id = 6, Name="Provider 6", Role="Electrician", MobileNumber="860889981", Address=" West Delhi", PinCode="606060"},
        };

        /// </inheritdoc>
        public int AddServiceProvider(Provider.ServiceProvider serviceProvider)
        {
            serviceProvider.Id = GenerateUniqueId();
            _providers.Add(serviceProvider);
            return serviceProvider.Id;
        }

        /// </inheritdoc>
        public List<Provider.ServiceProvider> GetServiceProviders()
        {
            return _providers;
        }

        /// </inheritdoc>
        public Provider.ServiceProvider GetProviderById(int providerId)
        {
            return _providers.Find(p => p.Id == providerId);
        }

        /// </inheritdoc>
        public List<int> GetProvidersByAvailability(string pincode, string role)
        {
           return _providers.FindAll(p => p.PinCode == pincode && p.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                .Select(sp => sp.Id)
                .ToList();
        }

        /// <summary>
        /// inheritdoc
        /// </summary>
        public bool IsServiceProviderExists(int id)
        {
            return _providers.Find(p => p.Id == id) != null;
        }

        private static int GenerateUniqueId()
        {
            Random random = new Random();
            return random.Next();
        }
    }
}
