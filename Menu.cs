namespace PizzaProj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Authentication;
using System.Security.Principal;

public class Menu
{
    private List<Dough> doughs = new List<Dough>();
    private List<Ingredient> ingredients = new List<Ingredient>();
    private List<Pizza> pizzas = new List<Pizza>();
    private List<Crust> crusts = new List<Crust>();

    private void AddDoughs()
    {
        const double classicDoughPrice = 200;
        Dough classic = new Dough("Классическое", 200, classicDoughPrice);
        Dough thick = new Dough("Толстое", 240, classicDoughPrice);
        Dough black = new Dough("Черное", 220, classicDoughPrice);

        doughs.Add(classic);
        doughs.Add(thick);
        doughs.Add(black);
    }

    private void AddCrusts()
    {
        Crust tomatoCrust = new Crust(ingredients[0]);
        Crust cheeseCrust = new Crust(ingredients[1]);

        cheeseCrust.CompatiblePizzas.Add(pizzas[0]);
        cheeseCrust.CompatiblePizzas.Add(pizzas[3]);
        cheeseCrust.CompatiblePizzas.Add(pizzas[1]);
        cheeseCrust.CompatiblePizzas.Add(pizzas[2]);

        tomatoCrust.CompatiblePizzas.Add(pizzas[1]);
        tomatoCrust.CompatiblePizzas.Add(pizzas[2]);

        crusts.Add(cheeseCrust);
        crusts.Add(tomatoCrust);
    }

    private void AddIngredients()
    {
        Ingredient ketchup = new Ingredient("Кетчуп", 100);
        Ingredient chease = new Ingredient("Сыр", 120);
        Ingredient chicken = new Ingredient("Курица", 160);
        Ingredient ham = new Ingredient("Колбаса", 140);
        Ingredient tomato = new Ingredient("Помидоры", 100);
        Ingredient olives = new Ingredient("Оливки", 60);
        Ingredient mushrooms = new Ingredient("Грибы", 120);
        Ingredient sausages = new Ingredient("Сосиски", 140);
        Ingredient pineapple = new Ingredient("Ананасы", 110);

        ingredients.Add(ketchup);
        ingredients.Add(chease);
        ingredients.Add(chicken);
        ingredients.Add(ham);
        ingredients.Add(tomato);
        ingredients.Add(olives);
        ingredients.Add(mushrooms);
        ingredients.Add(sausages);
        ingredients.Add(pineapple);
    }

    private void AddPizzas()
    {
        Pizza margarita = new Pizza("Маргарита", doughs[0]);
        margarita.Ingredients.Add(new PizzaIngredient(ingredients[0], 1, ingredients[0].Price));
        margarita.Ingredients.Add(new PizzaIngredient(ingredients[4], 1, ingredients[4].Price));
        margarita.Ingredients.Add(new PizzaIngredient(ingredients[1], 1, ingredients[1].Price));
        margarita.Ingredients.Add(new PizzaIngredient(ingredients[5], 1, ingredients[5].Price));

        Pizza meat = new Pizza("Мясная", doughs[1]);
        meat.Ingredients.Add(new PizzaIngredient(ingredients[0], 1, ingredients[0].Price));
        meat.Ingredients.Add(new PizzaIngredient(ingredients[3], 1, ingredients[3].Price));
        meat.Ingredients.Add(new PizzaIngredient(ingredients[7], 1, ingredients[7].Price));
        meat.Ingredients.Add(new PizzaIngredient(ingredients[2], 1, ingredients[2].Price));
        meat.Ingredients.Add(new PizzaIngredient(ingredients[1], 1, ingredients[1].Price));
        meat.Ingredients.Add(new PizzaIngredient(ingredients[4], 1, ingredients[4].Price));

        Pizza mushrum = new Pizza("Грибная", doughs[0]);
        mushrum.Ingredients.Add(new PizzaIngredient(ingredients[0], 1, ingredients[0].Price));
        mushrum.Ingredients.Add(new PizzaIngredient(ingredients[6], 1, ingredients[6].Price));
        mushrum.Ingredients.Add(new PizzaIngredient(ingredients[2], 1, ingredients[2].Price));
        mushrum.Ingredients.Add(new PizzaIngredient(ingredients[1], 1, ingredients[1].Price));

        Pizza hawaian = new Pizza("Гавайская", doughs[2]);
        hawaian.Ingredients.Add(new PizzaIngredient(ingredients[0], 1, ingredients[0].Price));
        hawaian.Ingredients.Add(new PizzaIngredient(ingredients[2], 1, ingredients[2].Price));
        hawaian.Ingredients.Add(new PizzaIngredient(ingredients[8], 1, ingredients[8].Price));
        hawaian.Ingredients.Add(new PizzaIngredient(ingredients[5], 1, ingredients[5].Price));
        hawaian.Ingredients.Add(new PizzaIngredient(ingredients[1], 1, ingredients[1].Price));

        pizzas.Add(margarita);
        pizzas.Add(meat);
        pizzas.Add(mushrum);
        pizzas.Add(hawaian);
    }

    private void InputNewIngredients()
    {
        while (true)
        {
            Console.WriteLine("Добавить свой ингредиент? 1 - да, 0 - нет");
            if (Console.ReadLine() == "0") break;

            Console.Write("Название: ");
            string name = Console.ReadLine();
            Console.Write("Цена: ");
            double price = double.Parse(Console.ReadLine());

            ingredients.Add(new Ingredient(name, price));
            Console.WriteLine("--- Добавлено! ---");
        }
    }

    private void InputNewDoughs()
    {
        const double classicDoughPrice = 200;
        while (true)
        {
            Console.WriteLine("Добавить свою основу для теста? 1 - да, 0 - нет");
            if (Console.ReadLine() == "0") break;

            Console.Write("Название: ");
            string name = Console.ReadLine();
            Console.Write("Цена: ");
            double price = double.Parse(Console.ReadLine());

            doughs.Add(new Dough(name, price, classicDoughPrice));
            Console.WriteLine("--- Добавлено! ---");
        }
    }

    private void InputNewCrusts()
    {
        while (true)
        {
            Console.WriteLine("Добавить новый бортик? 1 - да, 0 - нет");
            if (Console.ReadLine() == "0") break;

            Console.WriteLine("Выберите ингредиент для основы бортика:");
            ShowIngredients();
            int input = int.Parse(Console.ReadLine());
            if (!(input > 0 && input < ingredients.Count))
            {
                Console.WriteLine("Ошибка");
                input = int.Parse(Console.ReadLine());
            }
            Ingredient chosenIngredient = ingredients[input - 1];
            Crust newCrust = new Crust(chosenIngredient);

            Console.WriteLine("К каким пиццам подходит этот бортик?");
            ShowPizzas();
            Console.WriteLine("Введите номера через пробел. Если подходит ко всем, нажмите Enter.");
            string pizzaInput = Console.ReadLine();

            List<int> pizzaList = pizzaInput.Split(' ').Select(int.Parse).ToList();
            foreach (var pizza in pizzaList)
            {
                if (pizza > 0 && pizza <= pizzas.Count)
                {
                    if (!newCrust.CompatiblePizzas.Contains(pizzas[pizza - 1]))
                    {
                        newCrust.CompatiblePizzas.Add(pizzas[pizza - 1]);
                    }
                }
            }
            crusts.Add(newCrust);
            Console.WriteLine("---Добавлено!---");
        }
    }

    public void ShowIngredients()
    {
        Console.WriteLine("ИНГРЕДИЕНТЫ:");

        for (int i = 0; i < ingredients.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ingredients[i].Name} - {ingredients[i].Price} руб");
        }
    }

    public void ShowDoughs()
    {
        Console.WriteLine("ОСНОВЫ:");
        for (int i = 0; i < doughs.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {doughs[i].Name} - {doughs[i].Price} руб");
        }
    }

    public void ShowPizzas()
    {
        Console.WriteLine("ПИЦЦЫ:");
        for (int i = 0; i < pizzas.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pizzas[i].Name} - {pizzas[i].CountPrice()} руб");
        }
        
    }

    public void ShowPizzaIngredients()
    {
        Console.WriteLine("\nХотите посмотреть ингредиенты конкретной пиццы? номер или 0 для выхода)");
        int input = Convert.ToInt32(Console.ReadLine());
        if (input > 0 && input <= pizzas.Count)
        {
            pizzas[input - 1].PizzaInfo();
        }
    }
    public Pizza GetPizzaByIndex(int index)
    {
        if (index >= 0 && index < pizzas.Count)
        {
            return pizzas[index];
        }
        Console.WriteLine("Пицца не найдена!");
        return null;
    }

    public void ShowCrusts()
    {
        Console.WriteLine("\nБОРТИКИ:");
        for (int i = 0; i < crusts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {crusts[i].Name} - {crusts[i].Price} руб");
        }
    }

    public Crust GetCrustByIndex(int index)
    {
        if (index >= 0 && index < crusts.Count) 
        { 
            return crusts[index]; 
        }
        Console.WriteLine("Бортик не найден!");
        return null;
    }


    //!!!!!!!!!!!!!!!!!
    public Pizza MakePizza()
    {
        Console.WriteLine("Создаем пиццу!");

        Console.WriteLine("Введитте название:");
        string name = Console.ReadLine();

        Console.WriteLine("Выберите основу:");
        ShowDoughs();
        int input = Convert.ToInt32(Console.ReadLine());
        Dough chosenDough = null;

        if (input > 0 && input <= doughs.Count)
        {
            chosenDough = doughs[input - 1];
        }
        else
        {
            Console.WriteLine("Неверный номер основы, теперь основа: классическая");
            chosenDough = doughs[0];
        }
        Pizza newPizza = new Pizza(name, chosenDough);

        Console.WriteLine("Теперь выберите номера и введите все в строку c пробелами:");
        Console.WriteLine("Если ввести ингредиент дважды, он будет удвоен в пицце");
        ShowIngredients();
        string newpizzaIngredients = Console.ReadLine();
        List<int> newpizzaIngredientsList = newpizzaIngredients.Split(' ').Select(int.Parse).ToList();
        foreach (var ingredient in newpizzaIngredientsList)
        {
            Ingredient chosenIngredient = ingredients[ingredient - 1];
            newPizza.Ingredients.Add(new PizzaIngredient(chosenIngredient, 1, chosenIngredient.Price));
            Console.WriteLine($"Теперь в вашей пицце {chosenIngredient.Name}, + {chosenIngredient.Price} руб ");
        }

        Console.WriteLine("Выберите размер: 0 - Маленькая, 1 - Средняя, 2 - Большая");
        int sizeIdx = int.Parse(Console.ReadLine());
        newPizza.Size = (PizzaSize)sizeIdx;

        Console.WriteLine("\n---Вот ваша пицца:---");
        newPizza.PizzaInfo();
        return newPizza;
    }


    //!!!!!!!
    public IOrderable MakeHalfPizza()
    {
        Console.Clear();
        Console.WriteLine("=== КОНСТРУКТОР ПИЦЦЫ ИЗ ПОЛОВИНОК ===");

        ShowDoughs();
        Console.Write("Выберите общую основу для всей пиццы: ");
        int doughInput = int.Parse(Console.ReadLine()) - 1;
        Dough commonDough = doughs[doughInput];

        Pizza[] halves = new Pizza[2];
        for (int i = 0; i < 2; i++)
        {
            string side;
            if (i == 0) side = "правой";
            else side = "левой";
            Console.WriteLine($"\nНастройка {side} половины:");
            Console.WriteLine("1 - Выбрать готовую пиццу из меню");
            Console.WriteLine("2 - Собрать новую по ингредиентам");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                ShowPizzas();
                Console.Write("Введите номер пиццы: ");
                int pizzaInput = int.Parse(Console.ReadLine()) - 1;

                halves[i] = pizzas[pizzaInput].Clone();
                halves[i].Dough = commonDough;
            }
            else
            {
                Console.Write("Введите название для этой половины: ");
                string halfName = Console.ReadLine();
                halves[i] = new Pizza(halfName, commonDough);

                ShowIngredients();
                Console.WriteLine("Введите номера ингредиентов через пробел:");
                string ingrInputList = Console.ReadLine();
                var ingrInput = ingrInputList.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
                foreach (var ingr in ingrInput)
                {
                    halves[i].Ingredients.Add(new PizzaIngredient(ingredients[ingr - 1], 1, ingredients[ingr - 1].Price));
                }
            }
        }

        CombinedPizza finalPizza = new CombinedPizza(halves[0], halves[1], commonDough, null);

        Console.WriteLine("\nБортики будут общие (1) или разные у каждой половины (2)?");
        if (Console.ReadLine() == "1")
        {
            halves[0].Edge = null;
            halves[1].Edge = null;

            ShowCrusts();
            Console.Write("Выберите ОБЩИЙ бортик: ");
            int crustInput = int.Parse(Console.ReadLine()) - 1;
            finalPizza.CommonCrust = GetCrustByIndex(crustInput);
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                if (halves[i].Edge == null)
                {
                    Console.WriteLine($"Добавить бортик для половины '{halves[i].Name}'? 1-да, 0-нет");
                    if (Console.ReadLine() == "1")
                    {
                        ShowCrusts();
                        int cIdx = int.Parse(Console.ReadLine()) - 1;
                        halves[i].Edge = GetCrustByIndex(cIdx);
                    }
                }
            }
        }

        return finalPizza;
    }
    public void MakeMenu()
    {
        AddDoughs();
        AddIngredients();
        AddPizzas();
        AddCrusts();
    }

    public void ShowPizzasFilteredByIngredient(string ingredientName)
    {
        Console.Clear();
        Console.WriteLine($"--ПОИСК ПИЦЦ С ИНГРЕДИЕНТОМ: {ingredientName.ToUpper()}--");
        List<Pizza> filteredPizzas = new List<Pizza>();

        foreach (var pizza in pizzas)
        {   
            foreach (var item in pizza.Ingredients)
            {
                if (item.Ingredient.Name.Equals(ingredientName, StringComparison.OrdinalIgnoreCase))
                {
                    filteredPizzas.Add(pizza);
                    break;
                }
            }
        }
        if (filteredPizzas.Count > 0)
        {
            Console.WriteLine($"Найдено вариантов: {filteredPizzas.Count}\n");
            for (int i = 0; i < filteredPizzas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {filteredPizzas[i].Name} — {filteredPizzas[i].CountPrice()} руб.");
            }

            Console.WriteLine("\nХотите посмотреть информацию о пицце? введите номер или 0 для выхода");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= filteredPizzas.Count)
            {
                filteredPizzas[choice - 1].PizzaInfo();
            }
        }
        else
        {
            Console.WriteLine("Не найдено");
        }

        Console.WriteLine("\nНажмите Enter, чтобы вернуться");
        Console.ReadKey();
    }

    public void EditMenu()
    {
        Console.WriteLine("\nРедактирование меню:");
        Console.WriteLine("\nЧто редактировать:1 - основы, 2 - ингредиенты, 3 - бортики");
        string input = Console.ReadLine();
        if (input == "1")
        {
            InputNewDoughs();
        }
        else if (input == "2")
        {
            InputNewIngredients();
        }
        else if (input == "3")
        {
            InputNewCrusts();
        }

        Console.WriteLine("Изменения сохранены");
    } 
}