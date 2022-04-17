
using System.Collections.Generic;

namespace UrbanClap.CustomerService.Repositories
{
    public interface IUserRepository
    {
        int AddCustomer(Models.Customer customer);

        List<Models.Customer> GetCustomers();

        Models.Customer GetCustomerById(int id);
    }
}
