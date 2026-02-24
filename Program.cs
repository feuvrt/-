//namespace PizzaProj;
//using System;
//using System.Collections.Generic;
//public class Program
//{
//    public static void Main()
//    {
//        List<Order> allOrders = new List<Order>();
//        Menu newMenu = new Menu();
//        newMenu.MakeMenu();
//        Order newOrder = new Order();
//        newOrder.MakeOrder(newMenu);
//        allOrders.Add(newOrder);

//        bool flag = true;

//        while (flag)
//        {
//            Console.WriteLine("\nВыберите действие:");
//            Console.WriteLine("1) Посмотреть меню");
//            Console.WriteLine("2) Редактировать меню (добавить ингредиенты/основы)");
//            Console.WriteLine("3) Собрать свою пиццу");
//            Console.WriteLine("0) Выход");

//            string input = Console.ReadLine();
//            if (input == "1")
//            {
//                {
//                    Console.WriteLine("\nЧто посмотреть?");
//                    Console.WriteLine("1. Готовые пиццы");
//                    Console.WriteLine("2. Доступные основы и ингредиенты");

//                    string viewChoice = Console.ReadLine();

//                    if (viewChoice == "1")
//                    {
//                        Console.Clear();
//                        newMenu.ShowPizzas();
//                    }
//                    else if (viewChoice == "2")
//                    {
//                        Console.Clear();
//                        newMenu.ShowDoughs();
//                        Console.WriteLine("--------------");
//                        newMenu.ShowIngredients();
//                    }
//                    else
//                    {
//                        Console.WriteLine("Возврат в главное меню");
//                    }
//                }
//            }
//            else if (input == "2")
//            {
//                newMenu.EditMenu();
//            }
//            else if (input == "3")
//            {
//                newMenu.MakePizza();
//            }
//            else if (input == "0")
//            {
//                flag = false;
//            }
//            else
//            {
//                Console.WriteLine("Ошибка: введите число от 0 до 3");
//            }
//        }
//    }
//}