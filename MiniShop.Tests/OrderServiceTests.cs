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
        Assert.Contains(result, o => o.Id == 1 && o.OrderItems[0].ProductName == "Apple" && o.OrderItems[0].Quantity == 2 && o.OrderItems[0].UnitPrice == 10m);
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
        Assert.Contains(result, o => o.Id == 1 && o.OrderItems[0].ProductName == "Apple" && o.OrderItems[0].Quantity == 2 && o.OrderItems[0].UnitPrice == 10m);
        Assert.Contains(result, o => o.Id == 2 && o.OrderItems[0].ProductName == "Banana" && o.OrderItems[0].Quantity == 1 && o.OrderItems[0].UnitPrice == 15m);
    }  
    [Fact]
    public void AddOrder_ShouldOverrideExistingId()
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
            Id = 999
        };

        // ACT
        orderService.AddOrder(order);
        var result = orderService.GetOrders();

        // ASSERT
        Assert.Equal(1, result.Count);
        Assert.Contains(result, o => o.Id == 1 && o.OrderItems[0].ProductName == "Apple" && o.OrderItems[0].Quantity == 2 && o.OrderItems[0].UnitPrice == 10m);
    }       

    [Fact]
    public void GetOrders_ShouldReturnCopy()
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

        var thirdOrder = new Order
        {
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Pear",
                    UnitPrice = 5m,
                    Quantity = 1
                },
                new OrderItem
                {
                    ProductName = "Peach",
                    UnitPrice = 5m,
                    Quantity = 4
                }
            },
        };        

        // ACT
        orderService.AddOrder(firstOrder);
        orderService.AddOrder(secondOrder);
        orderService.AddOrder(thirdOrder);
        var ordersList = orderService.GetOrders(); // pobieramy nowa liste
        ordersList.Clear();

        var result = orderService.GetOrders();

        // ASSERT
        Assert.Equal(3, result.Count);
        Assert.Contains(result, o => o.Id == 1 && o.OrderItems[0].ProductName == "Apple" && o.OrderItems[0].Quantity == 2 && o.OrderItems[0].UnitPrice == 10m);
        Assert.Contains(result, o => o.Id == 2 && o.OrderItems[0].ProductName == "Banana" && o.OrderItems[0].Quantity == 1 && o.OrderItems[0].UnitPrice == 15m);
        Assert.Contains(result, o => o.Id == 3 && o.OrderItems[0].ProductName == "Pear" && o.OrderItems[0].Quantity == 1 && o.OrderItems[0].UnitPrice == 5m && o.OrderItems[1].ProductName == "Peach" && o.OrderItems[1].Quantity == 4 && o.OrderItems[1].UnitPrice == 5m);
    } 

    [Fact]
    public void GetOrders_ShouldReturnAllOrders()
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

        var thirdOrder = new Order
        {
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Pear",
                    UnitPrice = 5m,
                    Quantity = 1
                }
            },
        };        

        // ACT
        orderService.AddOrder(firstOrder);
        orderService.AddOrder(secondOrder);
        orderService.AddOrder(thirdOrder);
        var result = orderService.GetOrders();

        // ASSERT
        Assert.Equal(3, result.Count);
        Assert.Contains(result, o => o.Id == 1 && o.OrderItems[0].ProductName == "Apple" && o.OrderItems[0].Quantity == 2 && o.OrderItems[0].UnitPrice == 10m);
        Assert.Contains(result, o => o.Id == 2 && o.OrderItems[0].ProductName == "Banana" && o.OrderItems[0].Quantity == 1 && o.OrderItems[0].UnitPrice == 15m);
        Assert.Contains(result, o => o.Id == 3 && o.OrderItems[0].ProductName == "Pear" && o.OrderItems[0].Quantity == 1 && o.OrderItems[0].UnitPrice == 5m);
    }      

    [Fact]
    public void GetOrders_ShouldReturnEmptyList_WhenNoOrders()
    {
        // ARRANGE
        var orderService = new OrderService();
        
        // ACT
        var result = orderService.GetOrders();

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(0, result.Count);
    }
}

    