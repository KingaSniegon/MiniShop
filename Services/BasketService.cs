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

    public List<BasketItem> AddToBasket(int productId, int quantity)
    {
        var product = _productService.GetAllProducts().FirstOrDefault(p => p.Id == productId); // szuka produktu o podanym Id w liscie produktow dostepnych w sklepie
        
        if (product == null)
        {
            throw new ArgumentException($"Product with id {productId} doesn't exist");
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
        return _basket;
    }      

    public decimal CalculateTotal()
    {
        decimal total = 0;

        foreach (var item in _basket)
        {
            total += item.Product.Price * item.Quantity;
        }
        return total;
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