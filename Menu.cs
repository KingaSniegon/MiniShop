using MiniShop.Models;
using MiniShop.Services;

class Menu
{
    private readonly ProductService _productService;
    private readonly BasketService _basketService;
    private readonly OrderService _orderService;
    private readonly ShopService _shopService;
    private readonly ConsoleOutputService _consoleOutputService;


    public Menu(ProductService productService, BasketService basketService, OrderService orderService, ShopService shopService, ConsoleOutputService consoleOutputService)
    {
        _productService = productService;
        _basketService = basketService;
        _orderService = orderService;
        _shopService = shopService;
        _consoleOutputService = consoleOutputService;
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
            var productItems = _productService.GetAllProducts();
            _consoleOutputService.ShowProducts(productItems);
            return true;

        case "2":
            _shopService.AddProductsToBasket();
            return true;

        case "3":
            var basketItems = _basketService.GetBasketItems();
            var total = _basketService.CalculateTotal();
            _consoleOutputService.ShowBasket(basketItems, total);
            return true;

        case "4":
            _shopService.Checkout();
            var order = _orderService.GetOrders();
            _consoleOutputService.ShowOrders(order);
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