using MiniShop.Models;
using MiniShop.Interfaces;

namespace MiniShop.Services;

public class BasketService : IBasketService
{
    private List<BasketItem> _basket = new List<BasketItem>(); 
    private readonly IProductService _productService;

    public BasketService(IProductService productService)
    {
        _productService = productService;
    }

//HELPERS
    private BasketItem? GetBasketItem(int productId)
    {
        var basketItem = _basket.FirstOrDefault(b => b.Product.Id == productId);

        return basketItem;
    }

    private int GetQuantityInBasket(int productId)
    {
        var quantityInBasket = GetBasketItem(productId)?.Quantity ?? 0;
        return quantityInBasket;
    }

    private int GetAvailableQuantity(int productId)
    {
        var product = _productService.GetProductById(productId);

        if (product == null)
        {
            throw new ArgumentException($"Product with id {productId} doesn't exist");
        }

        var availableQuantity = product.Stock - GetQuantityInBasket(productId);

        return  availableQuantity;
    }


// ADD 
    public List<BasketItem> AddToBasket(int productId, int quantity)
    {
        var product = _productService.GetProductById(productId);
        
        if (product == null)
            throw new ArgumentException($"Product with id {productId} doesn't exist");

        if(quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0");

        var basketItem = GetBasketItem(productId);
        var availableQuantity = GetAvailableQuantity(productId);

        var toAdd = Math.Min(quantity, availableQuantity); 


        if(basketItem == null)
        {
            _basket.Add(new BasketItem { Product = product, Quantity = toAdd });
        }
        else
        {
            basketItem.Quantity += toAdd;
        }
        return _basket;
    }      

//UPDATE
    public void UpdateQuantity(int productId, int requestedQuantity)
    {
        if (requestedQuantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0");

        var basketItem = GetBasketItem(productId);

        if (basketItem == null)
            throw new ArgumentException("Product doesn't exist in basket");

        var product = _productService.GetProductById(productId);

        if(product == null)
            throw new ArgumentException($"Product with id {productId} doesn't exist");


        var stock = basketItem.Product.Stock;

        if (requestedQuantity > stock)
        {
        basketItem.Quantity = stock;
        Console.WriteLine($"Only {stock} items available. Quantity adjusted.");
        }
        else
        {
        basketItem.Quantity = requestedQuantity;
        }
    } 

//REMOVE
    public void RemoveFromBasket (int productId)
    {
        var productToRemove = GetBasketItem(productId);

        if(productToRemove == null)
            return;

        _basket.Remove(productToRemove);
    }

//READ
    public List<BasketItem> GetBasketItems()
    {
        return _basket.ToList();
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

    public void ClearBasket()
    {
        _basket.Clear();
    }
}