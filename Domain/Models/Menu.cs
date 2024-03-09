namespace Domain.Models;

public class Menu
{
    public int FoodId { get; set; }
    public string FoodName { get; set; }
    public decimal FoodPrice { get; set; }
    public bool AffordableFood { get; set; }
}
