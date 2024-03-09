using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController()
    {
        _customerService = new CustomerService();
    }

    [HttpPost("add-customer")]
    public async Task<string> Add(Customer some)
    {
        return await _customerService.Add(some);
    }

    [HttpDelete("delete-customer")]
    public async Task<string> Delete(int id)
    {
        return await _customerService.Delete(id);
    }

    [HttpGet("get-customers")]
    public async Task<List<Customer>> Get()
    {
        return await _customerService.Get();
    }

    [HttpPut("update-customer")]
    public async Task<string> Update(Customer some)
    {
        return await _customerService.Update(some);
    }

    [HttpGet("get-customer-orders-by-id{customerId}")]
    public async Task<CustomerOrders> GetCustomerOrdersById(int customerId)
    {
        return await _customerService.GetCustomerOrdersById(customerId);
    }
}
