using MiniShop.Models;
using MiniShop.Interfaces;

namespace MiniShop.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = new();

    public List<Product> GetAllProducts()
    {
        return _products.ToList();
    }

    public Product GetProductById(int productId)
    {
        return _products.FirstOrDefault(p => p.Id == productId);
    }

    public List<Product> AddProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException();
        }

        if (_products.Any(p => p.Id == product.Id))
        {
            throw new ArgumentException("Product with this ID already exists");
        }

        _products.Add(product);

        return _products;
    }
}