using System.Net.Http.Headers;
using MiniShop.Services;

namespace MiniShop.Models;

public class OrderService
{
    private List<Order> _orders = new List<Order>();
    private int _orderIdCounter = 1;

    public List<Order> ShowOrderDetails()
    {
        return _orders;
    }
    
    public void AddOrder(Order order)
    {
        order.Id = _orderIdCounter++;
        _orders.Add(order);
    }

    public List<Order> GetOrders()
    {
        return _orders;
    }
}