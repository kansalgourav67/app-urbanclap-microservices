using System;

namespace UrbanClap.ServiceCatalog.Repository
{
    /// <summary>
    /// Provides all the services that are portal provides like Interior Designer, Hair cut, etc
    /// </summary>
    public class Service
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public float Cost { get; set; }
    }
}
