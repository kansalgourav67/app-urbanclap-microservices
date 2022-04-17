using System;
using System.Collections.Generic;
using UrbanClap.CustomerService.Models;

namespace UrbanClap.CustomerService.Repositories
{
    public class UserRepository : IUserRepository
    {
        public static List<Customer> customerList = new List<Customer>
        {
            new Customer{CustomerId=1, Name="ABCD", MobileNumber="9080706050", EmailId="abcd@gmail.com"},
            new Customer{CustomerId=2, Name="WXYZ", MobileNumber="9877888888", EmailId="wxyz@outlook.com"}
        };

        public int AddCustomer(Customer customer)
        {
            customer.CustomerId = new Random().Next();
            customerList.Add(customer);
            return customer.CustomerId;
        }

        public Customer GetCustomerById(int id)
        {
            return customerList.Find(c => c.CustomerId == id);
        }

        public List<Customer> GetCustomers()
        {
            return customerList;
        }
    }
}
