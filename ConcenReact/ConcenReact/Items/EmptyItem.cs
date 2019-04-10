using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class EmptyItem : Item
    {
        public EmptyItem():base()
        {

        }
        public override int DrawDataString(Font textFont, Graphics g, int detailBoxWidth, int detailBoxHeight, int detailBoxX, int detailBoxY)
        {
            return -1;
        }
        public override string GetDataAsString()
        {
            return "";
        }
    }
}
