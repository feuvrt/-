namespace PizzaProj; 
using System;
using System.Collections.Generic;

public class Order
{
    private Guid id;
    private List<IOrderable> pizzas;
    private DateTime orderTime;
    private DateTime? deferredTime;
    private string comment;

    public Guid Id
    {
        get { return id; }
    }

    public List<IOrderable> Pizzas
    {
        get { return pizzas; }
        set { pizzas = value; }
    }

    public DateTime OrderTime
    {


        get { return orderTime; }
        set { orderTime = value; }
    }

    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }

    public Order()
    {
        this.id = Guid.NewGuid();
        this.pizzas = new List<IOrderable>();
        this.orderTime = DateTime.Now;
    }

    public void MakeOrder(Menu menu)
    {
        bool isGoing = true;
        while (isGoing)
        {
            Console.WriteLine("\n---Формирование заказа---");
            Console.WriteLine("1) Добавить готовую пиццу из меню");
            Console.WriteLine("2) Собрать свою пиццу");
            Console.WriteLine("3) Собрать пиццу из половинок");
            Console.WriteLine("0) Выход");

            string choice = Console.ReadLine();

            if (choice == "1") 
            {
                menu.ShowPizzas();
                Console.Write("Введите номер пиццы: ");
                int num = Convert.ToInt32(Console.ReadLine());
                Pizza input = menu.GetPizzaByIndex(num - 1);
                if (input != null)
                {
                    Pizza pizzaForOrder = input.Clone();
                    Console.WriteLine($"Выберите размер для '{pizzaForOrder.Name}': 0-Small, 1-Medium, 2-Big");
                    pizzaForOrder.Size = (PizzaSize)Convert.ToInt32(Console.ReadLine());

                    AddCrustToPizza(pizzaForOrder, menu);

                    pizzas.Add(pizzaForOrder);
                    Console.Write("--Пицца добавлена к заказу!--");
                }
            }
            else if (choice == "2")
            {
                Pizza ownPizza = menu.MakePizza();
                if (ownPizza != null)
                {
                    AddCrustToPizza(ownPizza, menu);
                    pizzas.Add(ownPizza);
                }
            }
            else if (choice == "3")
            {
                var halfPizza = menu.MakeHalfPizza();
                pizzas.Add(halfPizza);
            }
            else if (choice == "0")
            {
                isGoing = false;
            }
        }
        Console.WriteLine("\nКогда доставить заказ?");
        Console.WriteLine("1 - Как можно скорее");
        Console.WriteLine("2 - Отложенный заказ (указать дату и время)");
        string timeInput = Console.ReadLine();

        if (timeInput == "2")
        {
            Console.WriteLine("Введите дату и время в формате (ДД.ММ.ГГГГ ЧЧ:ММ):");
            string inputTime = Console.ReadLine();

            if (DateTime.TryParse(inputTime, out DateTime resultTime))
            {
                if (resultTime > DateTime.Now)
                {
                    this.deferredTime = resultTime;
                    Console.WriteLine($"Заказ принят на время: {this.deferredTime}");
                }
                else
                {
                    Console.WriteLine("Ошибка. Эта дата уже прошла. Выбрано: 'Как можно скорее'");

                }
            }
            else
            {
                Console.WriteLine("Неверный формат. Будет оформлено 'как можно скорее'.");
            }
        }

        Console.WriteLine("Введите комментарий:");
        this.comment = Console.ReadLine();
    }
    private void AddCrustToPizza(Pizza pizza, Menu menu)
    {
        Console.WriteLine("Хотите добавить бортик? 1 - да, 0 - нет");
        string needCrust = Console.ReadLine();
        if (needCrust == "1")
        {
            menu.ShowCrusts();
            Console.Write("Введите номер бортика: ");
            int crustidx = Convert.ToInt32(Console.ReadLine());

            Crust selectedCrust = menu.GetCrustByIndex(crustidx - 1);

            if (selectedCrust != null)
            {
                if (selectedCrust.IsCompatible(pizza))
                {
                    pizza.Edge = selectedCrust;
                    Console.WriteLine("Бортик добавлен");
                }
                else
                {
                    Console.WriteLine("Бортик не подходит к этой пицце");
                }
            }
        }
    }
    public double CalculateTotal()
    {
        double total = 0;
        foreach (var pizza in pizzas)
        { 
            total += pizza.CountPrice();
        }
        return total;
    }

    public void ShowOrderInfo()
    {
        Console.WriteLine($"\n--ЗАКАЗ N {id}--");
        Console.WriteLine($"Время оформления: {orderTime}");

        if (deferredTime.HasValue)
        {
            Console.WriteLine($"Статус:отложенный заказ");
            Console.WriteLine($"Приготовить к: {deferredTime.Value}");
        }
        else
        {
            Console.WriteLine($"Статус: приготовить как можно скорее");
        }

        if (!string.IsNullOrEmpty(comment))
        {
            Console.WriteLine($"Комментарий: {comment}");
        }

        Console.WriteLine("Состав заказа:");
        foreach (var item in pizzas)
        {
            Console.WriteLine($"- {item.Name}: {item.CountPrice()} руб");
        }
        Console.WriteLine($"ИТОГО К ОПЛАТЕ: {CalculateTotal()} руб");
    }
}