using Xunit;
using MiniShop.Models;
using MiniShop.Services;
using System.IO.Pipelines;

public class ProductServiceTests
{
    [Fact]
    public void AddProduct_ShouldAddProductCorrectly()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();

        var product = new Product
        {
            Id = 1,
            Name = "Apple",
            Price = 10
        };

        // ACT (działanie)
        var result = productService.AddProduct(product);

        // ASSERT (sprawdzenie)
        Assert.Contains(result, p => p.Id == 1);
        Assert.Contains(result, p => p.Name == "Apple");
        Assert.Contains(result, p => p.Price == 10);
    }

    [Fact]
    public void AddProduct_Null_ShouldReturnException()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();

        // ACT (działanie)
        var result = Assert.Throws<ArgumentNullException>(() =>
        productService.AddProduct(null));

        // ASSERT (sprawdzenie)
        Assert.Contains("Value cannot be null.", result.Message);
    }

    [Fact]
    public void AddProduct_DuplicatedId_ShouldReturnException()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();

        var product = new Product
        {
            Id = 1,
            Name = "Apple",
            Price = 10
        };

        productService.AddProduct(product);


        // ACT (działanie)
        var result = Assert.Throws<ArgumentException>(() =>
        productService.AddProduct(product));

        // ASSERT (sprawdzenie)
        Assert.Contains("Product with this ID already exists", result.Message);
    }

    [Fact]
    public void GetAllProducts_ShouldReturnProductsCorrectly()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();

        var productList = new List<Product>();

        productService.AddProduct(new Product { Id = 1, Name = "Apple", Price = 10 });
        productService.AddProduct(new Product { Id = 2, Name = "Banana", Price = 15 });
        productService.AddProduct(new Product { Id = 3, Name = "Pear", Price = 8 });      

        // ACT (działanie)
        var result = productService.GetAllProducts();

        // ASSERT (sprawdzenie)
        Assert.Equal(3, result.Count());
        Assert.Contains(result, p => p.Id == 1 && p.Name == "Apple" && p.Price == 10);
        Assert.Contains(result, p => p.Id == 2 && p.Name == "Banana" && p.Price == 15); 
        Assert.Contains(result, p => p.Id == 3 && p.Name == "Pear" && p.Price == 8);        
    }

    [Fact]
    public void GetProductById_ShouldReturnCorrectProduct()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();

        productService.AddProduct(new Product { Id = 1, Name = "Apple", Price = 10 });
        productService.AddProduct(new Product { Id = 2, Name = "Banana", Price = 15 });
        productService.AddProduct(new Product { Id = 3, Name = "Pear", Price = 8 });

        // ACT (działanie)
        var result = productService.GetProductById(2);

        // ASSERT (sprawdzenie)
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
        Assert.Equal("Banana", result.Name);
        Assert.Equal(15, result.Price);     
    }

    [Fact]
    public void GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();

        productService.AddProduct(new Product { Id = 1, Name = "Apple", Price = 10 });
        productService.AddProduct(new Product { Id = 2, Name = "Banana", Price = 15 });

        // ACT (działanie)
        var result = productService.GetProductById(3);

        // ASSERT (sprawdzenie)
        Assert.Null(result);  
    } 

    [Fact]
    public void GetAllProducts_ShouldReturnSeparateList() 
    // sprawdzenie czy nikt z zewnatrz nie moze zmodyfikowac listy dodanych produktow
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();

        productService.AddProduct(new Product { Id = 1, Name = "Apple", Price = 10 });
        productService.AddProduct(new Product { Id = 2, Name = "Banana", Price = 15 });

        // ACT (działanie)
        var productList = productService.GetAllProducts();
        productList.Clear();
        
        var result = productService.GetAllProducts();

        // ASSERT (sprawdzenie)
        Assert.NotNull(result);  
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Id == 1 && p.Name == "Apple" && p.Price == 10);
        Assert.Contains(result, p => p.Id == 2 && p.Name == "Banana" && p.Price == 15); 
    } 

    [Fact]
    public void GetAllProducts_ShouldReturnEmptyList_WhenProductDoesNotExist()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();

        // ACT (działanie)
        var result = productService.GetAllProducts();

        // ASSERT (sprawdzenie)
        Assert.Empty(result);  
    }     
}