using ToolsBazaar.Domain.CustomerAggregate;

namespace ToolsBazaar.Persistence;

public class CustomerRepository : ICustomerRepository
{
    public IEnumerable<Customer> GetAll() => DataSet.AllCustomers;

    //Low on time - Should add unit test for this methods logic
    public void UpdateCustomerName(int customerId, string name)
    {
        var customer = DataSet.AllCustomers.FirstOrDefault(c => c.Id == customerId);

        if (customer == null)
        {
            throw new KeyNotFoundException($"No customer found for #{customerId}");
        }

        customer.UpdateName(name);
    }
}