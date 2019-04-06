using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class GetItemPopupMenu : PopupMenu
    {
        private GetItemInteraction interaction; //Item-Interaktion für das Menü
        public GetItemPopupMenu(GetItemInteraction gii, Brush mB, int wX, int wY,Graphics g,DebugForm df) : base(mB, wX, wY, g,df)
        {
            interaction = gii;
        }
        public string GetItemInteractionString()
        {
            string interactString;
            interactString = interaction.InteractionName + "\n\n\nItem: "+ interaction.GetItem + "\n\n\n";
            interactString += interaction.InteractionText;

            return interactString;
        }
        
        public override void DrawPopup()
        {
            base.DrawPopup();   //Zeichnen der Basisklasse

            DrawHeader(interaction.InteractionName);
        }
    }
}
