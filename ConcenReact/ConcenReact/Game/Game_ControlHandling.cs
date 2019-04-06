using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace ConcenReact
{
    partial class Game
    {
        public void KeyHandler(Keys keys)
        {
            //Steuerung blockieren

            if (currPlayer == player1 && currPlayerMovePwr > 0 && !inBattle && !inOptions)
            {
                gameMap.Tiles[player1.XPos, player1.YPos].LeaveTile(player1);
                if (keys == Keys.Right || keys == Keys.D)
                {
                    if (player1.XPos + 1 < pbSizeX / tileSize)
                    {
                        if (gameMap.Tiles[player1.XPos + 1, player1.YPos].IsEnterable)
                        {
                            player1.XPos++;
                            currPlayerMovePwr--;
                        }

                    }
                }
                if (keys == Keys.Up || keys == Keys.W)
                {
                    if (player1.YPos - 1 >= 0)
                    {
                        if (gameMap.Tiles[player1.XPos, player1.YPos - 1].IsEnterable)
                        {
                            player1.YPos--;
                            currPlayerMovePwr--;

                        }

                    }
                }
                if (keys == Keys.Down || keys == Keys.S)
                {
                    if (player1.YPos + 1 < pbSizeY / tileSize)
                    {
                        if (gameMap.Tiles[player1.XPos, player1.YPos + 1].IsEnterable)
                        {
                            player1.YPos++;
                            currPlayerMovePwr--;
                        }

                    }
                }
                if (keys == Keys.Left || keys == Keys.A)
                {
                    if (player1.XPos - 1 >= 0)
                    {
                        if (gameMap.Tiles[player1.XPos - 1, player1.YPos].IsEnterable)
                        {
                            player1.XPos--;
                            currPlayerMovePwr--;
                        }

                    }
                }
                gameMap.Tiles[player1.XPos, player1.YPos].EnterTile(player1);
            }
            /*
             * NUR TEMPORÄR UM SPIELER2 MANUELL ZU STEUERN
             * 
             * /*/
            else
            {
                gameMap.Tiles[currPlayer.XPos, currPlayer.YPos].LeaveTile(currPlayer);
                if (keys == Keys.Right || keys == Keys.D)
                {
                    if (currPlayer.XPos + 1 < pbSizeX / tileSize)
                    {
                        if (gameMap.Tiles[currPlayer.XPos + 1, currPlayer.YPos].IsEnterable)
                        {
                            currPlayer.XPos++;
                            currPlayerMovePwr--;
                        }

                    }
                }
                if (keys == Keys.Up || keys == Keys.W)
                {
                    if (currPlayer.YPos - 1 >= 0)
                    {
                        if (gameMap.Tiles[currPlayer.XPos, currPlayer.YPos - 1].IsEnterable)
                        {
                            currPlayer.YPos--;
                            currPlayerMovePwr--;

                        }

                    }
                }
                if (keys == Keys.Down || keys == Keys.S)
                {
                    if (currPlayer.YPos + 1 < pbSizeY / tileSize)
                    {
                        if (gameMap.Tiles[currPlayer.XPos, currPlayer.YPos + 1].IsEnterable)
                        {
                            currPlayer.YPos++;
                            currPlayerMovePwr--;
                        }

                    }
                }
                if (keys == Keys.Left || keys == Keys.A)
                {
                    if (currPlayer.XPos - 1 >= 0)
                    {
                        if (gameMap.Tiles[currPlayer.XPos - 1, currPlayer.YPos].IsEnterable)
                        {
                            currPlayer.XPos--;
                            currPlayerMovePwr--;
                        }

                    }
                }
                gameMap.Tiles[currPlayer.XPos, currPlayer.YPos].EnterTile(currPlayer);
            }
            /*            /*
             *            
               ^^ WIRD NOCH ENTFERNT

            */
            
            //Inventar des aktuellen Spielers anzeigen
            if (keys == Keys.I)
            {
                if (!inAction())
                {
                    inInventory = true;
                }
                else
                    inInventory = false;
            }
            //TEST-BATTLESCREEN TODO: KAMPFKLASSE  
            if (keys == Keys.B)
            {
                if (!inAction())
                    inBattle = true;
                else
                    inBattle = false;
            }

            //TEST-Rundenwechsel zwischen P1/P2
            if (keys == Keys.C)
            {
                ChangeTurn();
            }

            //Interagieren
            if (keys == Keys.Enter)
            {
                //Abfrage ob der Nutzer bereits in einer Aktion ist
                if (!inAction())
                {
                    //Temp. Tile-Verweis zu weiteren Bearbeitung
                    Tile eventTile = gameMap.Tiles[currPlayer.XPos, currPlayer.YPos];

                    //Überprüfen, ob das Tile eine Interaktion besitzt
                    if (eventTile.GetInteraction() != null)
                    {
                        //Abfrage ob es interagierbar ist und noch nicht damit interagiert wurde
                        if (eventTile.IsInteractable && !eventTile.GetInteraction().Interacted)
                        {
                            //Überprüfung, um welche Interaktionsart es sich handelt
                            if (eventTile.GetInteraction().GetType() == typeof(GetItemInteraction))    //GetItemInteraktion
                            {

                                //Überprüfung ob Die GetItemInteraction eine Waffe als Rückgabe hat
                                isInteracting = true;





                                Item item = ((GetItemInteraction)eventTile.GetInteraction()).GetItem;
                                //Überprüfung auf Typ des Items
                                if (item.GetType() == typeof(Weapon))
                                {

                                    //currPlayer.AddWeapon(debugForm,(Weapon)item);
                                    eventTile.Interact(debugForm);

                                }
                            }
                        }
                        else
                            isInteracting = false;  //Zurücksetzen des Status bei nicht zutreffend

                    }
                }
                else
                    isInteracting = false;  //Zurücksetzen des Status bei nicht zutreffend
            }
        }
    }
}
