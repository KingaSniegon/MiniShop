using MiniShop.Models;
using MiniShop.Services;

class Program
{
private readonly BasketService? _basketService;

static void Main(string[] args)
{
        var productService = new ProductService();
        var basketService = new BasketService(productService);
        var orderService = new OrderService();
        var shop = new ShopService(basketService, orderService, productService);

        var products = productService.GetAllProducts();
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Id}: {product.Name} - {product.Price} PLN");
        }

        shop.AddProductsToBasket();
        basketService.ShowBasket();
        basketService.CalculateTotal();
        shop.Checkout();
        orderService.ShowOrderDetails();
}
}
