using System;
using System.Collections.Generic;
using System.Linq;

namespace FrigerExe
{
    class Program
    {
        static List<Refrigerator> refrigeratorsList = new List<Refrigerator>();

        public List<Refrigerator> SortfrigeBySpace(List<Refrigerator> frigerList)
        {
            frigerList = frigerList.OrderByDescending(o => o.SpaceLeft()).ToList();
            return frigerList;
        }
        public void EnterItemToFriger()
        {
            Console.WriteLine("Enter friger id:");
            int frigerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter item name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter type (drink, food):");
            string type = Console.ReadLine();
            Type userType; 
            userType = (Type)Enum.Parse(typeof(Type), type);
            Console.WriteLine("Enter kashrut (dairy, meaty, parve):");
            string kashrut  = Console.ReadLine();

            Kashrut userKashrut;

            //האם זה דרך נכונה או שעדיף לעשות עם switch
            userKashrut = (Kashrut)Enum.Parse(typeof(Kashrut), kashrut);
            Console.WriteLine("Enter expiry date: year:");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter expiry date: month:");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter expiry date: day:");
            int day = Convert.ToInt32(Console.ReadLine());
            DateTime date = new DateTime(year, month, day);
            Console.WriteLine("Enter space");
            int space = Convert.ToInt32(Console.ReadLine());
            Item item = new Item(name, userType, userKashrut, date, space);
            Refrigerator frigerToInsert;
            foreach (Refrigerator r in refrigeratorsList)
            {
                if (r.GetId() == frigerId)
                {
                    r.AddItem(item);
                    break;
                }
            }
            //if (!Enum.TryParse<Type>(type, out userType))
            //{
            //    Console.WriteLine("The value is incorrect, enter a new value.");
            //}




        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //List<Refrigerator> refrigeratorsList = new List<Refrigerator>();

            Refrigerator r1 = new Refrigerator("abcd", "green", 3);
            refrigeratorsList.Add(r1);
            Shelf s1 = new Shelf(1, 15);
            Shelf s2 = new Shelf(2, 10);
            Shelf s3 = new Shelf(3, 12);
            Item i1 = new Item("milk", Item.Type.drink, Item.Kashrut.dairy, new DateTime(2023, 10, 25), 5);
            Item i2 = new Item("bread", Item.Type.food, Item.Kashrut.parve, new DateTime(2023, 10, 29), 8);
            Item i3 = new Item("tomato", Item.Type.food, Item.Kashrut.parve, new DateTime(2023, 11, 4), 2);
            Item i4 = new Item("meat", Item.Type.food, Item.Kashrut.meaty, new DateTime(2023, 12, 20), 4);
            Item i5 = new Item("water", Item.Type.drink, Item.Kashrut.parve, new DateTime(2023, 12, 29), 6);
            Item i6 = new Item("cheese", Item.Type.food, Item.Kashrut.dairy, new DateTime(2023, 10, 24), 5);
            Item i7 = new Item("ketshop", Item.Type.food, Item.Kashrut.parve, new DateTime(2023, 11, 25), 6);

            s1.AddItem(i1);
            s1.AddItem(i2);
            s2.AddItem(i3);
            s3.AddItem(i4);
            s3.AddItem(i5);
            r1.AddShelf(s1);
            r1.AddShelf(s2);
            r1.AddShelf(s3);

            //Console.WriteLine( i1.ToString());
            //Console.WriteLine(s1.ToString());
            Console.WriteLine(r1.ToString());

            Console.WriteLine("Enter a number from 1 to 10 or 100 to exit: ");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    Console.WriteLine(r1.ToString());
                    break;
                case 2:
                    Console.WriteLine(r1.SpaceLeft());
                    break;
                case 3:
                    Console.WriteLine("Enter item to friger:");
                    EnterItemToFriger();
                    break;
                case 4:
                    Console.WriteLine("remove item from friger:");
                    Console.WriteLine("enter item id:");
                    int itemId = Convert.ToInt32(Console.ReadLine());
                    r1.RemoveItem(itemId);
                    break;
                case 5:
                    r1.CleanFriger();
                    break;
                case 6:
                    Console.WriteLine("What do you want to eat?");
                    Console.WriteLine("Enter kashrut (dairy, meaty, parve):");
                    string kashrut = Console.ReadLine();
                    Kashrut userKashrut;

                    //למה הוא לא מזהה אותו?

                    userKashrut = (Kashrut)Enum.Parse(typeof(Kashrut), kashrut);
                    Console.WriteLine("Enter type (food, drink):");
                    string type = Console.ReadLine();

                    Type userType;
                    userType = (Type)Enum.Parse(typeof(Type), type);
                    r1.WhatToEat(userKashrut, userType);
                    break;
                case 7:
                    foreach(Shelf s in r1.GetShelfList())
                    {
                        List<Item> Items = s.SortItemByExpiryDate();
                        Console.WriteLine(Items.ToString());
                    }
                    break;
                case 8:
                    r1.SortShelfBySpace().ToString();
                    break;
                case 9:
                    Console.WriteLine(SortfrigeBySpace(refrigeratorsList).ToString());
                    break;
                case 10:
                    r1.readyForShopping();
                    break;
                case 100:
                    return;
                    
            }



        }
    }
}
