 namespace MiniShop.Models;

public class Order
{
public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
public int Id { get; set; }
}