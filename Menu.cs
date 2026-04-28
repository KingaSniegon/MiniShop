using MiniShop.Models;
using MiniShop.Services;

class Menu
{
    private readonly ProductService _productService;
    private readonly BasketService _basketService;
    private readonly OrderService _orderService;
    private readonly ShopService _shopService;

    public Menu(ProductService productService, BasketService basketService, OrderService orderService, ShopService shopService)
    {
        _productService = productService;
        _basketService = basketService;
        _orderService = orderService;
        _shopService = shopService;
    }

    public bool ShowMenu()
    {
        Console.WriteLine("Welcome to MiniShop!");
        Console.WriteLine("1. View products");
        Console.WriteLine("2. Add product to basket");
        Console.WriteLine("3. View basket");
        Console.WriteLine("4. Place order");
        Console.WriteLine("5. Exit");
        Console.WriteLine("Please select an option: ");
        var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            _productService.ShowProducts();
            return true;

        case "2":
            _shopService.AddProductsToBasket();
            return true;

        case "3":
            _basketService.ShowBasket();
            return true;

        case "4":
            _shopService.Checkout();
            _orderService.ShowOrderDetails();
            return true;

        case "5":
            Console.WriteLine("Thank you for shopping!");
            return false;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            return true;
        }
    }
}