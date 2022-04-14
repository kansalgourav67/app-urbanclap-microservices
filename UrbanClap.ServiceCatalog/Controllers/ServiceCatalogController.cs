using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UrbanClap.ServiceCatalog.Repository;

namespace UrbanClap.ServiceCatalog.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServiceCatalogController : ControllerBase
    {
        public ServiceCatalogController()
        {
        }

        /// <summary>
        /// Returns all the services provided by the portal.
        /// </summary>
        [HttpGet]
        public List<Service> Get()
        {
            return ServiceCatalogRepository.GetAllService();
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
                return Ok(ServiceCatalogRepository.AddService(serviceCatalog));
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
                return Ok(ServiceCatalogRepository.UpdateService(serviceCatalog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
