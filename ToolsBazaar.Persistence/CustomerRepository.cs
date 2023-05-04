using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.Exceptions;

namespace ToolsBazaar.Persistence;

public class CustomerRepository : ICustomerRepository
{
    public IEnumerable<Customer> GetAll() => DataSet.AllCustomers;

    public IEnumerable<Customer> GetTop(int pageNumber, int pageSize, DateTime startDate, DateTime endDate) => DataSet
            .AllOrders
            .Where(o => o.Date.Date >= startDate && o.Date.Date <= endDate)
            .GroupBy(o => o.Customer)
            .Select(c => new
            {
                Customer = c.Key,
                Total = c.SelectMany(o => o.Items).Sum(p => p.Quantity * p.Price)
            })
            .OrderByDescending(c => c.Total)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .Select(c => c.Customer);

    public void UpdateCustomerName(int customerId, string name)
    {
        var customer = DataSet.AllCustomers.FirstOrDefault(c => c.Id == customerId);

        if (customer == null)
        {
            throw new EntityNotFoundException("Customer not found!");
        }

        customer.UpdateName(name);
    }
}