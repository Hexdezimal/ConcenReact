using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    class GetItemInteraction : Interaction
    {
        private Item getItem;   //Item, welches Durch die Interaktion zurückgegeben wird
        public GetItemInteraction(Item getItem, string interName, string interText) : base(interName, interText)
        {
                this.GetItem = getItem;
        }
        public static GetItemInteraction GetRandomItemInteraction(AssetHandler assetHandler)
        {
            GetItemInteraction interaction;
            Item item;

            Random r = new Random();
            int rand = 0;

            if (rand == 0) //Weapon
            {
                interaction = new GetItemInteraction(Weapon.GetRandomWeapon(assetHandler), "Interaktion", "Interaktiontext");
            }
            else
                interaction = new GetItemInteraction(Weapon.GetRandomWeapon(assetHandler), "Interak", "ItnerText");

            

            return interaction;
        }

        public Item Interact()
        {
            Item item;
  
            if (!Interacted)
            {
                item = getItem;

                Interacted = true;
            }
            else
                item = null;

            return item;
        }
      

        internal Item GetItem { get => getItem; set => getItem = value; }
    }
}
