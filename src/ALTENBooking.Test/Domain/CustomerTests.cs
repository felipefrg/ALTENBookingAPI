using ALTENBooking.Domain;
using ALTENBooking.Domain.Interfaces;
using ALTENBooking.Domain.Models;
using ALTENBooking.Domain.Services;
using ALTENBooking.Utils;

namespace ALTENBooking.Test.Domain
{
    public class CustomerTests
    {        
        CustomerService _customerService;
        IRepository<Customer> _customerRepositoryMock;

        public CustomerTests()
        {
            _customerRepositoryMock = new RepositoryMock<Customer>();            
            _customerService = new CustomerService(_customerRepositoryMock);
        }

        [Fact]
        public void GivenCreatingCustomer_WhenAllInfoDataOK_ThenCreateCustomerSuccessfully()
        {
            //arrange
            Customer customer = new Customer
            {
                DateOfBirth = new DateTime(1990, 01, 01)
                ,
                Document = "001-01"
                ,
                Email = "felipe.goncalves@alten.com"
                ,
                FirstName = "Felipe"
                ,
                LastName = "Gonçalves"
            };

            //act
            var result = _customerService.Create(customer);
            int customerCount = _customerRepositoryMock.GetAll().Count();

            //assert            
            Assert.False(result.HasError);
            Assert.Equal(1, customerCount);
        }

        [Fact]
        public void GivenCreatingCustomer_WhenEmailAlreadyExists_ThenReturnCreateCustomerError()
        {
            //arrange
            Customer customer = new Customer
            {
                DateOfBirth = new DateTime(1990, 01, 01)
                ,
                Document = "001-01"
                ,
                Email = "felipe.goncalves@alten.com"
                ,
                FirstName = "Felipe"
                ,
                LastName = "Gonçalves"
            };

            Customer customer2 = new Customer
            {
                DateOfBirth = new DateTime(1990, 01, 01)
               ,
                Document = "002-02"
               ,
                Email = "felipe.goncalves@alten.com"
               ,
                FirstName = "John"
               ,
                LastName = "Wick"
            };
            _customerRepositoryMock.Add(customer);

            //act
            var result = _customerService.Create(customer2);
            int customerCount = _customerRepositoryMock.GetAll().Count();

            //assert            
            Assert.Equal(1, customerCount);
            Assert.True(result.HasError);
            Assert.Equal(StringMessages.CustomerEmailAlreadyExists, result.Message);
        }

        [Fact]
        public void GivenGettingCustomer_WhenEmailAlreadyExists_ThenReturnCustomer()
        {            
            //arrange
            Customer customer = new Customer
            {
                DateOfBirth = new DateTime(1990, 01, 01)
                ,
                Document = "001-01"
                ,
                Email = "felipe.goncalves@alten.com"
                ,
                FirstName = "Felipe"
                ,
                LastName = "Gonçalves"
            };          
            _customerRepositoryMock.Add(customer);

            //act
            var result = _customerService.GetByEmail(customer.Email);            

            //assert            
            Assert.NotNull(result);            
            Assert.Equal(customer.Email, result.Email);
        }

        [Fact]
        public void GivenGettingCustomer_WhenEmailNotExists_ThenNotReturnCustomer()
        {
            //arrange            
            string email = "john.wick@alten.com";

            //act
            var result = _customerService.GetByEmail(email);

            //assert            
            Assert.Null(result);            
        }
    }
}
