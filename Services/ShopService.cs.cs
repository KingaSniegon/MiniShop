using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using MiniShop.Models;

namespace MiniShop.Services;

public class ShopService
{
    private readonly BasketService _basketService;// trzymane sa dane przekazane przez konstruktor.
    private readonly OrderService _orderService;
    private readonly ProductService _productService;


    public ShopService(BasketService basketService, OrderService orderService, ProductService productService)
    {
        _basketService = basketService;
        _orderService = orderService;
        _productService = productService;


        // dane startowe
        _productService.AddProduct(new Product { Id = 1, Name = "Laptop", Price = 3000, Stock = 5 });
        _productService.AddProduct(new Product { Id = 2, Name = "Mouse", Price = 100, Stock = 20 });
        _productService.AddProduct(new Product { Id = 3, Name = "Keyboard", Price = 200, Stock = 10 });
    }

     public void AddProductsToBasket()
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

        if(!_productService.GetAllProducts().Contains(_productService.GetAllProducts().FirstOrDefault(p => p.Id == productId)))
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

        _basketService.AddToBasket(productId, quantityInt);

        Console.WriteLine("Do you want to add another product? (yes/no)");
        var answer = Console.ReadLine();
        if(answer?.Trim().ToLower() != "yes")
        {
            break;
        }
        }
    }

    public void Checkout ()
    {
        var basketItems = _basketService.GetBasketItems();
        if (basketItems.Count == 0)
        {
            Console.WriteLine("Basket is empty. Cannot proceed to checkout.");
            return;
        }
        
        var order = new Order();
        foreach(var item in basketItems)
        {

            var orderItem = new OrderItem
            {
                ProductName = item.Product.Name,
                UnitPrice = item.Product.Price,
                Quantity = item.Quantity
            };
            order.OrderItems.Add(orderItem);
        }
        _orderService.AddOrder(order);
    }
}
