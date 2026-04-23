 namespace MiniShop.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; } // nullable string, moze byc null
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
