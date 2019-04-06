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

        public VisualMenuEntry(DebugForm debugForm, int windowSizeX, int windowSizeY, Brush menuBrush, string name, string header) : base(debugForm, name)
        {
            this.Header = header;
            this.menuBrush = menuBrush;
            background = new Bitmap(windowSizeX, windowSizeY);
            gesamt = new Bitmap(background);
            this.DebugForm = debugForm;
        }
        public void DrawBackground()
        {
            //Zeichnen des Hintergrunds
            gesamtGraphic = Graphics.FromImage(background);
            gesamtGraphic.FillRectangle(Brushes.Gray, new Rectangle(0, 0, gesamt.Width, gesamt.Height));    //Hintergrund
            gesamtGraphic.FillRectangle(menuBrush, new Rectangle(0, 0, gesamt.Width, gesamt.Height));       //menuBrush über Hintergrund gelegt
            gesamtGraphic.Dispose();
            
        }
        public virtual void KeyHandler(Keys key)
        {
            if (DebugForm != null)
                DebugForm.WriteLine(key.ToString());
        }
        public virtual void DrawVisualMenuEntry(PictureBox pb)
        {
            DrawBackground();
            gesamtGraphic = Graphics.FromImage(gesamt);
            gesamtGraphic.DrawImage(background, 0, 0, background.Width, background.Height);


            pb.Image = gesamt;
        }
        
        public virtual Bitmap GetGesamtBitmap()
        {
            return gesamt;
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
    }
}
