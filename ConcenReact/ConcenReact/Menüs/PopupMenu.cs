using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class PopupMenu
    {
        Brush menuBrush;
        
        int windowSizeX, windowSizeY;
        int popupSizeX, popupSizeY;
        int paddingY;    //Padding Y=10% von Y
        int paddingX;    //Padding X=35% von X

        float headerPositionY;
        Font headerFont;
        Font textFont;

        Graphics context;


        public PopupMenu(Brush mB, int wX, int wY, Graphics g, DebugForm df)
        {
            menuBrush = mB;
            WindowSizeX = wX;
            WindowSizeY = wY;

            //Ränder des gezeichneten Fensters
            paddingY = (int)(wY * 0.20);
            paddingX = (int)(wX * 0.55);


            //Größe des gezeichneten Fensters
            PopupSizeX = WindowSizeX - paddingX;
            PopupSizeY = WindowSizeY - paddingY;

            //Fonts
            headerFont = new Font("Arial", (float)(popupSizeY*0.05),FontStyle.Underline);
            textFont = new Font("Arial",(float)(popupSizeY*0.03));
            HeaderPositionY = (float)(PopupSizeY * 0.15);



            Context = g;
        }
        public void DrawHeader(string header)
        {
            Context.DrawString(header, HeaderFont, Brushes.White, GetHeaderPositionX(header), HeaderPositionY);
            
        }
        //Rückgabe des Rectangles des Fensters
        public Rectangle GetMenuRectangle()
        {
            Rectangle rect = new Rectangle(WindowSizeX / 4, WindowSizeY / 8, PopupSizeX,PopupSizeY);

            return rect;
        }
        //Rückgabe des Float-Rectangles des Fensters
        public RectangleF GetMenuRectangleF()
        {
            RectangleF rect = new RectangleF(WindowSizeX / 4, WindowSizeY / 8, PopupSizeX, PopupSizeY);


            return rect;
        }
        //Zeichnen des Fensters
        public virtual void DrawPopup()
        {
            Context.FillRectangle(menuBrush, GetMenuRectangleF());
        }
        //X-Position der Überschrift anhand der Fenstergröße zurückgeben
        public float GetHeaderPositionX(string s)
        {
            return PopupSizeX - (s.Length * HeaderFont.Size) / 4;
        }
        public void DrawSeperatorLine(Pen p, float y)
        {
            Context.DrawLine(p, GetMenuRectangleF().X, y, GetMenuRectangleF().Right, y);
            //Context.DrawLine(new Pen(Color.DarkGray, itemBitmapSize / 16), GetMenuRectangle().X, yPosItemBitmap + itemBitmapSize * 1.5f, GetMenuRectangleF().Right, yPosItemBitmap + itemBitmapSize * 1.5f);
        }

        //Properties
        public Graphics Context { get => context; set => context = value; }
        public int WindowSizeX { get => windowSizeX; set => windowSizeX = value; }
        public int WindowSizeY { get => windowSizeY; set => windowSizeY = value; }

        public int PopupSizeY { get => popupSizeY; set => popupSizeY = value; }
        public int PopupSizeX { get => popupSizeX; set => popupSizeX = value; }
        public Font HeaderFont { get => headerFont; set => headerFont = value; }
        public Font TextFont { get => textFont; set => textFont = value; }
        public float HeaderPositionY { get => headerPositionY; set => headerPositionY = value; }

    }
}
