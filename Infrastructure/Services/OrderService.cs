using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class OrderService
{
    private readonly DapperContext _context;

    public OrderService()
    {
        _context = new DapperContext();
    }

    public async Task<string> Add(Order some, List<OrderItem> orderItems)
    {
        var sql1 = @"insert into Orders(OrderInfo,CustomerId,TotalAmount,Status) values(@OrderInfo,@CustomerId,@TotalAmount,@Status)";
        await _context.Connection().ExecuteAsync(sql1, some);

        var sql2 = @"select Max(OrderId) from Orders";
        var orderId = _context.Connection().QueryFirst<int>(sql2);
        foreach (var item in orderItems)
        {
            item.OrderId = orderId;
            var orderItem = new OrderItemService();
            await orderItem.Add(item);
        }

        var sql3 = @"select * from OrderItems where OrderId=@OrderId";
        var _orderItems = _context.Connection().Query<OrderItem>(sql3, new { OrderId = orderId });
        foreach (var item in _orderItems)
        {
            var totalAmount = item.UnitPrice * item.Quantity;
            some.TotalAmount += totalAmount;
        }
        var sql4 = @"update Orders set TotalAmount=@TotalAmount where OrderId=@OrderId";
        _context.Connection().Execute(sql4, new { TotalAmount = some.TotalAmount, OrderId = orderId });
        return $"Added successfully!";
    }

    public async Task<string> Delete(int id)
    {
        var sql = @"delete from Orders where OrderId=@OrderId";
        await _context.Connection().ExecuteAsync(sql, new { OrderId = id });
        return $"Delete successfully!";
    }

    public async Task<List<Order>> Get()
    {
        var sql = @"select * from Orders";
        var result = await _context.Connection().QueryAsync<Order>(sql);
        return result.ToList();
    }

    public async Task<string> Update(Order some)
    {
        var sql = @"update Orders set OrderInfo=@OrderInfo,CustomerId=@CustomerId,TotalAmount=@TotalAmount,Status=@Status where OrderId=@OrderId ";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Update successfully!";
    }

    // 2
    public async Task<List<Order_Customer_OrderItems>> GetOrdersCustomerWithOrderItems()
    {
        var sql1 = @"select * from Orders;";
        var result = await _context.Connection().QueryAsync<Order>(sql1);
        var orders = result.ToList();
        var sql2 = @"select * from Orders where OrderId=@OrderId;";
        var sql3 = @"select * from Customers where CustomerId = @CustomerId;";
        var sql4 = @"select * from OrderItems where OrderId = @OrderId";

        var ordersWithOrderItems = new List<Order_Customer_OrderItems>();
        foreach (var item in orders)
        {
            var ordersAndOrderItems = new Order_Customer_OrderItems();
            ordersAndOrderItems.Order = _context.Connection().QueryFirst<Order>(sql2, new { OrderId = item.OrderId });
            ordersAndOrderItems.Customer = _context.Connection().QueryFirst<Customer>(sql3, new { CustomerId = item.CustomerId });
            var orderItems = _context.Connection().Query<OrderItem>(sql4, new { OrderId = item.OrderId });
            var orderItems2 = new List<OrderItem>();
            foreach (var orderItem in orderItems)
            {
                orderItems2.Add(orderItem);
            }
            ordersAndOrderItems.OrderItems = orderItems2;
            ordersWithOrderItems.Add(ordersAndOrderItems);
        }
        return ordersWithOrderItems;
    }


    public async Task<Order_OrderDetails> GetOrderWithOrderItemsById(int orderId)
    {
        var sql = @"select * from Orders where OrderId=@OrderId;
        select * from OrderItems where OrderId=@OrderId;
        ";

        using (var multiple = _context.Connection().QueryMultiple(sql, new { OrderId = orderId }))
        {
            var customerOrders = new Order_OrderDetails();
            customerOrders.Order = multiple.ReadFirst<Order>();
            customerOrders.OrderItems = multiple.Read<OrderItem>().ToList();
            return customerOrders;
        }
    }
}
