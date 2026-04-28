using System.Net.Http.Headers;
using MiniShop.Services;

namespace MiniShop.Models;

public class ProductService
{
    private List<Product> _products = new List<Product>();

    public void ShowProducts()
    {
        Console.WriteLine("Available products:");
        foreach(var product in _products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price} PLN");
        }
    }
    
    public List<Product> GetAllProducts() // metoda zwracajaca liste wszystkich produktow
    {
        return _products;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }
    
}