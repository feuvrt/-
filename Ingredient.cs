namespace PizzaProj; 
using System;

public class Ingredient
{
    private string name;
    private double price;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public double Price
    {
        get { return price; }
        set { price = value; }
    }

    public Ingredient(string name, double price)
    {
        this.name = name;
        this.price = price;
    }
}