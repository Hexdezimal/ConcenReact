using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    class VisualMenuEntryEnterNameOption : VisualMenuEntryOption
    {
        private bool entering;
        private string name;


        public VisualMenuEntryEnterNameOption(AssetHandler asset, Size windowSize) : base(asset, windowSize)
        {
            Title = "Name: ";
            Name = "";
           
        }
        public void SetTitle()
        {
            Title = "Name: " + Name;
        }
        public void AppendName(char c,bool shift)
        {
            

            if (!shift)
                c = Char.ToLower(c);

            Name += c;
            SetTitle();
        }
        public void ShortenName()
        {
            if (Name.Length > 1)
                Name = Name.Substring(0, Name.Length - 1);
            else
                Name = "";

            SetTitle();
        }
        public override void Action(int status, int entryX, int entryY, Graphics g)
        {
            SwitchStatusEntering();
        }
        //Status von entering invertieren
        public void SwitchStatusEntering()
        {
            if (entering)
                entering = false;
            else
                entering = true;
        }
        public string Name { get => name; set => name = value; }
    }
}
