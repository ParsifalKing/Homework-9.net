using Dapper;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class CustomerService : IService<Customer>
{
    private readonly DapperContext _context;

    public CustomerService()
    {
        _context = new DapperContext();
    }

    public async Task<string> Add(Customer some)
    {
        var sql = @"insert into Customers(FullName,Email) values(@FullName,@Email)";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Added successfully!";
    }

    public async Task<string> Delete(int id)
    {
        var sql = @"delete from Customers where CustomerId=@CustomerId";
        await _context.Connection().ExecuteAsync(sql, new { CustomerId = id });
        return $"Delete successfully!";
    }

    public async Task<List<Customer>> Get()
    {
        var sql = @"select * from Customers";
        var result = await _context.Connection().QueryAsync<Customer>(sql);
        return result.ToList();
    }

    public async Task<string> Update(Customer some)
    {
        var sql = @"update Customers set FullName=@FullName,Email=@Email where CustomerId=@CustomerId ";
        await _context.Connection().ExecuteAsync(sql, some);
        return $"Update successfully!";
    }


    public async Task<CustomerOrders> GetCustomerOrdersById(int customerId)
    {
        var sql = @"select * from Customers where CustomerId=@CustomerId;
        select * from Orders where CustomerId=@CustomerId;
        ";

        using (var multiple = _context.Connection().QueryMultiple(sql, new { CustomerId = customerId }))
        {
            var customerOrders = new CustomerOrders();
            customerOrders.Customer = multiple.ReadFirst<Customer>();
            customerOrders.Orders = multiple.Read<Order>().ToList();
            return customerOrders;
        }
    }

}
