using Xunit;
using MiniShop.Models;

public class BasketServiceTests
{
    [Fact]
    public void CalculateTotal_ShouldReturnCorrectSum()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();
        var basketService = new BasketService(productService);

        var product = new Product
        {
            Id = 1,
            Name = "Apple",
            Price = 10
        };

        productService.AddProduct(product);

        basketService.AddToBasket(product.Id, 2);

        // ACT (działanie)
        var result = basketService.CalculateTotal();

        // ASSERT (sprawdzenie)
        Assert.Equal(20, result);
    }

    [Fact]
    public void CalculateTotal_ShouldSumMultipleProducts()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();
        var basketService = new BasketService(productService);

        var productList = new List <Product>
        { new Product { Id = 1, Name = "Apple", Price = 10 },
          new Product { Id = 2, Name = "Banana", Price = 5 },
        };

        foreach (var product in productList)
        {
            productService.AddProduct(product);
            basketService.AddToBasket(product.Id, 1);
        }

        // ACT (działanie)
        var result = basketService.CalculateTotal();

        // ASSERT (sprawdzenie)
        Assert.Equal(15, result);
    }    

    [Fact]
    public void GetBasketItems_ShouldReturnCorrectList()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();
        var basketService = new BasketService(productService);

        var productList = new List <Product>
        { new Product { Id = 1, Name = "Apple", Price = 10 },
          new Product { Id = 2, Name = "Banana", Price = 5 },
        };

        foreach (var product in productList)
        {
            productService.AddProduct(product);
            basketService.AddToBasket(product.Id, 1);
        }

        // ACT (działanie)
        var result = basketService.GetBasketItems();

        // ASSERT (sprawdzenie)
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, x => x.Product.Id == 1 && x.Product.Name == "Apple" && x.Quantity == 1);
        Assert.Contains(result, x => x.Product.Id == 2 && x.Product.Name == "Banana" && x.Quantity == 1);
    } 

    [Fact]
    public void AddToBasket_ShouldIncreaseQuantity_WhenProductAlreadyExists()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();
        var basketService = new BasketService(productService);

        var product = new Product
        {
            Id = 1,
            Name = "Apple",
            Price = 10
        };

        productService.AddProduct(product);
        basketService.AddToBasket(product.Id, 1);

        // ACT (działanie)
        var result = basketService.AddToBasket(product.Id, 1);

        // ASSERT (sprawdzenie)
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Contains(result, x => x.Product.Id == 1 && x.Product.Name == "Apple" && x.Quantity == 2);
    } 

    [Fact]
    public void ClearBasket_ShouldEmptyBasket()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();
        var basketService = new BasketService(productService);

        var product = new Product
        {
            Id = 1,
            Name = "Apple",
            Price = 10
        };

        productService.AddProduct(product);
        basketService.AddToBasket(product.Id, 1);

        // ACT (działanie)
        basketService.ClearBasket();
        var result = basketService.GetBasketItems();

        // ASSERT (sprawdzenie)
        Assert.Empty(result);
    }     

    [Fact]
    public void AddToBasket_ProductDoesNotExist_ShouldNotAddToBasket()
    {
        // ARRANGE (przygotowanie)
        var productService = new ProductService();
        var basketService = new BasketService(productService);

        var product = new Product
        {
            Id = 1,
            Name = "Apple",
            Price = 10
        };

        productService.AddProduct(product);
        basketService.AddToBasket(1, 1);

        var result = Assert.Throws<ArgumentException>(() =>
        basketService.AddToBasket(2, 1));
        
        Assert.Equal("Product with id 2 doesn't exist", result.Message);
    }  
    
    [Fact]
    public void CalculateTotal_ShouldReturnZero_WhenBasketIsEmpty()
    {
        var productService = new ProductService();
        var basketService = new BasketService(productService);

        var result = basketService.CalculateTotal();

        Assert.Equal(0, result);
    }
}
