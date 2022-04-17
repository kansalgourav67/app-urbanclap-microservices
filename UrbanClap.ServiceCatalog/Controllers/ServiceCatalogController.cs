using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UrbanClap.ServiceCatalog.Repositories;
using UrbanClap.ServiceCatalog.Repository;

namespace UrbanClap.ServiceCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCatalogController : ControllerBase
    {
        private readonly IServiceCatalogRepository _serviceCatalogRepository;

        public ServiceCatalogController(IServiceCatalogRepository serviceCatalogRepository)
        {
            _serviceCatalogRepository = serviceCatalogRepository ?? throw new ArgumentNullException(nameof(serviceCatalogRepository));
        }

        /// <summary>
        /// Returns all the services provided by the portal.
        /// </summary>
        [HttpGet]
        public List<Service> Get()
        {
            return _serviceCatalogRepository.GetAllService();
        }

        /// <summary>
        /// Add or resister new service to the portal.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] Service serviceCatalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_serviceCatalogRepository.AddService(serviceCatalog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update service.
        /// </summary>
        [HttpPut]
        public IActionResult Put([FromBody] Service serviceCatalog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return Ok(_serviceCatalogRepository.UpdateService(serviceCatalog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Returns latest price of service by service id.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        [HttpGet("price/{serviceId}")]
        public IActionResult GetPriceByServiceType(int serviceId)
        {
            try
            {
                if(!_serviceCatalogRepository.ServiceExists(serviceId))
                {
                    return NotFound($"Service with id {serviceId} does not exists");
                }

                return Ok(_serviceCatalogRepository.GetPriceByServiceId(serviceId));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
