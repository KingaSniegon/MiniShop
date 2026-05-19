using System.Collections.Generic;
using MiniShop.Models;

namespace MiniShop.Interfaces;
public interface IBasketService
{
    List<BasketItem> AddToBasket(int productId, int quantity);
    decimal CalculateTotal();
    List<BasketItem> GetBasketItems();
    void ClearBasket();
}