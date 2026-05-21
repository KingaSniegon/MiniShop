using System.Collections.Generic;
using MiniShop.Models;

namespace MiniShop.Interfaces;
public interface IOrderService
{
    void AddOrder(Order order);
    List<Order> GetOrders();
    Order CreateOrder(List<BasketItem> basketItems);
    List<Order> GetAllOrders();
}