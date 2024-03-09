namespace Domain.Models;

public class Order_Customer_OrderItems
{
    public Order Order { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
