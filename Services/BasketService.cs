using System.Net.Http.Headers;
using MiniShop.Services;

namespace MiniShop.Models;

public class BasketService
{
    private List<BasketItem> _basket = new List<BasketItem>(); 
    private readonly ProductService _productService;

    public BasketService(ProductService productService)
    {
        _productService = productService;
    }

    public void AddToBasket(int productId, int quantity)
    {
        var product = _productService.GetAllProducts().FirstOrDefault(p => p.Id == productId); // szuka produktu o podanym id w liscie produktow zwracanej przez metode GetAllProducts() z serwisu ProductService
        
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

    public void CalculateTotal()
    {
        decimal total = 0;

        foreach (var item in _basket)
        {
            total += item.Product.Price * item.Quantity;
        }
        Console.WriteLine($"Total: {total} PLN");
    }        
    
    public List<BasketItem> GetBasketItems()
    {
        return new List<BasketItem>(_basket); // zwraca nowa liste, ktora jest kopia listy _basket, dzieki temu zewnetrzne modyfikacje tej listy nie beda mialy wplywu na oryginalna liste _basket w klasie ShopService
    }

    public void ClearBasket()
    {
        _basket.Clear();
    }
}