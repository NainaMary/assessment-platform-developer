using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Services
{
    public interface ICustomerServiceCmd
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
    public class CustomerServiceCmd : ICustomerServiceCmd
    {
        private readonly ICustomerRepositoryCmd customerRepository;

        public CustomerServiceCmd(ICustomerRepositoryCmd customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public void AddCustomer(Customer customer)
        {
            customerRepository.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerRepository.Update(customer);
        }

        public void DeleteCustomer(int id)
        {
            customerRepository.Delete(id);
        }
    }
    public interface ICustomerServiceQry
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
    }
    public class CustomerServiceQry : ICustomerServiceQry
    {
        private readonly ICustomerRepositoryQry customerRepository;

        public CustomerServiceQry(ICustomerRepositoryQry customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return customerRepository.GetAll();
        }

        public Customer GetCustomer(int id)
        {
            return customerRepository.Get(id);
        }
    }
    //public interface ICustomerService
    //{
    //    IEnumerable<Customer> GetAllCustomers();
    //    Customer GetCustomer(int id);
    //    void AddCustomer(Customer customer);
    //    void UpdateCustomer(Customer customer);
    //    void DeleteCustomer(int id);
    //}

    //public class CustomerService : ICustomerService
    //{
    //    private readonly ICustomerRepository customerRepository;

    //    public CustomerService(ICustomerRepository customerRepository)
    //    {
    //        this.customerRepository = customerRepository;
    //    }

    //    public IEnumerable<Customer> GetAllCustomers()
    //    {
    //        return customerRepository.GetAll();
    //    }

    //    public Customer GetCustomer(int id)
    //    {
    //        return customerRepository.Get(id);
    //    }

    //    public void AddCustomer(Customer customer)
    //    {
    //        customerRepository.Add(customer);
    //    }

    //    public void UpdateCustomer(Customer customer)
    //    {
    //        customerRepository.Update(customer);
    //    }

    //    public void DeleteCustomer(int id)
    //    {
    //        customerRepository.Delete(id);
    //    }
    //}

}