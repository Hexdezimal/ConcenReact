using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    class MethodLib
    {
        public static Font GetResizedFont(string s, Font font,int surfaceWidth)
        {
            Font newFont = new Font(font, FontStyle.Regular);

            if (TextRenderer.MeasureText(s, font).Width>surfaceWidth)
            {
                newFont = new Font("Arial",((float)(surfaceWidth + surfaceWidth * 0.4) / s.Length));
            }

            return newFont;
        }

    }
}
