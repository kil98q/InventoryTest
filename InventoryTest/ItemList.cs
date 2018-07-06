using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTest
{
    class ItemList {
        public static readonly Dictionary<string, Item> ItemsDictionairy = new Dictionary<string, Item>()
        {
            { "banana",new Item("Banana","banana",Item.ItemType.Craftable,Properties.Resources.coin_us_dollar_icon) },
            { "sword",new Weapon("Swuuurd","sword",10,1) }
        };
    }
}
