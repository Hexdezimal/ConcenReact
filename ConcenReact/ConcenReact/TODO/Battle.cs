using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConcenReact
{
    class Battle
    {
        Brush backgroundBrush;
        Bitmap gesamt;
        Bitmap hintergrund;
        Graphics g;

        int windowSizeX, windowSizeY;
        int paddingY;    //Padding Y=10% von Y
        int paddingX;    //Padding X=35% von X

        int difficulty;  //0 - Easy; 1 - Normal; 2 - Schwer

        public Battle(int wX,int wY)    //Allg. Konstruktor mit Übergabe der Fenstergröße
        {
            //Größen um den Battle-Screen in der Mitte zu zeichnen
            windowSizeX = wX;
            windowSizeY = wY;
            paddingY = (int)(wY * 0.25);
            paddingX = (int)(wX * 0.45);
    
            difficulty = 0;

            hintergrund = new Bitmap(windowSizeX - paddingX, windowSizeY - paddingY);
            gesamt = new Bitmap(hintergrund.Width,hintergrund.Height);

            backgroundBrush = new SolidBrush(Color.FromArgb(100,Color.DarkGray));

            


        }
        private void NewBattle(Player p)
        {

        }
        public void DrawBattleScreen()
        {
            g = Graphics.FromImage(gesamt);

            g.FillRectangle(backgroundBrush, new RectangleF(0, 0,gesamt.Width, gesamt.Height));

            g.Dispose();
        }
        public Bitmap GetGesamtBitmap()
        {
            return gesamt;
        }
        

    }
}
