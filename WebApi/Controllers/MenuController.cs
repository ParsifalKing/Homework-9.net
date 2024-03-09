using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController
{
    private readonly MenuService _menuService;

    public MenuController()
    {
        _menuService = new MenuService();
    }

    [HttpPost("add-food")]
    public async Task<string> Add(Menu some)
    {
        return await _menuService.Add(some);
    }

    [HttpDelete("delete-food")]
    public async Task<string> Delete(int id)
    {
        return await _menuService.Delete(id);
    }

    [HttpGet("get-foods")]
    public async Task<List<Menu>> Get()
    {
        return await _menuService.Get();
    }

    [HttpPut("update-food")]
    public async Task<string> Update(Menu some)
    {
        return await _menuService.Update(some);
    }
}
