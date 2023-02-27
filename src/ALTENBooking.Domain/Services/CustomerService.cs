using ALTENBooking.Domain.Interfaces;
using ALTENBooking.Domain.Models;
using ALTENBooking.Domain.Queries;
using ALTENBooking.Utils;

namespace ALTENBooking.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Result Create(Customer customer)
        {
            Result result = CheckProperties(customer);
            if (result.HasError)
                return result;

            var customerExist = GetByEmail(customer.Email);
            if(customerExist != null)
            {
                result.HasError = true;
                result.Message = StringMessages.CustomerEmailAlreadyExists;
                return result;
            }

            _customerRepository.Add(customer);

            return result;
        }

        public Customer GetByEmail(string email)
        {
            return _customerRepository.GetAll(CustomerQuery.GetByEmail(email)).FirstOrDefault();
        }

        public Customer Get(Guid id)
        {
            return _customerRepository.GetById(id);
        }

        Result CheckProperties(Customer customer)
        {
            Result result = new Result();
            if (string.IsNullOrWhiteSpace(customer.FirstName))
            {
                result.HasError = true;
                result.Message = String.Format(StringMessages.PropertyEmpty, nameof(customer.FirstName));
            }
            if (string.IsNullOrWhiteSpace(customer.LastName))
            {
                result.HasError = true;
                result.Message = String.Format(StringMessages.PropertyEmpty, nameof(customer.LastName));
            }
            if (!customer.DateOfBirth.HasValue)
            {
                result.HasError = true;
                result.Message = String.Format(StringMessages.PropertyEmpty, nameof(customer.DateOfBirth));
            }
            if (string.IsNullOrWhiteSpace(customer.Document))
            {
                result.HasError = true;
                result.Message = String.Format(StringMessages.PropertyEmpty, nameof(customer.Document));
            }
            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                result.HasError = true;
                result.Message = String.Format(StringMessages.PropertyEmpty, nameof(customer.Email));
            }

            return result;
        }

    }
}
