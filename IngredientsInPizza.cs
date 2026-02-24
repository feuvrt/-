namespace PizzaProj;

public class PizzaIngredient
{
    public Ingredient Ingredient { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }

    public PizzaIngredient(Ingredient ingredient, int count, double price)
    {
        Ingredient = ingredient;
        Count = count;
        Price = price;
    }

    public double CountPrice()
    {
        return Ingredient.Price * Count;
    }
}