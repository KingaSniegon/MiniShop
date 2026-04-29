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
            var products = _productService.GetAllProducts();
            
        Console.WriteLine("Available products:");
        foreach(var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price} PLN");
        }
            return true;

        case "2":
            _shopService.AddProductsToBasket();
            return true;

        case "3":

            var basketItems = _basketService.GetBasketItems();
            if (basketItems.Count == 0)
            {
                Console.WriteLine("Your basket is empty.");
            }
            else
            {
                Console.WriteLine("Your basket contains:");
                foreach (var item in basketItems)
                {
                    Console.WriteLine($"{item.Product.Name} - Quantity: {item.Quantity}, Total Price: {item.Product.Price * item.Quantity} PLN");
                }
                var total = _basketService.CalculateTotal();
                Console.WriteLine($"Total: {total} PLN");
            }
            return true;

        case "4":
            _shopService.Checkout();
            var orders = _orderService.ShowOrderDetails();
                    foreach(var order in orders)
        {
            Console.WriteLine($"\nOrder ID: {order.Id}");
            foreach(var item in order.OrderItems)
            {
                Console.WriteLine($"  {item.ProductName} - Quantity: {item.Quantity}, Unit Price: {item.UnitPrice} PLN, Total: {item.UnitPrice * item.Quantity} PLN");
            }
            Console.WriteLine($"Order total: {order.OrderItems.Sum(i => i.UnitPrice * i.Quantity)} PLN");
        }
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