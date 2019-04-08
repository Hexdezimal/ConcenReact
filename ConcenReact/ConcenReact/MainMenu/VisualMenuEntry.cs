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
            background = new Bitmap(windowSizeX, windowSizeY);
            Gesamt = new Bitmap(background);
            this.DebugForm = debugForm;
            this.assetHandler = assetHandler;
        }
        public void DrawBackground()
        {
            //Zeichnen des Hintergrunds
            GesamtGraphic = Graphics.FromImage(background);
            GesamtGraphic.FillRectangle(Brushes.Gray, new Rectangle(0, 0, Gesamt.Width, Gesamt.Height));    //Hintergrund
            GesamtGraphic.FillRectangle(MenuBrush, new Rectangle(0, 0, Gesamt.Width, Gesamt.Height));       //menuBrush über Hintergrund gelegt
            GesamtGraphic.Dispose();
            
        }
        public virtual void KeyHandler(Keys key, VisualMenuEntry sender)
        {
            if (DebugForm != null && sender!=null)
                DebugForm.WriteLine(sender.ToString()+" : "+key.ToString());
        }
        public virtual void DrawVisualMenuEntry(PictureBox pb)
        {
            DrawBackground();
            GesamtGraphic = Graphics.FromImage(Gesamt);
            GesamtGraphic.DrawImage(background, 0, 0, background.Width, background.Height);

            
            pb.Image = Gesamt;
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
    }
}
