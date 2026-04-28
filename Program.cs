using MiniShop.Models;
using MiniShop.Services;

class Program
{

static void Main(string[] args)
{
        var productService = new ProductService();
        var basketService = new BasketService(productService);
        var orderService = new OrderService();
        var shopService = new ShopService(basketService, orderService, productService);
        var menu = new Menu(productService, basketService, orderService, shopService);

        while (menu.ShowMenu())
        {
            // Loop until the user chooses to exit
        }
        
}
}