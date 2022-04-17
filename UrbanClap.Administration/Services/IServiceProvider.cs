using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanClap.AdministrationService.Services
{
    /// <summary>
    /// This class will returns the list of all service providers who are available to cater the customer request.
    /// </summary>
    public interface IServiceProvider
    {
        /// <summary>
        /// Returns available service providers.
        /// </summary>
        Task<List<int>> GetAvailableProviders(string serviceType, string pincode);
    }
}
