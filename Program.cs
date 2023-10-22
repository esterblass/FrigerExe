using System;
using System.Collections.Generic;
using System.Linq;

namespace FrigerExe
{
    class Program
    {
        static List<Refrigerator> refrigeratorsList = new List<Refrigerator>();

        public static List<Refrigerator> SortfrigeBySpace(List<Refrigerator> frigerList)
        {
            frigerList = frigerList.OrderByDescending(o => o.SpaceLeft()).ToList();
            return frigerList;
        }
        public static List<Item> SortItemByExpiryDate(List<Item> itemToSort)
        {
            itemToSort = itemToSort.OrderBy(o => o.GetExpiryDate()).ToList();
            return itemToSort;
        }

        public static void EnterItemToFriger()
        {
            Console.WriteLine("Enter friger id:");
            int frigerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter item name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter type (drink, food):");
            string type = Console.ReadLine();
            Item.Type userType;
            if (type == "food" || type == "drink")
            {
                userType = (Item.Type)Enum.Parse(typeof(Item.Type), type);
            }
            else
            {
                Console.WriteLine("The type input incorrect!");
                return;
            }

            Console.WriteLine("Enter kashrut (dairy, meaty, parve):");
            string kashrut = Console.ReadLine();
            Item.Kashrut userKashrut;
            if (kashrut == "dairy" || kashrut == "parve" || kashrut == "meaty")
            {
                userKashrut = (Item.Kashrut)Enum.Parse(typeof(Item.Kashrut), kashrut);
            }
            else
            {
                Console.WriteLine("The kashrut input incorrect!");
                return;
            }

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

            foreach (Refrigerator r in refrigeratorsList)
            {
                if (r.GetId() == frigerId)
                {
                    r.AddItem(item);
                    Console.WriteLine("The item added to the friger.");
                    break;
                }
            }




        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


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
            //Console.WriteLine(r1.ToString());
            int number = 0;
            while (number != 100)
            {


                Console.WriteLine("Enter a number from 1 to 10 or 100 to exit: ");
                number = Convert.ToInt32(Console.ReadLine());
                switch (number)
                {
                    case 1:
                        Console.WriteLine("Friger's details:");
                        Console.WriteLine(r1.ToString());
                        break;

                    case 2:
                        Console.WriteLine($"The place left in the friger: {r1.SpaceLeft()}");
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
                        List<Item> itemsToRemove = r1.CleanFriger();
                        if (itemsToRemove.Count() == 0)
                        {
                            Console.WriteLine("There is no item to remove");
                        }
                        else
                        {
                            Console.WriteLine("The items that removed:");
                            foreach (Item i in itemsToRemove)
                            {
                                Console.WriteLine(i.ToString());
                            }
                        }
                        break;

                    case 6:
                        Console.WriteLine("What do you want to eat?");
                        Console.WriteLine("Enter kashrut (dairy, meaty, parve):");
                        string kashrut = Console.ReadLine();
                        Item.Kashrut userKashrut;
                        if (kashrut == "dairy" || kashrut == "parve" || kashrut == "meaty")
                        {
                            userKashrut = (Item.Kashrut)Enum.Parse(typeof(Item.Kashrut), kashrut);
                        }
                        else
                        {
                            Console.WriteLine("The kashrut input incorrect!");
                            break;
                        }
                        Console.WriteLine("Enter type (food, drink):");
                        string type = Console.ReadLine();

                        Item.Type userType;
                        if (type == "food" || type == "drink")
                        {
                            userType = (Item.Type)Enum.Parse(typeof(Item.Type), type);
                        }
                        else
                        {
                            Console.WriteLine("The kashrut input incorrect!");
                            break;
                        }
                        List<Item> itemToEat = r1.WhatToEat(userKashrut, userType);

                        foreach (Item i in itemToEat)
                        {
                            Console.WriteLine(i.ToString());
                        }

                        break;

                    case 7:
                        List<Item> ItemsToSort = new List<Item>();
                        foreach (Shelf s in r1.GetShelfList())
                        {
                            foreach (Item i in s.GetItemsOnShelf())
                            {
                                ItemsToSort.Add(i);
                            }
                        }
                        ItemsToSort = SortItemByExpiryDate(ItemsToSort);
                        foreach (Item i in ItemsToSort)
                        {
                            Console.WriteLine(i.ToString());
                        }
                        break;

                    case 8:
                        List<Shelf> sortShelves = r1.SortShelfBySpace();
                        foreach (Shelf s in sortShelves)
                        {
                            Console.WriteLine(s.ToString());
                        }
                        break;

                    case 9:
                        List<Refrigerator> sortFriger = SortfrigeBySpace(refrigeratorsList);
                        foreach (Refrigerator r in sortFriger)
                        {
                            Console.WriteLine(r.ToString());
                        }

                        break;

                    case 10:
                        r1.readyForShopping();
                        break;
                }



            }
        }
    }
}
