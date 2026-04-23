using MiniShop.Services;

var shop = new ShopService();

var products = shop.GetAllProducts();
foreach (var product in products)
{
    Console.WriteLine($"{product.Id}: {product.Name} - {product.Price} PLN");
}
shop.AddProducts();
shop.ShowBasket();
shop.CalculateTotal();
shop.Checkout();
shop.ShowOrderDetails();
