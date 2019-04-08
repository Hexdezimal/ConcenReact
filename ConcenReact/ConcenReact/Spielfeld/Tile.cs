using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConcenReact
{
    class Tile
    {
        private bool isEnterable;
        private bool isInteractable;
        private Bitmap tileBitmap;
        private string tileName;
        private Player currPlayerOnTile;
        private Interaction interaction;
        
        

        public static Tile GetRandomTile(AssetHandler assetHandler,DebugForm debugForm, int salt)
        {
            Random tempRand = new Random(salt);
            Tile randTile = new Tile();

            int rand = tempRand.Next(0, 1000);

            if (debugForm != null)
                debugForm.WriteLine("Random-Value: " + rand);
            
            if(rand<250)
            {
                randTile.isEnterable = true;
                randTile.isInteractable = false;
                randTile.tileBitmap = assetHandler.Assets.TileIcons[(int)Tiles.Plain_0];
                randTile.tileName = "Plain_0";
            }
            else if(rand<500)
            {
                randTile.isEnterable = true;
                randTile.isInteractable = false;
                randTile.tileBitmap = assetHandler.Assets.TileIcons[(int)Tiles.Plain_1];
                randTile.tileName = "Plain_1";
            }
            else if(rand<750)
            {
                randTile.isEnterable = true;
                randTile.isInteractable = false;
                randTile.tileBitmap = assetHandler.Assets.TileIcons[(int)Tiles.Plain_2];
                randTile.tileName = "Plain_2";
            }
            else if(rand<965)
            {
                randTile.isEnterable = true;
                randTile.isInteractable = false;
                randTile.tileBitmap = assetHandler.Assets.TileIcons[(int)Tiles.Plain_3];
                randTile.tileName = "Plain_3";
            }
            else if(rand<985)
            {
                randTile.isEnterable = true;
                randTile.isInteractable = true;
                randTile.tileBitmap = assetHandler.Assets.TileIcons[(int)Tiles.Plain_House_0];
                randTile.tileName = "Plain_House_0";

                //randTile.interaction = new GetItemInteraction(Weapon.GetRandomWeapon(assetHandler),"Dorfbesuch","Willkommen im Dummy-Dorf, hier ist ein Item-Test!");
                randTile.interaction = GetItemInteraction.GetRandomItemInteraction(assetHandler);
                
            }
            else if(rand<998)
            {
                randTile.isEnterable = true;
                randTile.isInteractable = true;
                randTile.tileBitmap = assetHandler.Assets.TileIcons[(int)Tiles.Plain_House_1];
                randTile.tileName = "Plain_House_1";
            }
            else if(rand<1000)
            {
                randTile.isEnterable = true;
                randTile.isInteractable = false;
                randTile.tileBitmap = assetHandler.Assets.TileIcons[(int)Tiles.Plain_Castle_0];
                randTile.tileName = "Plain_Castle_0";
            }
            else
            {
                randTile.isEnterable = true;
                randTile.isInteractable = true;
                randTile.tileBitmap = assetHandler.Assets.TileIcons[(int)Tiles.Dummy];
                randTile.tileName = "DUMMY";
            }



            return randTile;
        }
        public void EnterTile(Player p)
        {
            currPlayerOnTile = p;
            isEnterable = false;
        }
        public void LeaveTile(Player p)
        {
            currPlayerOnTile = null;
            isEnterable = true;
        }
        public String GetDebugTileString()
        {
            String tileString;

            tileString = "---\nTile-Filename: " + tileName+"\n";
            tileString += "isEnterable: " + isEnterable+"\n";
            tileString += "isInteractable: " + isInteractable + "\n";

            if(interaction!=null)
                tileString += "Interacted: " + interaction.Interacted + "\n\n";


            if (currPlayerOnTile != null)
            {
                tileString += "Player: " + currPlayerOnTile.Name+"\n";
                tileString += "isEnemy: " + currPlayerOnTile.IsEnemy + "\n";
                tileString += "X: " + currPlayerOnTile.XPos + "   Y:" + currPlayerOnTile.YPos+"\n";

            }
            else
                tileString += "Player: NONE";
            tileString += "---END\n";

            return tileString;
        }
        public void Interact(DebugForm debugForm)
        {
            if(interaction.GetType()==typeof(GetItemInteraction) && !interaction.Interacted)
            {
                currPlayerOnTile.AddEquipWeapon(debugForm,(Weapon)((GetItemInteraction)interaction).Interact());
                interaction.Interacted = true;
            }
        }
        public Interaction GetInteraction()
        {
            return interaction;
        }

        //Properties
        public bool IsEnterable { get => isEnterable; set => isEnterable = value; }
        public bool IsInteractable { get => isInteractable; set => isInteractable = value; }
        public Bitmap TileBitmap { get => tileBitmap; set => tileBitmap = value; }
        public string TileName { get => tileName; set => tileName = value; }
    }
}
