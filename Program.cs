using MiniShop.Interfaces;
using MiniShop.Models;
using MiniShop.Services;

class Program
{

static void Main(string[] args)
{
        var consoleInputService = new ConsoleInputService();
        var consoleOutputService = new ConsoleOutputService();
        IProductService productService = new ProductService();
        IBasketService basketService = new BasketService(productService);
        IOrderService orderService = new OrderService();
        IShopService shopService = new ShopService(basketService, orderService, productService, consoleInputService);
        var menu = new Menu(productService, basketService, orderService, shopService, consoleOutputService, consoleInputService);

        productService.AddProduct(new Product { Id = 1, Name = "Laptop", Price = 3000, Stock = 5 });
        productService.AddProduct(new Product { Id = 2, Name = "Mouse", Price = 100, Stock = 20 });
        productService.AddProduct(new Product { Id = 3, Name = "Keyboard", Price = 200, Stock = 10 });

        while (menu.ShowMenu())
        {
            // Loop until the user chooses to exit
        }
        
}
}