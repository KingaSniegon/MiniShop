 namespace MiniShop.Models;

public class BasketItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; } // default value of 0
}
