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
        private int gameTime;

        //Player1 = Mensch || Player2 = CPU
        private Player player1;
        private Player player2;
        private InventoryPopupMenu invenPopUp;

        //Battle-Objekt
        private Battle battle;

        private Player currPlayer;
        private int currPlayerMovePwr;

        //Spielkarte
        private Karte gameMap;

        //Form für die Debug-Konsole
        private DebugForm debugForm;

        //zuletzt geklickte Position
        private Point lastClickedPos;

        //Größe von tiles und der picturebox
        private int tileSize, pbSizeX, pbSizeY;

        //Bitmaps 
        private Bitmap background;
        private Bitmap gesamt;
        private Graphics gesamtGraphics;
        private Brush menuBrush;

        //Status-Abfragen für aktionen
        private bool inBattle;
        private bool inOptions;
        private bool isInteracting;
        private bool inInventory;
        private bool debug;
        private bool inMainMenu;


        public Game(DebugForm debugForm,bool debug, Player p1, Player p2, int tS,int pX, int pY)
        {
            //Debug-Form
            this.debug = debug;
            this.debugForm = debugForm;
            //Spielerzuweisung
            player1 = p1;
            Player2 = p2;
            

            currPlayer = player1;
            currPlayerMovePwr = player1.MovePower;

            inBattle = false;
            inOptions = false;
            isInteracting = false;
            inInventory = false;


            //Festlegung der Tilesize und der Pixelgröße des zu zeichnenden bereichs
            tileSize = tS;
            pbSizeX = pX;
            pbSizeY = pY;
            battle = new Battle(pbSizeX, pbSizeY);


            //Random Map
            gameMap = new Karte(pbSizeX / tileSize, pbSizeY / tileSize, tileSize);
            gameMap.Tiles = Karte.GenerateRandomTiles(pbSizeX / tileSize, pbSizeY / tileSize,null);
            background = gameMap.GetMapBitmap();

            //Gesamt-Bitmap
            gesamt = new Bitmap(background.Width, background.Height);

           
            menuBrush = new SolidBrush(Color.FromArgb(100, Color.DarkBlue));
            CheckPlayerOnTile();
            
            
        }

        //Zusammenfassung der Aktionen
        private bool inAction()
        {
            bool action;

            if (isInteracting || inBattle || inOptions || inInventory)
                action = true;
            else
                action = false;

            return action;
        }
        private void ChangeTurn()
        {
            //Runde wechseln;
            if(currPlayer==player1)
            {
                currPlayer = player2;
                currPlayerMovePwr = player2.MovePower;
            }
            else
            {
                currPlayer = player1;
                currPlayerMovePwr = player1.MovePower;
            }
        }
        private void CheckPlayerOnTile()
        {
            //Setzen der besuchten tiles
            gameMap.Tiles[player1.XPos, player1.YPos].EnterTile(player1);
            gameMap.Tiles[player2.XPos, player2.YPos].EnterTile(player2);
        }
        public void ClickGame(Point clickPos)
        {
            //Tile-Information in Debug-Konsole
            if(debugForm!=null)
                debugForm.WriteLine(gameMap.Tiles[clickPos.X / tileSize, clickPos.Y / tileSize].GetDebugTileString());


            lastClickedPos.X = clickPos.X / tileSize;
            lastClickedPos.Y = clickPos.Y / tileSize;
        }
        public void GameTick(PictureBox gamePB)
        {
            DrawGame();
            gamePB.Image = GetGesamtBitmap();

            //DEBUG TODO: ENTFERNEN
            if(debug)
                currPlayerMovePwr = 99;
        }
        
        public Bitmap GetGesamtBitmap()
        {
            return gesamt;
        }
        private void DrawPlayer(Player player)
        {
            //Spieler zeichnen
            gesamtGraphics.DrawImage(player.CharacterBitmap, player.XPos * tileSize, player.YPos * tileSize, player.CharacterBitmap.Width, player.CharacterBitmap.Height);
            DrawHpBar(player);
        }
        private void DrawHpBar(Player player)
        {
            //HP-Balken     TODO: MAGIC-VALUES ersetzen
            gesamtGraphics.FillRectangle(menuBrush, player.XPos * tileSize, 2 + tileSize + player.YPos * tileSize, player.CharacterBitmap.Width, 8);
            gesamtGraphics.FillRectangle(Brushes.Green, 2 + player.XPos * tileSize, 4 + tileSize + player.YPos * tileSize, (tileSize / player.HpMax * player.Hp) - 2, 4);
        }
        public void DrawGame()
        {
            gesamtGraphics = Graphics.FromImage(gesamt);
            if(!inOptions && !inBattle)
            {
                //Hintergrund zeichnen
                gesamtGraphics.DrawImage(background, 0, 0, pbSizeX, pbSizeY);

                //Charaktere zeichnen
                DrawPlayer(player1);
                DrawPlayer(player2);


                //Click-Marker zeichnen
                gesamtGraphics.DrawRectangle(Pens.White, new Rectangle(lastClickedPos.X * tileSize, lastClickedPos.Y * tileSize, tileSize, tileSize));



                //Aktuelle Tile
                Tile tempTile = gameMap.Tiles[currPlayer.XPos, currPlayer.YPos];

                //Indikator für Interaktion, überprüfen ob Interagierbar + Interaktion nicht leer
                if(tempTile.IsInteractable && tempTile.GetInteraction()!=null)
                {
                    Brush tempBrush;
                    //Farbe je nach Interaktionsstatus
                    if (!tempTile.GetInteraction().Interacted)
                        tempBrush = Brushes.MediumVioletRed;
                    else
                        tempBrush = Brushes.Gray;
 
                    gesamtGraphics.FillEllipse(tempBrush, currPlayer.XPos * tileSize, currPlayer.YPos * tileSize, tileSize / 4, tileSize / 4);
                }

                //Debug-Menü mit Tile-Informationen
                gesamtGraphics.FillRectangle(menuBrush, new Rectangle(0,pbSizeY-tileSize*4,tileSize*8,tileSize*4));
                gesamtGraphics.DrawString(GetSubMenuString(tempTile), SystemFonts.DefaultFont, Brushes.White,0,pbSizeY-tileSize*4);
                tempTile = null;

            }
            //Stop Hintergrund
            if(inBattle)
            {
                battle.DrawBattleScreen();
                gesamtGraphics.DrawImage(battle.GetGesamtBitmap(), pbSizeX/4, pbSizeY/8, battle.GetGesamtBitmap().Width, battle.GetGesamtBitmap().Height);
                
            }
            //Wenn mit etwas interagiert wird (Dorf-Dialog geöffnet etc)
            if(isInteracting)
            {
                Tile tempTile = gameMap.Tiles[currPlayer.XPos, currPlayer.YPos];

                //Erneute Überprüfung auf Vorhandensein des Events zur sicherheit
                if(tempTile.GetInteraction()!=null)
                {
                    //Überprüfung auf Art der Interaktion
                    if (tempTile.GetInteraction().GetType() == typeof(GetItemInteraction))
                    {

                        //Temporäres Pop-Up zur Darstellung
                        GetItemPopupMenu itemPopup = new GetItemPopupMenu((GetItemInteraction)tempTile.GetInteraction(), menuBrush, pbSizeX, pbSizeY,gesamtGraphics,debugForm);

                        //Anzeige des Pop-Ups
                        itemPopup.DrawPopup();
                        
                        
                    }
                

                }
            }
            //Abfrage, ob Spieler das Inventar offen hat
            if(inInventory)
            {
                //Festlegene des Inventar-Pop-Ups des Spielers, welcher am Zug ist
                invenPopUp = new InventoryPopupMenu(currPlayer, menuBrush, pbSizeX, pbSizeY, gesamtGraphics,debugForm);
                //Anzeigen des Pop-Ups
                invenPopUp.DrawPopup();
            }

            gesamtGraphics.Dispose();
        }
        private String GetSubMenuString(Tile tempTile)
        {
            String sMenu;

            //Current Player Data
            sMenu = "\nCurrent Player: " + currPlayer.Name + "\n";
            sMenu += "Moves Left: " + currPlayerMovePwr + "\n";

            //Tile-Informationen
            sMenu += "Interactable: " + tempTile.IsInteractable + "\n";
            if (tempTile.GetInteraction() != null)
                sMenu += "Interacted: " + tempTile.GetInteraction().Interacted+"\n";
            sMenu += "isInteracting: " + isInteracting + "\n";

            sMenu += "Tilename: " + tempTile.TileName + "\n";
            return sMenu;
        }
        public void DebugClose()
        {
            //Freigeben von Resourcen
            debugForm.Close();
        }
        //Properties
        public int GameTime { get => gameTime; set => gameTime = value; }
        internal Player Player1 { get => player1; set => player1 = value; }
        internal Player Player2 { get => player2; set => player2 = value; }
    }
}
