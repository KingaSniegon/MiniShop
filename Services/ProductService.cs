using MiniShop.Models;

namespace MiniShop.Services;

public class ProductService
{
    private readonly List<Product> _products = new();

    public List<Product> GetAllProducts()
    {
        return _products.ToList();
    }

    public void AddProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        if (_products.Any(p => p.Id == product.Id))
        {
            throw new ArgumentException("Product with this ID already exists");
        }

        _products.Add(product);
    }
}