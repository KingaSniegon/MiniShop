using System.Net.Http.Headers;
using MiniShop.Services;

namespace MiniShop.Models;

public class OrderService
{
    private List<Order> _orders = new List<Order>();
    private int _orderIdCounter = 1;

    public void ShowOrderDetails()
    {
        if (_orders.Count == 0)
        {
            Console.WriteLine("No orders found.");
            return;
        }

        foreach(var order in _orders)
        {
            Console.WriteLine($"\nOrder ID: {order.Id}");
            foreach(var item in order.OrderItems)
            {
                Console.WriteLine($"  {item.ProductName} - Quantity: {item.Quantity}, Unit Price: {item.UnitPrice} PLN, Total: {item.UnitPrice * item.Quantity} PLN");
            }
            Console.WriteLine($"Order total: {order.OrderItems.Sum(i => i.UnitPrice * i.Quantity)} PLN");
        }
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