namespace MiniShop.Services;

public class ConsoleInputService
{
    public int ReadProductId()
    {
        while (true)
        {
            Console.WriteLine("Enter product id:");
            if (int.TryParse(Console.ReadLine(), out int id))
                return id;

            Console.WriteLine("Invalid id");
        }
    }

    public int ReadQuantity()
    {
        while (true)
        {
            Console.WriteLine("Enter quantity:");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                return quantity;

            Console.WriteLine("Invalid quantity");
        }
    }

    public bool AskToContinue()
    {
        Console.WriteLine("Add another product? (yes/no)");
        return Console.ReadLine()?.Trim().ToLower() == "yes";
    }
}