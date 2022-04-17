using Microsoft.AspNetCore.Mvc;
using System;
using UrbanClap.CustomerService.Repositories;

namespace UrbanClap.CustomerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Returns all customers registered on the portal.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userRepository.GetCustomers());
        }

        /// <summary>
        /// Returns single customer as per customer id.
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        [HttpGet("{customerid}")]
        public IActionResult Get(int customerid)
        {
            return Ok(userRepository.GetCustomerById(customerid));
        }

        /// <summary>
        /// Register new customer to the portal.
        /// </summary>
        [HttpPost]
        public IActionResult Post(Models.Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(userRepository.AddCustomer(customer));
        }
    }
}
