using System;
using FluentAssertions;
using NSubstitute;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Domain.ProductAggregate;
using ToolsBazaar.Domain.Services;

namespace ToolsBazaar.Tests
{
	public class CustomerSpendingServiceTests
	{
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private CustomerSpendingService _customerSpendingService;
        public CustomerSpendingServiceTests()
		{
            _customerRepository = Substitute.For<ICustomerRepository>();
            _orderRepository = Substitute.For<IOrderRepository>();
            _productRepository = Substitute.For<IProductRepository>();

            _customerSpendingService = new CustomerSpendingService(
                _customerRepository,
                _orderRepository,
                _productRepository
            );
        }

        //Low on time - Should add more test cases 
        [Fact]
        public void GetTopCustomersBySpending_ShouldReturnTopCustomersOrderedBySpending()
        {
            // Arrange
            DateTime startDate = new DateTime(2015, 1, 1);
            DateTime endDate = new DateTime(2022, 12, 31);

            var customer1 = new Customer { Id = 1, Name = "Customer 1" };
            var customer2 = new Customer { Id = 2, Name = "Customer 2" };
            var customer3 = new Customer { Id = 3, Name = "Customer 3" };
            var customer4 = new Customer { Id = 4, Name = "Customer 4" };
            var customer5 = new Customer { Id = 5, Name = "Customer 5" };
            var customer6 = new Customer { Id = 6, Name = "Customer 6" };

            var product1 = new Product() { Id = 1, Name = "Product 1", Price = 10 };
            var product2 = new Product() { Id = 2, Name = "Product 2", Price = 15 };
            var product3 = new Product() { Id = 3, Name = "Product 3", Price = 20 };
            var product4 = new Product() { Id = 4, Name = "Product 4", Price = 5 };

            var order1 = new Order
            {
                Id = 1,
                Date = new DateTime(2022, 1, 1),
                Customer = customer1,
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = product1, Quantity = 2 },
                    new OrderItem { Product = product2, Quantity = 1 }
                }
            };

            var order2 = new Order
            {
                Id = 2,
                Date = new DateTime(2021, 6, 1),
                Customer = customer2,
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = product3, Quantity = 3 }
                }
            };

            var order3 = new Order
            {
                Id = 3,
                Date = new DateTime(2021, 6, 1),
                Customer = customer3,
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = product4, Quantity = 4 }
                }
            };

            var order4 = new Order
            {
                Id = 4,
                Date = new DateTime(2021, 6, 1),
                Customer = customer4,
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = product4, Quantity = 3 }
                }
            };

            var order5 = new Order
            {
                Id = 5,
                Date = new DateTime(2021, 6, 1),
                Customer = customer5,
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = product4, Quantity = 2 }
                }
            };

            var order6 = new Order
            {
                Id = 6,
                Date = new DateTime(2021, 6, 1),
                Customer = customer6,
                Items = new List<OrderItem>
                {
                    new OrderItem { Product = product3, Quantity = 1 }
                }
            };

            var orders = new List<Order> { order1, order2, order3, order4, order5, order6 };

            _orderRepository.GetAll().Returns(orders);
            _productRepository.GetProductById(Arg.Is<int>(product1.Id)).Returns(product1);
            _productRepository.GetProductById(Arg.Is<int>(product2.Id)).Returns(product2);
            _productRepository.GetProductById(Arg.Is<int>(product3.Id)).Returns(product3);
            _productRepository.GetProductById(Arg.Is<int>(product4.Id)).Returns(product4);

            // Act
            List<Customer> topCustomers = _customerSpendingService.GetTopCustomersBySpending();

            // Assert
            topCustomers.Should().HaveCount(5);
            topCustomers[0].Should().Be(customer2);
            topCustomers[1].Should().Be(customer1);
        }
    }
}

