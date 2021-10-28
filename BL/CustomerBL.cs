using System;
using Persistance;
using DAL;
using System.Collections.Generic;
namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL customerDAL;

        public CustomerBL()
        {
            customerDAL = new CustomerDAL();
        }

        public Customer GetCustomerByIdcard(string Id_Card)
        {
            return customerDAL.GetCustomerByIdcard(Id_Card);
        }

        public List<Customer> GetListCustomers()
        {
            return customerDAL.GetListCustomers();
        }



        public Customer GetCustomerById(int CustomerId)
        {
            return customerDAL.GetCustomerById(CustomerId);
        }
        public bool AddCustomer (Customer customer)
        {
            int rt = customerDAL.CreateCustomer(customer);
            if(rt > 0)
            {
                customer.CustomerId = rt;
                return true;
            }
            return false;
        }
    }
}