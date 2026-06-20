// Exercise 11: Implementing Dependency Injection
// Name: Nilanjan Pradhan
// Assignment: Cognizant Digital Nurture 5.0 - Week 1 (Design Patterns & Principles)

using System;

namespace DependencyInjectionExample
{
    // Repository Interface
    public interface ICustomerRepository
    {
        string FindCustomerById(int customerId);
    }

    // Concrete Repository
    public class CustomerRepositoryImpl : ICustomerRepository
    {
        public string FindCustomerById(int customerId)
        {
            return $"Customer Found -> ID: {customerId}, Name: Rahul Sharma";
        }
    }

    // Service Class
    public class CustomerService
    {
        private readonly ICustomerRepository customerRepository;

        // Constructor Injection
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void GetCustomer(int customerId)
        {
            string customer = customerRepository.FindCustomerById(customerId);
            Console.WriteLine(customer);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dependency Injection Example - Customer Management System\n");

            ICustomerRepository repository =
                new CustomerRepositoryImpl();

            CustomerService customerService =
                new CustomerService(repository);

            customerService.GetCustomer(101);
        }
    }
}