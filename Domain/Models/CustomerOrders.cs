namespace Domain.Models;

public class CustomerOrders
{
    public Customer Customer { get; set; }
    public List<Order> Orders { get; set; }
}
