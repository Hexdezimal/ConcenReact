using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    abstract class ItemWithIcon : Item
    {
        AssetHandler assetHandler;


        public ItemWithIcon(AssetHandler assetHandler,Bitmap itemBitmap, bool isUsable, bool isEquipable, string name, string prefix, int rarity) : base(itemBitmap, isUsable, isEquipable, name,prefix, rarity)
        {
            this.AssetHandler = assetHandler;
        }
        public ItemWithIcon(ItemWithIcon item)
        {
            this.assetHandler = item.assetHandler;
            this.ItemBitmap = item.ItemBitmap;
            this.IsUsable = item.IsUsable;
            this.IsEquipable = item.IsEquipable;
            this.Name = item.Name;
        }
        public ItemWithIcon():base()
        {

        }
        public override string GetDataAsString()
        {
            string data;

            data = Prefix + Name+"\n"; //Name
            data += "Rarity: " + Rarity+"\n";

            return data;
        }

        internal AssetHandler AssetHandler { get => assetHandler; set => assetHandler = value; }
    }
}
