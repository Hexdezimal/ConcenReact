using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConcenReact
{
    class Player : Charakter
    {

        private List<int> scores;   //Liste für alle Scores
        private int xPos, yPos;
        private int indexHighscore; //Index des Highscores
        private Guid saveGUID;
        private bool isEnemy;
        private int inventorySpace;

        private Armor armor;
        private Weapon weapon;
        private List<Item> items;

        private int movePower;


        //Debug-Konstruktor
        public Player(string name,bool isEnemy):base(name,Properties.Resources.Character_0_Myrmim,Properties.Resources.Avatar_0_Myrmim)
        {
            scores = new List<int>();
            Items = new List<Item>();

            indexHighscore = -1;
            //Random GUID für Savegames -> TODO SAVEGAME
            saveGUID = Guid.NewGuid();

            //Ob Gegner -> Farbe ändern
            this.IsEnemy = isEnemy;
            
            XPos = 0;
            YPos = 0;
            MovePower = 5;
            InventorySpace = 4;

            if (isEnemy)
            {
                ChangeColorToEnemy();
                XPos = 10;
                YPos = 10;

            }
           
        }

        private void ChangeColorToEnemy()
        {
            //Verweis auf aktuellen Pixel
            Color currPixel;
            //Durchlaufen der Bitmap um Rot-Anteil um 25% zu erhöhen und Blau-Anteil um 45% zu verringern
            for(int y=0;y<CharacterBitmap.Height;y++)
            {
                for(int x=0;x<CharacterBitmap.Width;x++)
                {
                    
                    currPixel = CharacterBitmap.GetPixel(x, y);
                    if (currPixel.R * 1.25 < 255 && currPixel.B * 0.55 >= 0)
                        CharacterBitmap.SetPixel(x, y, Color.FromArgb((int)(currPixel.R + currPixel.R * 0.45), currPixel.G, (int)(currPixel.B - currPixel.B * 0.45)));
                    
                }
            }
            CharacterBitmap.MakeTransparent(Color.Black);
        }
        public void AddWeapon(DebugForm debug,Weapon w)
        {
            if (weapon == null)
            {
                if (debug != null)
                    debug.WriteLine("Waffe ausgerüstet: " + w.Name);
                weapon = w;
            }
            else
            {
                if (debug != null)
                    debug.WriteLine("Waffe ins Inventar gepackt: " + w.Name);
                Items.Add(w);
            }
        }
        public void AddArmor(Armor a)
        {
            if (armor == null)
                armor = a;
            else
                Items.Add(a);
        }
        


        //Properties
        public int XPos { get => xPos; set => xPos = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public bool IsEnemy { get => isEnemy; set => isEnemy = value; }
        public int MovePower { get => movePower; set => movePower = value; }
        internal Armor Armor { get => armor; set => armor = value; }
        internal Weapon Weapon { get => weapon; set => weapon = value; }
        internal List<Item> Items { get => items; set => items = value; }
        public int InventorySpace { get => inventorySpace; set => inventorySpace = value; }
    }
}
