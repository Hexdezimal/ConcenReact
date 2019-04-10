using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class Weapon : ItemWithIcon
    {
        private double attackModifier;  //Prozentsatz für Schadensbonus
        private double scoreModifier;   //Prozentsatz für Score-Bonus


        public Weapon(AssetHandler assetHandler, Bitmap itemBitmap, bool isUsable, bool isEquipable, string name,string prefix, double attackModifier, double scoreModifier, int rarity)
            : base(assetHandler, itemBitmap, isUsable, isEquipable, name,prefix, rarity)
        {
            this.attackModifier = attackModifier;
            this.scoreModifier = scoreModifier;
            
        }
        public Weapon(ItemWithIcon item,double attackModifier, double scoreModifier) : base(item)
        {
            this.attackModifier = attackModifier;
            this.scoreModifier = scoreModifier;
        }
        public static Weapon GetRandomWeapon(AssetHandler assetHandler)
        {
            //Zufallsgenerator initialisieren

            Random r = new Random(assetHandler.Rand.Next());

            int rand;

            //temporäre Merker
            Bitmap icon;
            string name;
            int rarity;
            double attackModifier;
            double scoreModifier;

            try
            {
                rand = r.Next(0,assetHandler.WeaponIconsCount);

                
                icon = assetHandler.Assets.WeaponIcons[rand];

                rand = r.Next(0, assetHandler.WeaponNamesCount);
                name = assetHandler.Assets.WeaponNames[rand];

                rand = r.Next(0, 50);
                attackModifier = (double)rand / 10;

                int combScore= rand;
                rand = r.Next(0, 50);
                scoreModifier = (double)rand /10;

                combScore += rand;

                //Kombinierter Score für Abfrage nach Rarity
           

                if (combScore < 55)  //common
                {
                    rarity = 0;
                }
                else if (combScore < 79) //rare
                {
                    rarity = 1;
                }
                else if (combScore < 88) //very rare
                {
                    rarity = 2;
                }
                else if (combScore < 94) //epic 
                {
                    rarity = 3;
                }
                else if (combScore < 97.5) //legendary
                {
                    rarity = 4;
                }
                else if (combScore < 100) //unique
                {
                    rarity = 5;
                    attackModifier += 0.25;
                    scoreModifier += 0.25;
                }
                else
                    rarity = 0;

                
                
            }
            catch(Exception e)
            {
                icon = assetHandler.Assets.WeaponIcons[(int)ItemWeapons.Eisenschwert];
                name = assetHandler.Assets.WeaponNames[(int)ItemWeapons.Eisenschwert];
                rarity = 0;
                attackModifier = 0.0;
                scoreModifier = 0.0;
                

            }
            rand = r.Next(0,assetHandler.Assets.RarityPrefix[rarity].Count);

            Weapon w = new Weapon(assetHandler, icon, false, true, name,assetHandler.Assets.RarityPrefix[rarity][rand], attackModifier, scoreModifier, rarity);
            
            return w;
            
        }
        public override int DrawDataString(Font textFont, Graphics g, int detailBoxWidth, int detailBoxHeight, int detailBoxX, int detailBoxY)
        {
            int entry = base.DrawDataString(textFont, g, detailBoxWidth, detailBoxHeight, detailBoxX,detailBoxY);     //detailBoxY (obere kante des fensters + entry (Anzahl der Zeile) * Die Höhe der Schriftart
            g.DrawString("Attack Mod.: " + attackModifier + "%", textFont, Brushes.White, detailBoxX,detailBoxY+ entry * textFont.Height);
            entry++;
            g.DrawString("Score Mod.: " + scoreModifier + "%",textFont, Brushes.White, detailBoxX, detailBoxY + entry * textFont.Height);
            entry++;
            
            return entry;

        }
        public override string GetDataAsString()
        {
            string data;
           
            data =  base.GetDataAsString();

            data += "Attack Mod.: " + attackModifier + "%"+"\n";
            data += "Score Mod.: " + scoreModifier  + "%" + "\n";

            return data;
        }

        //Standard-Konstruktor für leeres Item
        public Weapon() :base()
        {
            
            this.attackModifier = 0.0;
            this.scoreModifier = 0.0;
        }
    }
}
