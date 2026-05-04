using MiniShop.Models;

namespace MiniShop.Services;

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
        var product = _productService.GetAllProducts().FirstOrDefault(p => p.Id == productId);
        
        if (product == null)
        {
            throw new ArgumentException($"Product with id {productId} doesn't exist");
        }

        var basketItem = _basket.FirstOrDefault(b => b.Product.Id == productId);

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
        return new List<BasketItem>(_basket);
    }

    public void ClearBasket()
    {
        _basket.Clear();
    }
}