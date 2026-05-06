using Xunit;
using MiniShop.Models;
using MiniShop.Services;

public class OrderServiceTests
{
    [Fact]
    public void AddOrder_ShouldStoreOrder()
    {
        // ARRANGE
        var orderService = new OrderService();
        var order = new Order
        {
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Apple",
                    UnitPrice = 10m,
                    Quantity = 2
                }
            },
            Id = 1,
        };

        // ACT
        orderService.AddOrder(order);
        var result = orderService.GetOrders();

        // ASSERT
        Assert.Single(result);
        Assert.Contains(result, o => o.Id == 1 && o.OrderItems[0].ProductName == "Apple" && o.OrderItems[0].Quantity == 2);
    }

    [Fact]
    public void AddOrder_ShouldIncrementIdForEachNewOrder()
    {
        // ARRANGE
        var orderService = new OrderService();
        var firstOrder = new Order
        {
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Apple",
                    UnitPrice = 10m,
                    Quantity = 2
                }
            },
        };

        var secondOrder = new Order
        {
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Banana",
                    UnitPrice = 15m,
                    Quantity = 1
                }
            },
        };

        // ACT
        orderService.AddOrder(firstOrder);
        orderService.AddOrder(secondOrder);
        var result = orderService.GetOrders();

        // ASSERT
        Assert.Equal(2, result.Count);
        Assert.Contains(result, o => o.Id == 1 && o.OrderItems[0].ProductName == "Apple" && o.OrderItems[0].Quantity == 2);
        Assert.Contains(result, o => o.Id == 2 && o.OrderItems[0].ProductName == "Banana" && o.OrderItems[0].Quantity == 1);
    }  
}

    