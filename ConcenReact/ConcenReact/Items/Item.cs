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
        private string prefix;
        private string description;
        private int rarity;


        public Item(Bitmap itemBitmap, bool isUsable, bool isEquipable, string name,string prefix, int rarity)
        {
            this.ItemBitmap = new Bitmap(itemBitmap);
            this.IsUsable = isUsable;
            this.IsEquipable = isEquipable;
            this.Name = name;
            this.rarity = rarity;
            this.prefix = prefix;
            this.description = "Flavor-Text_01";
        }
        public Item()
        {
            this.ItemBitmap = new Bitmap(Properties.Resources.Item_Sword_0_ManniKatt.Width, Properties.Resources.Item_Sword_0_ManniKatt.Height);
            this.IsUsable = false;
            this.IsEquipable = false;
            this.Name = "Nichts";
            this.rarity = 0;
            this.prefix = "";
            this.description = "Flavor-Text_01";
        }
        public abstract string GetDataAsString();
        public abstract int DrawDataString(Font textFont,Graphics g,int detailBoxWidth, int detailBoxHeight, int detailBoxX, int detailBoxY);
        
        public string Name { get => name; set => name = value; }
        public Bitmap ItemBitmap { get => itemBitmap; set => itemBitmap = value; }
        public bool IsUsable { get => isUsable; set => isUsable = value; }
        public bool IsEquipable { get => isEquipable; set => isEquipable = value; }
        public int Rarity { get => rarity; set => rarity = value; }
        public string Prefix { get => prefix; set => prefix = value; }
        public string Description { get => description; set => description = value; }
    }
}
