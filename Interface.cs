namespace PizzaProj; 
using System;


public interface IOrderable
{
    string Name { get; }
    double CountPrice();
    void PizzaInfo();
}