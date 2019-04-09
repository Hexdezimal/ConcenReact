using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    abstract class VisualMenuEntryOption
    {
        private string title;
        private AssetHandler assetHandler;

        private Size windowSize;
        public VisualMenuEntryOption(AssetHandler asset, Size windowSize)
        {
            AssetHandler = asset;

            this.windowSize = windowSize;
        }
        //Abstrakte Methode
        public abstract void Action(int status, int entryX, int entryY,Graphics g);

        public string Title { get => title; set => title = value; }
        internal AssetHandler AssetHandler { get => assetHandler; set => assetHandler = value; }

        public Size WindowSize { get => windowSize; set => windowSize = value; }
    }
}
