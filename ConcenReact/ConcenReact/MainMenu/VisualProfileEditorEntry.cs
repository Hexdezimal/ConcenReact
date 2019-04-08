using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    class VisualProfileEditorEntry : VisualMenuEntry
    {
        private int currentMenuItemIndex;
        public VisualProfileEditorEntry(AssetHandler assetHandler, DebugForm debugForm, int windowSizeX, int windowSizeY, Brush menuBrush, string name, string header) : base(assetHandler, debugForm, windowSizeX, windowSizeY, menuBrush, name, header)
        {
            currentMenuItemIndex = 0;
        }
        
        public override void DrawVisualMenuEntry(PictureBox pb)
        {
            base.DrawVisualMenuEntry(pb);
            GesamtGraphic = Graphics.FromImage(Gesamt);




            GesamtGraphic.Dispose();
        }
        public override void KeyHandler(Keys key, VisualMenuEntry sender)
        {
            base.KeyHandler(key,sender);

        }
    }
}
