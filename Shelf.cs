using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrigerExe
{
    class Shelf
    {
        private static int uniqueId = 1;
        private int _id;
        private int _floorOfShelf;
        private int _space;
        private List<Item> _itemsOnShelf;
        public Shelf(int floorOfShelf, int space)
        {
            _id = uniqueId++;
            _floorOfShelf = floorOfShelf;
            _space = space;
            //_itemsOnShelf = itemsOnShelf;
            _itemsOnShelf = new List<Item>();
        }

        public Shelf() { }
        public int GetId() { return _id; }
        public int GetFloorOfShelf() { return _floorOfShelf; }
        public int GetSpace() { return _space; }
        public List<Item> GetItemsOnShelf() { return _itemsOnShelf; }
        
        public void SetSpace(int spc) { _space = spc; }

        public void AddItem(Item i)
        {
            if (i.GetSpace() > _space)
            {
                Console.WriteLine("There is no space for the item, try to add the item to other shelf");
            }
            else
            {
                i.SetShelfId(_id);
                _space += i.GetSpace();
                _itemsOnShelf.Add(i);
            }
        }
        public void RemoveItem(Item i)
        {
            _itemsOnShelf.Remove(i);
            i.SetShelfId(0);
            this.SetSpace(_space + i.GetSpace());
        }

        public int SpaceLeft()
        {
            int spaceAvailable = _space;
            foreach (Item i in _itemsOnShelf)
            {
                spaceAvailable = spaceAvailable - i.GetSpace();
            }
            return spaceAvailable;
        }

        public List<Item> SortItemByExpiryDate()
        {
            _itemsOnShelf = _itemsOnShelf.OrderBy(o => o.GetExpiryDate()).ToList();
            return _itemsOnShelf;
        }
        public string ToString()
        {
            string result = $"id: {this.GetId()}\n floor of shelf: {this.GetFloorOfShelf()}\n space: {this.GetSpace()}\n items on shelves: ";
            foreach ( Item i in _itemsOnShelf)
            {
                result += i.ToString();
            }
            //string r = _itemsOnShelf.ForEach(i => i.ToString());
            return result;

        }
    }
}
