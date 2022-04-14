using System;
using System.Collections.Generic;

namespace UrbanClap.ServiceCatalog.Repository
{
    /// <summary>
    /// Service Catalog Repository stores in-memory data (in list).
    /// </summary>
    public class ServiceCatalogRepository
    {
        private static List<Service> _services = new List<Service>
        {
            new Service{ServiceId = 1, ServiceName = "Electrician", Cost = 1100.00f },
            new Service{ServiceId = 2, ServiceName = "Hair Cut", Cost = 800.00f },
            new Service{ServiceId = 3, ServiceName = "Interior Design", Cost = 5000.00f }
        };

        /// <summary>
        /// Add new service.
        /// </summary>
        public static int AddService(Service serviceCatalog)
        {
            // set unique id by calculating the count of service list.
            serviceCatalog.ServiceId = _services.Count + 1;
            _services.Add(serviceCatalog);
            return serviceCatalog.ServiceId;   
        }

        /// <summary>
        /// Get All Services.
        /// </summary>
        public static List<Service> GetAllService()
        {
            return _services;
        }

        /// <summary>
        /// Update service.
        /// </summary>
        public static int UpdateService(Service serviceCatalog)
        {
            Service service = _services.Find(service => service.ServiceId == serviceCatalog.ServiceId);
            if (service == null) 
                throw new Exception($"Service with {serviceCatalog.ServiceId} not present");

            service.ServiceName = serviceCatalog.ServiceName;
            service.Cost = serviceCatalog.Cost;

            return service.ServiceId;
        }
    }
}
