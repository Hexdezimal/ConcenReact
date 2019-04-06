using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class Weapon : Item
    {
        private double attackModifier;  //Prozentsatz für Schadensbonus
        private double scoreModifier;   //Prozentsatz für Score-Bonus
        

        public Weapon(Bitmap itemBitmap, bool isUsable, bool isEquipable, string name, double attackModifier, double scoreModifier)
            : base(itemBitmap, isUsable, isEquipable, name)
        {
            this.attackModifier = attackModifier;
            this.scoreModifier = scoreModifier;
        }
        //Standard-Konstruktor für leeres Item
        public Weapon():base()
        {
            this.attackModifier = 0.0;
            this.scoreModifier = 0.0;
        }
    }
}
