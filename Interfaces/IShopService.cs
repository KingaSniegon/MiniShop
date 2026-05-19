using System.Collections.Generic;
using MiniShop.Models;

namespace MiniShop.Interfaces;
public interface IShopService
{
void AddProductsToBasket();
public void Checkout ();
}