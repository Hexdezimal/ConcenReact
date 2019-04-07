using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    abstract class PopupMenu
    {
        
        int windowSizeX, windowSizeY;
        int popupSizeX, popupSizeY;
        int paddingY;    //Padding Y=10% von Y
        int paddingX;    //Padding X=35% von X

        float itemBitmapSize;
        int tileSize;

        float headerPositionY;
        Font headerFont;
        Font textFont;

        Brush menuBrush;
        Brush highlightBrush;
        Graphics context;
        Brush iconBackgroundBrush;
        List<Pen> rarityPens;
        private DebugForm debugForm;

        public PopupMenu(Brush mB, Brush iconBg,List<Pen> rarityPens, int wX, int wY, Graphics g, DebugForm df)
        {
            //Brushes und Pens
            MenuBrush = mB;
            iconBackgroundBrush = iconBg;
            this.RarityPens = rarityPens;

            //Fenstergröße
            WindowSizeX = wX;
            WindowSizeY = wY;

            //Ränder des gezeichneten Fensters
            paddingY = (int)(wY * 0.20);
            paddingX = (int)(wX * 0.55);


            //Größe des gezeichneten Popup-Fensters
            PopupSizeX = WindowSizeX - paddingX;
            PopupSizeY = WindowSizeY - paddingY;

            //Fonts
            headerFont = new Font("Arial", (float)(popupSizeY*0.05),FontStyle.Underline);
            textFont = new Font("Arial",(float)(popupSizeY*0.03));
            HeaderPositionY = (float)(PopupSizeY * 0.15);
            debugForm = df;

            //Menü-Farbe als Highlight farbe in hell anzeigen
            CreateHighlightBrush();

            //Grafikkontext
            Context = g;
        }
        private void CreateHighlightBrush()
        {
            Color temp = (menuBrush as SolidBrush).Color;
            temp = ControlPaint.Light(temp,50);
            highlightBrush = new SolidBrush(Color.FromArgb(128,temp));
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
            Context.FillRectangle(MenuBrush, GetMenuRectangleF());
        }
        //X-Position der Überschrift anhand der Fenstergröße zurückgeben
        public float GetHeaderPositionX(string s)
        {
            return PopupSizeX - (s.Length * HeaderFont.Size) / 4;
        }
        public void DrawSeperatorLine(Pen p, float y)
        {
            Context.DrawLine(p, GetMenuRectangleF().X, y, GetMenuRectangleF().Right, y);
            
        }

        /*
         * Abstrakte Methoden
         * */ 
        //Keyhandler für Steuerung im Pop-up
        public abstract void KeyHandler(Keys key);
        //Größe der Elemente setzen
        public abstract void SetBitmapAndTileSize(Bitmap bmp);


        //Properties
        public Graphics Context { get => context; set => context = value; }
        public int WindowSizeX { get => windowSizeX; set => windowSizeX = value; }
        public int WindowSizeY { get => windowSizeY; set => windowSizeY = value; }

        public int PopupSizeY { get => popupSizeY; set => popupSizeY = value; }
        public int PopupSizeX { get => popupSizeX; set => popupSizeX = value; }
        public Font HeaderFont { get => headerFont; set => headerFont = value; }
        public Font TextFont { get => textFont; set => textFont = value; }
        public float HeaderPositionY { get => headerPositionY; set => headerPositionY = value; }
        public Brush IconBackgroundBrush { get => iconBackgroundBrush; set => iconBackgroundBrush = value; }
        public float ItemBitmapSize { get => itemBitmapSize; set => itemBitmapSize = value; }
        public int TileSize { get => tileSize; set => tileSize = value; }
        public List<Pen> RarityPens { get => rarityPens; set => rarityPens = value; }
        public DebugForm DebugForm { get => debugForm; set => debugForm = value; }
        public Brush HighlightBrush { get => highlightBrush; set => highlightBrush = value; }
        public Brush MenuBrush { get => menuBrush; set => menuBrush = value; }
    }
}
