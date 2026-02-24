using PizzaProj;
using System;
using System.Collections.Generic;

public class Crust
{
    public Ingredient ingredient { get; }
    public List<Pizza> CompatiblePizzas { get; set; }

    public string Name => $"Бортик: {ingredient.Name}";
    public double Price => ingredient.Price;
    public Crust(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        this.CompatiblePizzas = new List<Pizza>();
    }

    public bool IsCompatible(Pizza pizza)
    {
        if (CompatiblePizzas.Count == 0) return true;
        return CompatiblePizzas.Any(p => p.Name == pizza.Name);
    }
}