using System;
using ToolsBazaar.Domain.CustomerAggregate;

namespace ToolsBazaar.Domain
{
	public interface ICustomerSpendingService
	{
        List<Customer> GetTopCustomersBySpending();
    }
}

