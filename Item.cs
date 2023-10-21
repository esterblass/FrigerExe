using System;
using System.Collections.Generic;
using System.Text;

namespace FrigerExe
{
    class Item
    {
        private static int uniqueId = 1;
        public enum Kashrut { meaty, dairy, parve }
        public enum Type { food, drink }
        private int _id;
        private string _name;
        private int _shelfId;
        private Type _type;
        private Kashrut _kashrut;
        private DateTime _expiryDate;
        private int _space;

        public Item() { }

        public Item(string name, Item.Type type, Item.Kashrut kashrut, DateTime expiryDate, int space)
        {
            _id = uniqueId++;
            _name = name;
            _shelfId = 0;
            _type = type;
            _kashrut = kashrut;
            _expiryDate = expiryDate;
            _space = space;

        }
        public DateTime GetExpiryDate() { return _expiryDate; }

        public int GetSpace() { return _space; }
        public int GetId() { return _id;}
        public string GetName(){ return _name; }
        public int GetShelfId() { return _shelfId;}
        public void SetShelfId(int sId){ _shelfId = sId; }
        public Type GetType() {return _type; }
        public Kashrut GetKashrut() { return _kashrut; }

        public string ToString()
        {
            string result = $"id: {this.GetId()} \n name: {this.GetName()}\n shelf id: {this.GetShelfId()}\n type: { this.GetType().ToString()}\n kashrut: {this.GetKashrut().ToString()}\n expiry date: {this.GetExpiryDate()}\n space: {this.GetSpace()} ";                               
            return result;
        }
    }
}
