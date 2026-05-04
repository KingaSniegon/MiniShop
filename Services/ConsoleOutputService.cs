using MiniShop.Models;

namespace MiniShop.Services;

public class ConsoleOutputService
{

    public void ShowProducts(List<Product> products)
    {
        Console.WriteLine("Available products:");

        foreach (var p in products)
        {
            Console.WriteLine($"ID: {p.Id}, Name: {p.Name}, Price: {p.Price} PLN");
        }
    }

    public void ShowBasket(List<BasketItem> items, decimal total)
    {
        if (items.Count == 0)
        {
            Console.WriteLine("Basket is empty.");
            return;
        }

        Console.WriteLine("Basket:");

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Product.Name} x{item.Quantity} = {item.Product.Price * item.Quantity} PLN");
        }

        Console.WriteLine($"Total: {total} PLN");
    }

    public void ShowOrders(List<Order> orders)
    {
        foreach (var order in orders)
        {
            Console.WriteLine($"\nOrder ID: {order.Id}");

            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($"{item.ProductName} x{item.Quantity} = {item.UnitPrice * item.Quantity}");
            }

            Console.WriteLine($"Total: {order.OrderItems.Sum(i => i.UnitPrice * i.Quantity)}");
        }
    }
}