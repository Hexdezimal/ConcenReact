using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class Armor : ItemWithIcon
    {
        private double defenseModifier; //Prozentsatz für Verteidigung


        public Armor(AssetHandler assetHandler, Bitmap itemBitmap, bool isUsable, bool isEquipable, string name,string prefix, double defenseModifier, int rarity)
            :base(assetHandler, itemBitmap, isUsable,isEquipable,name,prefix,rarity)
        {
            this.defenseModifier = defenseModifier;
        }
        public Armor(ItemWithIcon item,double defenseModifier):base(item)
        {
            this.defenseModifier = defenseModifier;
        }
        public override string GetDataAsString()
        {
            string data;
            data = base.GetDataAsString();

            data += "Defense Mod.: " + defenseModifier  + "%\n";
            return data;
        }
        //Standard-Konstruktor für Leeres Item
        public Armor():base()
        {
            this.defenseModifier = 0.0;
        }
    }
}
