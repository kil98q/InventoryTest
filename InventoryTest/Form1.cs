using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryTest
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Setup();
        }
        private FormInventory invent;
        private FormInventory invent2;

        private void button1_Click(object sender, EventArgs e)
        {
            if (invent.inventory.Insert(ItemList.ItemsDictionairy["banana"]))
            {
                Console.WriteLine("Insertion Succesfull :D");
                invent.Refresh();
            }
            else
            {
                Console.WriteLine("Failed to add item to inventory :<");
            }
            
            
        }
        public void Setup()
        {
            invent2 = new FormInventory(new Inventory(10), flowLayoutPanel2);
            invent = new FormInventory(new Inventory(10),flowLayoutPanel1);
            invent.Slots[0].item = ItemList.ItemsDictionairy["banana"];
            invent.Refresh();
        }
        
    }
}
