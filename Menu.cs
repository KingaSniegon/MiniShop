using MiniShop.Services;
using MiniShop.Interfaces;

class Menu
{
    private readonly IProductService _productService;
    private readonly IBasketService _basketService;
    private readonly IOrderService _orderService;
    private readonly IShopService _shopService;
    private readonly ConsoleOutputService _consoleOutputService;
    private readonly ConsoleInputService _consoleInputService;


    public Menu(IProductService productService, IBasketService basketService, IOrderService orderService, IShopService shopService, ConsoleOutputService consoleOutputService, ConsoleInputService consoleInputService)
    {
        _productService = productService;
        _basketService = basketService;
        _orderService = orderService;
        _shopService = shopService;
        _consoleOutputService = consoleOutputService;
        _consoleInputService = consoleInputService;
    }

    public bool ShowMenu()
    {
        Console.WriteLine("1. View products");
        Console.WriteLine("2. Add product to basket");
        Console.WriteLine("3. View basket");
        Console.WriteLine("4. Remove product from the basket");
        Console.WriteLine("5. Change quantity of product");
        Console.WriteLine("6. Place order");
        Console.WriteLine("7. Exit");
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
            var productId = _consoleInputService.ReadProductId();
            _basketService.RemoveFromBasket(productId);
            return true;

        case "5":
            var productIdForQuantity = _consoleInputService.ReadProductId();
            var quantity = _consoleInputService.ReadQuantity();
            _basketService.UpdateQuantity(productIdForQuantity, quantity);
            return true;
        case "6":
            _shopService.Checkout();
            var order = _orderService.GetOrders();
            _consoleOutputService.ShowOrders(order);
            return true;

        case "7":
            Console.WriteLine("Thank you for shopping!");
            return false;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            return true;
        }
    }
}