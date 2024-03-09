using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class MenuService : IService<Menu>
{
    private readonly DapperContext _context;

    public MenuService()
    {
        _context = new DapperContext();
    }

    public async Task<string> Add(Menu some)
    {
        var sql = @"insert into Menu(FoodName,FoodPrice,AffordableFood) values(@FoodName,@FoodPrice,@AffordableFood)";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Added successfully!";
    }

    public async Task<string> Delete(int id)
    {
        var sql = @"delete from Menu where FoodId=@FoodId";
        await _context.Connection().ExecuteAsync(sql, new { MenuId = id });
        return $"Delete successfully!";
    }

    public async Task<List<Menu>> Get()
    {
        var sql = @"select * from Menu";
        var result = await _context.Connection().QueryAsync<Menu>(sql);
        return result.ToList();
    }

    public async Task<string> Update(Menu some)
    {
        var sql = @"update Menu set FoodName=@FoodName,FoodPrice=@FoodPrice,AffordableFood=@AffordableFood where FoodId=@FoodId ";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Update successfully!";
    }
}
