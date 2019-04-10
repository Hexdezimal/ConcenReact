using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ConcenReact
{
    class VisualMenuEntry : MainMenuEntry
    {
        private string header;
        private Bitmap background;
        private Bitmap gesamt;
        private Graphics gesamtGraphic;
        private Brush menuBrush;
        private bool isVisible;

        private AssetHandler assetHandler;

        public VisualMenuEntry(AssetHandler assetHandler, DebugForm debugForm, int windowSizeX, int windowSizeY, Brush menuBrush, string name, string header) : base(debugForm, name)
        {
            this.Header = header;
            this.MenuBrush = menuBrush;
            Background = new Bitmap(windowSizeX, windowSizeY);
            Gesamt = new Bitmap(Background);
            this.DebugForm = debugForm;
            this.assetHandler = assetHandler;
        }
        public void DrawBackground()
        {
            //Zeichnen des Hintergrunds
            GesamtGraphic = Graphics.FromImage(Background);
            GesamtGraphic.FillRectangle(Brushes.Gray, new Rectangle(0, 0, Gesamt.Width, Gesamt.Height));    //Hintergrund
            GesamtGraphic.FillRectangle(MenuBrush, new Rectangle(0, 0, Gesamt.Width, Gesamt.Height));       //menuBrush über Hintergrund gelegt
            GesamtGraphic.Dispose();
            
        }
        public virtual void KeyHandler(KeyEventArgs e, VisualMenuEntry sender)
        {
            if (DebugForm != null && sender!=null)
                DebugForm.WriteLine(sender.ToString()+" : "+e.KeyCode.ToString());
        }
        public virtual void DrawVisualMenuEntry()
        {
            DrawBackground();
            GesamtGraphic = Graphics.FromImage(Gesamt);
            GesamtGraphic.DrawImage(Background, 0, 0, Background.Width, Background.Height);

            

            GesamtGraphic.Dispose();
        }
        
        public virtual Bitmap GetGesamtBitmap()
        {
            return Gesamt;
        }

        public override void Press()
        {
            base.Press();
            isVisible = true;
        }
        public virtual void Close()
        {
            isVisible = false;
        }

        public string Header { get => header; set => header = value; }
        public bool IsVisible { get => isVisible; set => isVisible = value; }
        public Graphics GesamtGraphic { get => gesamtGraphic; set => gesamtGraphic = value; }
        public Brush MenuBrush { get => menuBrush; set => menuBrush = value; }
        public Bitmap Gesamt { get => gesamt; set => gesamt = value; }
        internal AssetHandler AssetHandler { get => assetHandler; set => assetHandler = value; }
        public Bitmap Background { get => background; set => background = value; }
    }
}
