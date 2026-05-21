using MiniShop.Models;
using MiniShop.Interfaces;

namespace MiniShop.Services;

public class OrderService : IOrderService
{
    private readonly List<Order> _orders = new List<Order>();
    private int _orderIdCounter = 1;
    
    public void AddOrder(Order order)
    {
        order.Id = _orderIdCounter++;
        _orders.Add(order);
    }

    public List<Order> GetOrders()
    {
        return _orders.ToList();
    }

    public Order CreateOrder(List<BasketItem> basketItems)
    {
        var order = new Order();

        foreach(var item in basketItems)
        {
            var orderItem = new OrderItem
            {
                ProductName = item.Product.Name ?? "Unknown Product Name",
                UnitPrice = item.Product.Price,
                Quantity = item.Quantity
            };
            order.OrderItems.Add(orderItem);
        }
        _orders.Add(order);

        return order;
    }

    public List<Order> GetAllOrders()
    {
        return _orders.ToList();
    }
}