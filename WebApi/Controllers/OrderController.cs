using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController
{
    private readonly OrderService _orderService;

    public OrderController()
    {
        _orderService = new OrderService();
    }

    [HttpPost("add-order")]
    public async Task<string> Add([FromQuery] Order some, List<OrderItem> orderItems)
    {
        return await _orderService.Add(some, orderItems);
    }

    [HttpDelete("delete-order")]
    public async Task<string> Delete(int id)
    {
        return await _orderService.Delete(id);
    }

    [HttpGet("get-orders")]
    public async Task<List<Order>> Get()
    {
        return await _orderService.Get();
    }

    [HttpPut("update-order")]
    public async Task<string> Update(Order some)
    {
        return await _orderService.Update(some);
    }

    [HttpGet("get-orders-customer-with-orderItems")]
    public async Task<List<Order_Customer_OrderItems>> GetOrdersCustomerWithOrderItems()
    {
        return await _orderService.GetOrdersCustomerWithOrderItems();
    }

    [HttpGet("get-order-with-orderItems-by-id")]
    public async Task<Order_OrderDetails> GetOrderWithOrderItemsById(int orderId)
    {
        return await _orderService.GetOrderWithOrderItemsById(orderId);
    }
}
