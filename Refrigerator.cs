using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrigerExe
{
    class Refrigerator
    {
        private static int uniqueId = 1;
        private int _id;
        private string _model;
        private string _color;
        private int _numOfShelves;
        private List<Shelf> _shelfList;

        public Refrigerator(string model, string color, int numOfShelves)
        {
            _id = uniqueId++;
            _model = model;
            _color = color;
            _numOfShelves = numOfShelves;
            _shelfList = new List<Shelf>();

        }

        public int GetId() { return _id; }
        public string GetModel() { return _model; }
        public string GetColor() { return _color; }
        public int GetNumOfShelves() { return _numOfShelves; }
        public List<Shelf> GetShelfList() { return _shelfList; }


        public void AddShelf(Shelf s)
        {
            if (_shelfList.Count() >= _numOfShelves)
            {
                Console.WriteLine("The fridge is already full of shelves");
            }
            else { _shelfList.Add(s); }

        }
        public void RemoveShelf(Shelf s)
        {
            _shelfList.Remove(s);
        }

        public int SpaceLeft()
        {
            int spaceAvailable = 0;
            foreach (Shelf s in _shelfList)
            {
                spaceAvailable += s.SpaceLeft();
            }
            return spaceAvailable;
        }

        public void AddItem(Item i)
        {

            bool predicate(Shelf s)
            {
                return s.SpaceLeft() - i.GetSpace() >= 0;
            }
            Shelf freeShelf = _shelfList.Find(predicate);
            freeShelf.AddItem(i);
            freeShelf.SetSpace(freeShelf.GetSpace() + i.GetSpace());
            i.SetShelfId(freeShelf.GetId());
        }

        public Item RemoveItem(int itemId)
        {
            bool predicate(Item i)
            {
                return i.GetId() == itemId;
            }
            Shelf shelf = new Shelf();
            Item itemToRemove = new Item();
            foreach (Shelf s in _shelfList)
            {
                if (s.GetItemsOnShelf().Exists(predicate))
                {
                    itemToRemove = s.GetItemsOnShelf().Find(predicate);
                    shelf = s;
                    break;
                }
            }
            shelf.RemoveItem(itemToRemove);
            return itemToRemove;
        }

        public List<Item> CleanFriger()
        {
            List<Item> ItemsToRemove = new List<Item>();
            DateTime today = DateTime.Today;
            foreach (Shelf s in _shelfList)
            {
                foreach (Item i in s.GetItemsOnShelf())
                {
                    if (i.GetExpiryDate() < today)
                    {
                        ItemsToRemove.Add(i);
                        s.RemoveItem(i);
                    }

                }
            }
            return ItemsToRemove;
        }

        public List<Item> WhatToEat(Item.Kashrut kashrut, Item.Type type)
        {
            List<Item> itemToEat = new List<Item>();
            foreach (Shelf s in _shelfList)
            {
                foreach (Item i in s.GetItemsOnShelf())
                {
                    if (i.GetKashrut() == kashrut && i.GetType() == type && i.GetExpiryDate() >= DateTime.Today)
                    {
                        itemToEat.Add(i);
                    }
                }
            }
            return itemToEat;
        }


        public void readyForShopping()
        {
            if (this.SpaceLeft() >= 20)
            {
                Console.WriteLine("There is space in friger");
                return;
            }
            this.CleanFriger();
            if (this.SpaceLeft() >= 20)
            {
                Console.WriteLine("There is space in friger after cleaning");
                return;
            }
            int freeSpace = this.SpaceLeft();
            List<int> dairyItem = new List<int>();
            List<int> meatyItem = new List<int>();
            List<int> parveItem = new List<int>();
            int dairySpace = 0;
            int meatySpace = 0;
            int parveSpace = 0;

            foreach (Shelf s in _shelfList)
            {
                foreach (Item i in s.GetItemsOnShelf())
                {
                    if (i.GetKashrut() == Item.Kashrut.dairy && i.GetExpiryDate() < DateTime.Today.AddDays(3))
                    {
                        dairyItem.Add(i.GetId());
                        dairySpace += i.GetSpace();
                    }
                    if (i.GetKashrut() == Item.Kashrut.meaty && i.GetExpiryDate() < DateTime.Today.AddDays(7))
                    {
                        meatyItem.Add(i.GetId());
                        meatySpace += i.GetSpace();
                    }
                    if (i.GetKashrut() == Item.Kashrut.parve && i.GetExpiryDate() < DateTime.Today.AddDays(1))
                    {
                        parveItem.Add(i.GetId());
                        parveSpace += i.GetSpace();
                    }
                }
            }
            if (freeSpace + dairySpace >= 20)
            {
                foreach (int id in dairyItem)
                {
                    RemoveItem(id);
                }
                Console.WriteLine("All dairy items were thrown away");
            }
            else if (freeSpace + dairySpace + meatySpace >= 20)
            {
                foreach (int id in dairyItem)
                {
                    RemoveItem(id);
                }
                foreach (int id in meatyItem)
                {
                    RemoveItem(id);
                }
                Console.WriteLine("All dairy and meaty items were thrown away");

            }
            else if (freeSpace + dairySpace + meatySpace + parveSpace >= 20)
            {
                foreach (int id in dairyItem)
                {
                    RemoveItem(id);
                }
                foreach (int id in meatyItem)
                {
                    RemoveItem(id);
                }
                foreach (int id in parveItem)
                {
                    RemoveItem(id);
                }
                Console.WriteLine("All dairy, meaty and parve items were thrown away");

            }
            else
            {
                Console.WriteLine("This is not the time to shop!");
            }
        }

        public List<Shelf> SortShelfBySpace()
        {
            _shelfList = _shelfList.OrderByDescending(o => o.GetSpace()).ToList();
            return _shelfList;
        }
        public string ToString()
        {
            string result = $"id: {this.GetId()}\n model: {this.GetModel()}\n color: {this.GetColor()}\n number of shelves: {this.GetNumOfShelves()}\n shelf list: \n";
            foreach (Shelf s in _shelfList)
            {
                result += s.ToString();
            }
            return result;
        }



    }
}
