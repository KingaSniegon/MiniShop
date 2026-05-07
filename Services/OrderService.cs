using MiniShop.Models;

namespace MiniShop.Services;

public class OrderService
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
}