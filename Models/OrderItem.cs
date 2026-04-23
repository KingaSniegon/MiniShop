using System.Net.Http.Headers;
using MiniShop.Services;

namespace MiniShop.Models;

public class OrderItem
{
    public string? ProductName { get; set; }
    public decimal UnitPrice {get; set; }
    public int Quantity { get; set; }
}