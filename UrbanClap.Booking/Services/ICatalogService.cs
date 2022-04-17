using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrbanClap.BookingService.Services
{
    /// <summary>
    /// This class provides the latest cost of specific service by serviceId, like interior design service.
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// Returns the latest cost of service as per service id.
        /// </summary>
        Task<float> GetServiceCost(int serviceId);
    }
}
