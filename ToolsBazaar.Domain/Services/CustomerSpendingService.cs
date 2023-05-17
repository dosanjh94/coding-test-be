using System;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Domain.ProductAggregate;

namespace ToolsBazaar.Domain.Services
{
	public class CustomerSpendingService : ICustomerSpendingService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        public CustomerSpendingService(ICustomerRepository customerRepository,IOrderRepository orderRepository,IProductRepository productRepository)
		{
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public List<Customer> GetTopCustomersBySpending()
        {
            //Normally these would be passed in 
            DateTime startDate = new DateTime(2015, 1, 1);
            DateTime endDate = new DateTime(2022, 12, 31);

            List<Order> orders = _orderRepository.GetAll()
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToList();

            var customerSpending = new Dictionary<Customer, decimal>();

            foreach (Order order in orders)
            {
                decimal orderTotal = order.Items.Sum(item => item.Quantity * _productRepository.GetProductById(item.Product.Id).Price);

                if (!customerSpending.ContainsKey(order.Customer))
                    customerSpending[order.Customer] = 0;

                customerSpending[order.Customer] += orderTotal;
            }

            List<Customer> topCustomers = customerSpending
                .OrderByDescending(kv => kv.Value)
                .Take(5)
                .Select(kv => kv.Key)
                .ToList();

            return topCustomers;
        }
    }
}

