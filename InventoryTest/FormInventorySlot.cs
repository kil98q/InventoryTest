using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace InventoryTest
{
    class FormInventorySlot
    {
        public PictureBox PictureBox = new PictureBox();
        public Item item = null;

        public FormInventorySlot(Item item = null)
        {
            this.item = item;
            PictureBox.AllowDrop = true;
            PictureBox.Size = new Size(32, 32);
            PictureBox.BackgroundImage = Properties.Resources.inventoryslot;
            PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private Bitmap GeneratePlaceholderImage()
        {
            Bitmap Picture = new Bitmap(16, 16);
            Graphics pGraphics = Graphics.FromImage(Picture);
            pGraphics.Clear(Color.Purple);
            pGraphics.Dispose();
            return Picture;
        }
        public void Refresh()
        {
            Console.WriteLine("Refreshing slot " + PictureBox.GetHashCode());
            if (item != null)
            {
                if (item.Icon != null)
                {
                    PictureBox.Image = item.Icon;
                }
                else
                {
                    PictureBox.Image = GeneratePlaceholderImage();
                }
            }else
            {
                PictureBox.Image = null;
            }
            
        }

    }
    class FormInventory
    {
        public Inventory inventory = null;
        public List<FormInventorySlot> Slots = new List<FormInventorySlot>();
        private FlowLayoutPanel container = null;

        public FormInventory(Inventory inventory,FlowLayoutPanel container)
        {
            Console.WriteLine("setting inventory up");
            this.container = container;
            this.inventory = inventory;
            foreach(Item item in inventory._inventory)
            {
                Console.WriteLine("New slot!");
                FormInventorySlot Temp = new FormInventorySlot(item);
                Slots.Add(Temp);
                Temp.PictureBox.DragEnter += SlotDragEnter;
                Temp.PictureBox.DragDrop += SlotDragDrop;
                Temp.PictureBox.MouseDown += SlotMouseDown;
                
                container.Controls.Add(Temp.PictureBox);

            }
            Refresh();
        }
        public void Refresh()
        {
            Console.WriteLine("Refreshing inventory");
            for (int i = 0; i < Slots.LongCount();i++)
            {
                Slots[i].item = inventory._inventory[i];
                Slots[i].Refresh();
            }
        }
        
        public void SlotMouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            FormInventorySlot CurrentSlot = FindSlot(pb);
            pb.Select();

            if (CurrentSlot != null && CurrentSlot.item != null)
            {
                DataObject data = new DataObject(DataFormats.Serializable, CurrentSlot);
                pb.DoDragDrop(data, DragDropEffects.Copy);
            }
        }
        private FormInventorySlot FindSlot(PictureBox Picturebox)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].PictureBox.Equals(Picturebox))
                {
                    Console.WriteLine("Found it element " + i);
                    return Slots[i];
                }
            }
            return null;
        }
        private void SlotDragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void SlotDragDrop(object sender, DragEventArgs e)
        {
            Console.WriteLine("Dropped!");
            FormInventorySlot Source = (FormInventorySlot)e.Data.GetData(DataFormats.Serializable);
            FormInventorySlot ThisSlot = FindSlot((PictureBox)sender);
            if (ThisSlot.PictureBox != Source.PictureBox)
            {
                ThisSlot.item = Source.item;
                Source.item = null;
                Source.Refresh();
                ThisSlot.Refresh();
                Console.WriteLine("ItemTransferred!");
            }
        }
    }
}
