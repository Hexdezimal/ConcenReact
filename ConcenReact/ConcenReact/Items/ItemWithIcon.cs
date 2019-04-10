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
        public override int DrawDataString(Font textFont, Graphics g, int detailBoxWidth, int detailBoxHeight,int detailBoxX, int detailBoxY)
        {
            int entry = 0; //detailBoxY (obere kante des fensters + entry (Anzahl der Zeile) * Die Höhe der Schriftart

            g.DrawString(Prefix + Name, MethodLib.GetResizedFont(Prefix+Name,textFont,detailBoxWidth), Brushes.White, detailBoxX,detailBoxY+ entry*textFont.Height);

            entry++;
            g.DrawString("Rarity: " + ((ItemRarity)Rarity).ToString(), textFont, assetHandler.Assets.RarityBrushes[Rarity], detailBoxX, detailBoxY+entry * textFont.Height);
            entry++;
            
            return entry;
        }

        internal AssetHandler AssetHandler { get => assetHandler; set => assetHandler = value; }
    }
}
