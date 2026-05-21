using MiniShop.Interfaces;
using MiniShop.Models;

namespace MiniShop.Services;

public class ShopService : IShopService
{
    private readonly IBasketService _basketService;
    private readonly IOrderService _orderService;
    private readonly ConsoleInputService _consoleInputService;

    public ShopService(IBasketService basketService, IOrderService orderService, IProductService productService, ConsoleInputService consoleInputService)
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
            return;
        }
        _orderService.CreateOrder(basketItems);
        _basketService.ClearBasket();
    }
}