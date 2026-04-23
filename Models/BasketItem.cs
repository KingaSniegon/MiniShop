 namespace MiniShop.Models;

public class BasketItem
{
    public Product? Product { get; set; } // wlasciciwosc referencyjna do obiektu Product wiec musimy nadac jej wartosc lub pozwolic na nulla
    public int Quantity { get; set; } // default value of 0
}
