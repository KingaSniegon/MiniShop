using System.Net.Http.Headers;
using MiniShop.Services;

namespace MiniShop.Models;

public class OrderService
{
    private List<OrderItem> _order = new List<OrderItem>();

     public void ShowOrderDetails()
    {
        Console.WriteLine("Order details:");
        foreach(var item in _order)
        {
            Console.WriteLine($"{item.ProductName} - Quantity: {item.Quantity} , Unit Price: {item.UnitPrice} PLN, Total: {item.UnitPrice * item.Quantity} PLN");
        }

        Console.WriteLine($"Total order cost: {_order.Sum(i => i.UnitPrice * i.Quantity)} PLN");
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        _order.Add(orderItem);
    }

    
}