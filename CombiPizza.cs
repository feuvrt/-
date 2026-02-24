namespace PizzaProj;
using System;

public class CombinedPizza : IOrderable
{
    public string Name => $"Комбинированная ({half1.Name} + {half2.Name})";
    private Pizza half1;
    private Pizza half2;
    public Dough CommonDough { get; set; }
    public Crust CommonCrust { get; set; }

    public CombinedPizza(Pizza first, Pizza second, Dough commonDough, Crust commonEdge)
    {
        half1 = first;
        half2 = second;
        CommonDough = commonDough;
        CommonCrust = commonEdge;
    }

    public double CountPrice()
    {
        double price = (half1.CountPrice() + half2.CountPrice()) / 2;
        if (CommonCrust != null)
        {
            price += CommonCrust.Price;
        }
        return price;
    }

    public void PizzaInfo()
    {
        Console.WriteLine("КОМБИНИРОВАННАЯ ПИЦЦА:");
        Console.WriteLine("ЛЕВАЯ ПОЛОВИНА:");
        half1.PizzaInfo();

        Console.WriteLine("\nПРАВАЯ ПОЛОВИНА:");
        half2.PizzaInfo();

        Console.WriteLine($"\n Стоимость пиццы из половинок: {CountPrice()} руб");
    }
}