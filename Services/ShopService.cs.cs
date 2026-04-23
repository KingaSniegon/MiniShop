using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using MiniShop.Models;

namespace MiniShop.Services;

public class ShopService
{
    private List<Product> _products = new List<Product>(); // stan obiektu w klasie, przechowuje liste produktow. "_products" to pole klasy, jest prywatne i przechowuje liste produktow
    private List<BasketItem> _basket = new List<BasketItem>(); 
    private List<OrderItem> _order = new List<OrderItem>();
    
    public ShopService()
    {
        // dane startowe
        _products.Add(new Product { Id = 1, Name = "Laptop", Price = 3000, Stock = 5 });
        _products.Add(new Product { Id = 2, Name = "Mouse", Price = 100, Stock = 20 });
        _products.Add(new Product { Id = 3, Name = "Keyboard", Price = 200, Stock = 10 });
    }

    public void Checkout ()
    {
        var basketItems = GetBasketItems();
        if (basketItems.Count == 0)
        {
            Console.WriteLine("Basket is empty. Cannot proceed to checkout.");
            return;
        }

        foreach(var item in basketItems)
        {
            var orderItem = new OrderItem
            {
                ProductName = item.Product.Name,
                UnitPrice = item.Product.Price,
                Quantity = item.Quantity
            };
            _order.Add(orderItem);
        }
        
        _basket.Clear();
    }

    public void ShowOrderDetails()
    {
        Console.WriteLine("Order details:");
        foreach(var item in _order)
        {
            Console.WriteLine($"{item.ProductName} - Quantity: {item.Quantity} , Unit Price: {item.UnitPrice} PLN, Total: {item.UnitPrice * item.Quantity} PLN");
        }

        Console.WriteLine($"Total order cost: {_order.Sum(i => i.UnitPrice * i.Quantity)} PLN");
    }

    public List<BasketItem> GetBasketItems()
    {
        return new List<BasketItem>(_basket); // zwraca nowa liste, ktora jest kopia listy _basket, dzieki temu zewnetrzne modyfikacje tej listy nie beda mialy wplywu na oryginalna liste _basket w klasie ShopService
    }

    public void CalculateTotal()
    {
        decimal total = 0;

        foreach (var item in _basket)
        {
            total += item.Product.Price * item.Quantity;
        }
        Console.WriteLine($"Total: {total} PLN");
    }

    public void ShowBasket()
    {
        Console.WriteLine("Basket includes: ");

        if (_basket.Count == 0)
        {
            Console.WriteLine("Basket is empty.");
            return;
        }

        foreach (var item in _basket)
        {
            Console.WriteLine($"{item.Product.Name} - Quantity: {item.Quantity} , Price: {item.Product.Price * item.Quantity} PLN"); // interpolacja stringow, wyswietla nazwe produktu, ilosc i calkowity koszt dla tego produktu (cena * ilosc)   
        }
    }

    public void AddToBasket(int productId, int quantity)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId); // szuka produktu o podanym id w liscie produktow i zwraca go, jesli nie znajdzie to zwraca null

        if(product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        var basketItem = _basket.FirstOrDefault(b => b.Product.Id == productId); // szuka w koszyku przedmiotu, ktory jest tym samym produktem co znaleziony produkt

        if(basketItem == null)
        {
            _basket.Add(new BasketItem { Product = product, Quantity = quantity });
        }
        else
        {
            basketItem.Quantity += quantity;
        }
    }

    public void AddProducts()
    {
        while (true)
        {
        Console.WriteLine("Please enter product id which you want to buy: ");
        var Id = Console.ReadLine();

        if(!int.TryParse(Id, out int productId))
        {
            Console.WriteLine("You entered invalid product Id. Try again.");
            continue;

        } 

        if(!_products.Contains(_products.FirstOrDefault(p => p.Id == productId)))
        {
            Console.WriteLine("Product not found in the shop.");
            continue;
        }         

        Console.WriteLine("Please enter quantity: ");
        var quantity = Console.ReadLine();

        if(!int.TryParse(quantity, out int quantityInt) || quantityInt <= 0)
        {
            Console.WriteLine("You entered invalid quantity. Try again.");
            continue;
        }  

        AddToBasket(productId, quantityInt);

        Console.WriteLine("Do you want to add another product? (yes/no)");
        var answer = Console.ReadLine();
        if(answer?.Trim().ToLower() != "yes")
        {
            break;
        }
        }
    }
    public List<Product> GetAllProducts() // metoda zwracajaca liste wszystkich produktow
    {
        return _products;
    }
}
