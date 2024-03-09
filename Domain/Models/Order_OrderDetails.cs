namespace Domain.Models;

public class Order_OrderDetails
{
    public Order Order { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
