using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class OrderItemService
{
    private readonly DapperContext _context;

    public OrderItemService()
    {
        _context = new DapperContext();
    }

    public async Task<string> Add(OrderItem some)
    {
        var sql1 = @"select * from Menu where FoodId=@FoodId";
        var food = _context.Connection().QueryFirst<Menu>(sql1, new { FoodId = some.FoodId });
        some.UnitPrice = food.FoodPrice;
        if (food.AffordableFood == false)
        {
            return $"Now your food with foodId {some.FoodId} is not affordable, excuse us";
        }

        var sql2 = @"insert into OrderItems(OrderId,FoodId,Quantity,UnitPrice) values(@OrderId,@FoodId,@Quantity,@UnitPrice)";
        await _context.Connection().ExecuteAsync(sql2, some);
        return $"Added successfully!";
    }

    public async Task<string> Delete(int id)
    {
        var sql = @"delete from OrderItems where OrderItemId=@OrderItemId";
        await _context.Connection().ExecuteAsync(sql, new { OrderItemId = id });
        return $"Delete successfully!";
    }

    public async Task<List<OrderItem>> Get()
    {
        var sql = @"select * from OrderItems";
        var result = await _context.Connection().QueryAsync<OrderItem>(sql);
        return result.ToList();
    }

    public async Task<string> Update(OrderItem some)
    {
        var sql = @"update OrderItems set OrderId=@OrderId,FoodId=@FoodId,Quantity=@Quantity,UnitPrice=@UnitPrice where OrderItemId=@OrderItemId ";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Update successfully!";
    }
}
