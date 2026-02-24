namespace PizzaProj;
using System;
using System.Collections.Generic;

public class Pizza : IOrderable
{
    private string name;
    private Dough dough;
    private List<PizzaIngredient> ingredients;
    private Crust crust;
    private PizzaSize size = PizzaSize.Medium;

    public string Name { get => name; set => name = value; }
    public Dough Dough { get => dough; set => dough = value; }
    public List<PizzaIngredient> Ingredients { get => ingredients; set => ingredients = value; }
    public Crust Edge { get => crust; set => crust = value; }
    public PizzaSize Size { get => size; set => size = value; }

    public Pizza(string name, Dough dough)
    {
        if (dough == null)
        {
            Console.WriteLine("Без основы создать пиццу невозможно");
            return;
        }
        this.name = name;
        this.dough = dough;
        this.ingredients = new List<PizzaIngredient>();
    }

    public double CountPrice()
    {
        double price = dough.Price;
        foreach (var item in ingredients)
        {
            price += item.CountPrice();
        }

        if (crust != null) price += crust.Price;

        if (size == PizzaSize.Small) return price * 0.75;
        if (size == PizzaSize.Big) return price * 1.25;

        return price;
    }

    public Pizza Clone()
    {
        Pizza copyPizza = new Pizza(this.Name, this.Dough);
        copyPizza.Size = this.Size;
        copyPizza.Edge = this.Edge;

        foreach (var item in this.Ingredients)
        {
            PizzaIngredient newPosition = new PizzaIngredient(item.Ingredient, item.Count, item.Ingredient.Price);

            copyPizza.Ingredients.Add(newPosition);
        }

        return copyPizza;
    }

    public void PizzaInfo()
    {
        Console.WriteLine($"Пицца: {name} {size}");
        Console.WriteLine($"Тесто: {dough.Name}  +{dough.Price} руб");
        if (Edge != null)
        {
            Console.WriteLine($"{Edge.Name} +{Edge.Price} руб");
        }

        if (ingredients.Count > 0)
        {
            Console.WriteLine("Ингредиенты:");
            foreach (var ingr in ingredients)
            {
                Console.WriteLine($"- {ingr.Ingredient.Name} (+{ingr.CountPrice()} руб)");
                if (ingr.Count > 1)
                {
                    Console.Write($" x{ingr.Count}");
                }
                Console.WriteLine();
            }
        }
        Console.WriteLine($"Стоимость: {CountPrice()} руб");
    }
}

public enum PizzaSize
{
    Small,
    Medium,
    Big
}