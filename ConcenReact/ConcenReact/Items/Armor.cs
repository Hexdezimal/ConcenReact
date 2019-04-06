using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class Armor : Item
    {
        private double defenseModifier; //Prozentsatz für Verteidigung


        public Armor(Bitmap itemBitmap, bool isUsable, bool isEquipable, string name, double defenseModifier, Bitmap armorBitmap)
            :base(itemBitmap, isUsable,isEquipable,name)
        {
            this.defenseModifier = defenseModifier;
        }
        //Standard-Konstruktor für Leeres Item
        public Armor():base()
        {
            this.defenseModifier = 0.0;
        }
    }
}
