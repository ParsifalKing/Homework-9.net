namespace Domain.Models;

public class Order
{
    public int OrderId { get; set; }
    public string OrderInfo { get; set; }
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public bool Status { get; set; }

}
