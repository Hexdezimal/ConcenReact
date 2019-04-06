using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class Charakter
    {
        private string name; //Charakternamen
        private Bitmap characterBitmap;
        private Bitmap avatarBitmap;

        public Charakter(string name, Bitmap characterBitmap,Bitmap avatarBitmap)
        {
            this.name = name;
            //Kopien der Bitmaps erstellen
            this.characterBitmap = new Bitmap(characterBitmap);
            this.avatarBitmap = new Bitmap(avatarBitmap);
            
        }

        public string Name { get => name; set => name = value; }
        public Bitmap CharacterBitmap { get => characterBitmap; set => characterBitmap = value; }
        public Bitmap AvatarBitmap { get => avatarBitmap; set => avatarBitmap = value; }
    }
}
