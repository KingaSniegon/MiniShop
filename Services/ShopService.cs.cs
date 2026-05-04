using MiniShop.Models;

namespace MiniShop.Services;

public class ShopService
{
    private readonly BasketService _basketService;
    private readonly OrderService _orderService;
    private readonly ConsoleInputService _consoleInputService;

    public ShopService(BasketService basketService, OrderService orderService, ProductService productService, ConsoleInputService consoleInputService)
    {
        _basketService = basketService;
        _orderService = orderService;
        _consoleInputService = consoleInputService;

    }

     public void AddProductsToBasket()
    {
         while (true)
    {
        int productId = _consoleInputService.ReadProductId();
        int quantity = _consoleInputService.ReadQuantity();

        _basketService.AddToBasket(productId, quantity);

        if (!_consoleInputService.AskToContinue())
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
                ProductName = item.Product.Name ?? "Unknown Product Name",
                UnitPrice = item.Product.Price,
                Quantity = item.Quantity
            };
            order.OrderItems.Add(orderItem);
        }
        _orderService.AddOrder(order);
    }
}
