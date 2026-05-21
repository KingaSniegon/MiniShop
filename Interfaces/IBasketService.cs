using System.Collections.Generic;
using MiniShop.Models;

namespace MiniShop.Interfaces;
public interface IBasketService
{
    List<BasketItem> AddToBasket(int productId, int quantity);
    void UpdateQuantity(int productId, int requestedQuantity);
    void RemoveFromBasket (int productId);
    List<BasketItem> GetBasketItems();
    decimal CalculateTotal();
    public void ClearBasket();
}