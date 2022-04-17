using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanClap.ServiceCatalog.Repository;

namespace UrbanClap.ServiceCatalog.Repositories
{
    public interface IServiceCatalogRepository
    {
        /// <summary>
        /// Add new service.
        /// </summary>
        int AddService(Service serviceCatalog);

        /// <summary>
        /// Get All Services.
        /// </summary>
         List<Service> GetAllService();

        /// <summary>
        /// Update service.
        /// </summary>
        int UpdateService(Service serviceCatalog);

        /// <summary>
        /// Returns price of service id.
        /// </summary>
        float GetPriceByServiceId(int id);

        /// <summary>
        /// Returns true if the service exists.
        /// </summary>
        bool ServiceExists(int id);
    }
}
