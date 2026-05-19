using System.Collections.Generic;
using MiniShop.Models;

namespace MiniShop.Interfaces;
public interface IProductService
{
    List<Product> GetAllProducts();
    Product GetProductById(int productId);
    List<Product> AddProduct(Product product);
}