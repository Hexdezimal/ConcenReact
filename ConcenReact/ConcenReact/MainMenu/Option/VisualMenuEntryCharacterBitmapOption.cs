using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    class VisualMenuEntryCharacterBitmapOption : VisualMenuEntryOption
    {
        private Bitmap currBitmap;
        private int iconCount;
        private int iconIndex;
        private float iconSize;


        public VisualMenuEntryCharacterBitmapOption(AssetHandler assetHandler,Size windowSize):base(assetHandler, windowSize)
        {
            iconIndex = 0;
            iconCount = assetHandler.Assets.CharacterBitmaps.Count;
            iconSize = windowSize.Width * 0.1f;


            SetCurrentBitmap();
            Refresh();


        }

        public Bitmap CurrBitmap { get => currBitmap; set => currBitmap = value; }

        public override void Action(int status, int entryX, int entryY, Graphics g)
        {
            //Status, der Übergeben wird (Reaktion auf 1 - weiter oder 0 - zurück 
            if(status==1)
            {

                if(iconIndex+1<iconCount)
                {

                    iconIndex++;
                    Refresh();

                }
            }
            else if(status==0)
            {
                if(iconIndex-1>=0)
                {
                    iconIndex--;
                    Refresh();

                }
            }
            // g.DrawImage(AssetHandler.Assets.CharacterBitmaps[iconIndex], entryX, entryY);
            SetCurrentBitmap();
        }
        private void SetCurrentBitmap()
        {
            currBitmap = AssetHandler.Assets.CharacterBitmaps[iconIndex];
        }
        private void Refresh()
        {
            SetTitle();
        }
        private void SetTitle()
        {
            Title = ((CharacterIcons)iconIndex).ToString();
        }

    }
}
