using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    abstract class Item
    {
        private bool isUsable;          //Ob Konumierbar
        private bool isEquipable;       //Ob ausrüstbar
        private Bitmap itemBitmap;
        private string name;           



        public Item(Bitmap itemBitmap, bool isUsable, bool isEquipable, string name)
        {
            this.ItemBitmap = new Bitmap(itemBitmap);
            this.isUsable = isUsable;
            this.isEquipable = isEquipable;
            this.Name = name;
     
        }
        public Item()
        {
            this.ItemBitmap = new Bitmap(Properties.Resources.Item_Sword_0_ManniKatt.Width, Properties.Resources.Item_Sword_0_ManniKatt.Height);
            this.isUsable = false;
            this.isEquipable = false;
            this.Name = "Nichts";
        }

        public string Name { get => name; set => name = value; }
        public Bitmap ItemBitmap { get => itemBitmap; set => itemBitmap = value; }
    }
}
