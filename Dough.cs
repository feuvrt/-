namespace PizzaProj;
using System;

public class Dough
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

    public Dough(string name, double price, double classicPrice)
    {
        this.name = name;
        this.price = price;

        if (name.ToLower() == "классическое")
        {
            this.price = price;
        }
        else
        {
            double MaxPrice = classicPrice * 1.2;
            if (price > MaxPrice) this.price = MaxPrice;
            else this.price = price;
        }
    }
}