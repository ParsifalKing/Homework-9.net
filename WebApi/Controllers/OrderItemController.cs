using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController
{
    private readonly OrderItemService _orderItemService;

    public OrderItemController()
    {
        _orderItemService = new OrderItemService();
    }

    // [HttpPost("add-orderItem")]
    // public async Task<string> Add(OrderItem some)
    // {
    //     return await _orderItemService.Add(some);
    // }

    // [HttpDelete("delete-orderItem")]
    // public async Task<string> Delete(int id)
    // {
    //     return await _orderItemService.Delete(id);
    // }

    [HttpGet("get-orderItems")]
    public async Task<List<OrderItem>> Get()
    {
        return await _orderItemService.Get();
    }

    // [HttpPut("update-orderItem")]
    // public async Task<string> Update(OrderItem some)
    // {
    //     return await _orderItemService.Update(some);
    // }
}
