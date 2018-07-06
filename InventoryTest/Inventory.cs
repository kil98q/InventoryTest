using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace InventoryTest
{
    class Inventory
    {
        public Item[] _inventory = null;
        //public Type[] _typesAllowed = null;
        public List<Item> Whitelist = new List<Item>();

        public Inventory(int Size)
        {
            _inventory = new Item[Size];
        }

        public bool Extract(int InventorySlot, out Item ExtractedItem)
        {
            if(_inventory.Length > InventorySlot || _inventory[InventorySlot] == null)
            {
                ExtractedItem = null;
                return false;
            }
            else
            {
                ExtractedItem = _inventory[InventorySlot];
                _inventory[InventorySlot] = null;
                return true;
            }
        }
        public bool Insert(Item item)
        {
            for(int i = 0; i < _inventory.Length; i++)
            {
                if(_inventory[i] == null && item != null)
                {
                    _inventory[i] = item;
                    return true;
                }
            }
            return false;
        }
        public bool Insert(Item item,int InventorySlot)
        {
            if (_inventory[InventorySlot] != null)
            {
                _inventory[InventorySlot] = item;
                return true;
            }
            return false;
        }
        public bool CheckItemAllowed(Item item)
        {
            // This function will check whitelists and allowed types of items
            return false;
        }
    }
    class Item
    {
        public enum ItemType { Default, Craftable, Weapon };
        public string VanityName = "Im vanity!";
        public readonly string Name = "Not assigned";
        public readonly ItemType Type = ItemType.Default;
        public readonly Bitmap Icon = null;
        
        
        public Item(string VanityName, string Name, ItemType Type, Bitmap Icon = null)
        {
            this.VanityName = VanityName;
            this.Name = Name;
            this.Type = Type;
            this.Icon = Icon;
        }
    }
    class Weapon : Item
    {
        public readonly int Damage = 0;
        public readonly int Speed = 0;
    public Weapon(string VanityName, string Name,int Damage,int Speed) : base(VanityName, Name, ItemType.Weapon)
        {
            this.Damage = Damage;
            this.Speed = Speed;
        }
    }
}
