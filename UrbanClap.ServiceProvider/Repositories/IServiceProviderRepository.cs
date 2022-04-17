using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanClap.ServiceProvider.Repositories
{
    public interface IServiceProviderRepository
    {
        /// <summary>
        /// Add new service provider.
        /// </summary>
        int AddServiceProvider(Provider.ServiceProvider serviceProvider);

        /// <summary>
        /// Returns all service providers.
        /// </summary>
        /// <returns></returns>
        List<Provider.ServiceProvider> GetServiceProviders();

        /// <summary>
        /// Return service provider by unique id.
        /// </summary>
        Provider.ServiceProvider GetProviderById(int providerId);

        /// <summary>
        /// Returns id of all service providers by pincode and role.
        /// </summary>
        List<int> GetProvidersByAvailability(string pincode, string role);

        /// <summary>
        /// Returns true if service provider exists.
        /// </summary>
        bool IsServiceProviderExists(int id);
    }
}
