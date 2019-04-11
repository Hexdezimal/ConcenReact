using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    class VisualProfileEditorEntry : VisualMenuEntry
    {
        private int currentMenuItemIndex;
        private int optionButtonWidth, optionButtonHeight;
        private Size windowSize;
        private AssetHandler assetHandler;


        List<VisualMenuEntryOption> entries;
        private Brush entryBackgroundBrush;
        private Brush entryHighlightBrush;

        //Merker für Charaktererstellung
        private Player createdPlayer;
        private string tempName;
        private Bitmap tempBitmap;
        private GameConfig config;

        //Maße für Width/Height
        int mapXTiles, mapYTiles;


        public VisualProfileEditorEntry(GameConfig config,AssetHandler assetHandler, DebugForm debugForm, int windowSizeX, int windowSizeY, Brush menuBrush, string name, string header) : base(assetHandler, debugForm, windowSizeX, windowSizeY, menuBrush, name, header)
        {
            //Spielekonfig Merken zur Bearbeitung
            this.config = config;

            //Aktuell angewählter Menüeintrag
            createdPlayer = new Player("DUMMY", false);
            tempBitmap = createdPlayer.CharacterBitmap;

            currentMenuItemIndex = 0;
            optionButtonWidth = windowSizeX - windowSizeX / 4;
            optionButtonHeight = windowSizeY / 12;

            //Fenstergröße
            windowSize = new Size(windowSizeX, windowSizeY);
            this.assetHandler = assetHandler;

            //Einträge-Liste
            entries = new List<VisualMenuEntryOption>();

            //Hintergrundfarbe erstellen
            CreateEntryBackgroundAndHighlightBrush(menuBrush);

            //Option-Entry
            SetupCharacterBitmapOption();

            mapXTiles = config.xTiles;
            mapYTiles = config.yTiles;

        }
        //Brushes anlegen TODO: assetHandler
        private void CreateEntryBackgroundAndHighlightBrush(Brush menuBrush)
        {

            Color temp = (menuBrush as SolidBrush).Color;
            temp = ControlPaint.Dark(temp, 15);
            entryBackgroundBrush = new SolidBrush(Color.FromArgb(128, temp));

            temp = (MenuBrush as SolidBrush).Color;
            temp = ControlPaint.Light(temp, 15);
            entryHighlightBrush = new SolidBrush(Color.FromArgb(128, temp));
        }
        private void SetupCharacterBitmapOption()
        {
            //Default Width 35 / Default Height 25
            
            entries.Add(new VisualMenuEntryCharacterBitmapOption(assetHandler, windowSize));
            entries.Add(new VisualMenuEntryEnterNameOption(assetHandler, windowSize));
            entries.Add(new VisualMenuEntryChangeIntValueOption(assetHandler, windowSize, config.xTiles, 70, 15,"X-Felder"));
            entries.Add(new VisualMenuEntryChangeIntValueOption(assetHandler, windowSize, config.yTiles, 40, 10,"Y-Felder"));

        }
        public override Bitmap GetGesamtBitmap()
        {
            return Gesamt;
        }
        public override void DrawVisualMenuEntry()
        {
            base.DrawVisualMenuEntry();

            GesamtGraphic = Graphics.FromImage(Gesamt);

            GesamtGraphic.FillRectangle(entryBackgroundBrush, new RectangleF(windowSize.Width * 0.15f, currentMenuItemIndex * optionButtonHeight, optionButtonWidth, optionButtonHeight));
            for (int i=0;i<entries.Count;i++)
            {

                    GesamtGraphic.FillRectangle(entryBackgroundBrush, new RectangleF(windowSize.Width * 0.15f, i * optionButtonHeight , optionButtonWidth, optionButtonHeight));
                


                    GesamtGraphic.DrawString(entries[i].Title, SystemFonts.DefaultFont, Brushes.White, windowSize.Width * 0.15f, i * optionButtonHeight);

            }

            GesamtGraphic.DrawImage(((VisualMenuEntryCharacterBitmapOption)entries[0]).CurrBitmap, windowSize.Width / 2, 0 * optionButtonHeight);

            GesamtGraphic.Dispose();
        }
        public override void Press()
        {
            base.Press();
        }
        public override void Close()
        {
            base.Close();
           // Pressed = false;
        }
        private bool shift;
        public override void KeyHandler(KeyEventArgs e, VisualMenuEntry sender)
        {
            //Übergabe an base-methode für Debug-Output
            base.KeyHandler(e,sender);



            if(e.KeyCode>=Keys.A && e.KeyCode<=Keys.Z)
            {
                if (e.Modifiers == Keys.Shift)
                    shift = true;
                else
                    shift = false;
                //Übergabe der Keys an AppendName der VisualMenuEntryEnterNameOptions
                if(entries[currentMenuItemIndex]!=null)
                {
                    if (entries[currentMenuItemIndex].GetType()==typeof(VisualMenuEntryEnterNameOption))
                    {
                        ((VisualMenuEntryEnterNameOption)entries[currentMenuItemIndex]).AppendName((char)e.KeyCode,shift);
                        tempName = ((VisualMenuEntryEnterNameOption)entries[currentMenuItemIndex]).Name;
                        CreatePlayer();
                    }

                }
            }

            shift = false;
            //Menübewegung
            if(e.KeyCode==Keys.Down)
            {
                if(currentMenuItemIndex+1<entries.Count)
                {
                    currentMenuItemIndex++;
                }
            }
            else if(e.KeyCode == Keys.Up)
            {
                if (currentMenuItemIndex - 1 >=0)
                {
                    currentMenuItemIndex--;
                }
            }


            else if(e.KeyCode==Keys.Enter || e.KeyCode==Keys.Add)
            {

                if (entries[currentMenuItemIndex] != null)
                {
                    GesamtGraphic = Graphics.FromImage(Gesamt);
                    entries[currentMenuItemIndex].Action(1, windowSize.Width / 4, currentMenuItemIndex * optionButtonHeight,GesamtGraphic);

                    GesamtGraphic.Dispose();

                    if (entries[currentMenuItemIndex].GetType() == typeof(VisualMenuEntryCharacterBitmapOption))
                    {
                        //gewählte Bitmap in Merker schreiben
                        tempBitmap = ((VisualMenuEntryCharacterBitmapOption)entries[currentMenuItemIndex]).CurrBitmap;
                        CreatePlayer();
                    }
                    if (entries[currentMenuItemIndex].GetType() == typeof(VisualMenuEntryChangeIntValueOption))
                    {
                        ChangeAndSaveConfig();
                    }
                }
            }
            else if(e.KeyCode==Keys.Back || e.KeyCode==Keys.Subtract)
            {
                if(entries[currentMenuItemIndex]!=null)
                {
                    GesamtGraphic = Graphics.FromImage(Gesamt);
                    //Action des Eintrags aufrufen
                    entries[currentMenuItemIndex].Action(0, windowSize.Width / 4, currentMenuItemIndex * optionButtonHeight, GesamtGraphic);


                    //Abfrage, ob EnterNameOption
                    if (entries[currentMenuItemIndex].GetType() == typeof(VisualMenuEntryEnterNameOption))
                    {
                        //Name kürzen und in Merker schreiben
                        ((VisualMenuEntryEnterNameOption)entries[currentMenuItemIndex]).ShortenName();
                        tempName = ((VisualMenuEntryEnterNameOption)entries[currentMenuItemIndex]).Name;
                        
                    }
                    if (entries[currentMenuItemIndex].GetType() == typeof(VisualMenuEntryCharacterBitmapOption))
                    {
                        //gewählte Bitmap in Merker schreiben
                        tempBitmap = ((VisualMenuEntryCharacterBitmapOption)entries[currentMenuItemIndex]).CurrBitmap;
                        CreatePlayer();
                    }
                    if(entries[currentMenuItemIndex].GetType()==typeof(VisualMenuEntryChangeIntValueOption))
                    {
                        ChangeAndSaveConfig();
                    }

  




                    GesamtGraphic.Dispose();
                }
            }
            else if(e.KeyCode==Keys.Escape)
            {
                
                if (entries[currentMenuItemIndex].GetType() == typeof(VisualMenuEntryCharacterBitmapOption))
                {
                    
                }
            }
        }
        private void ChangeAndSaveConfig()
        {
            //Ändern des eigentlichen Werts
            ChangeIntValueOptionValues();
            //Abfrage auf X oder Y entry
            if (((VisualMenuEntryChangeIntValueOption)entries[currentMenuItemIndex]).Title == "X-Felder: " + mapXTiles)
            {
                config.xTiles = mapXTiles;
            }
            else if (((VisualMenuEntryChangeIntValueOption)entries[currentMenuItemIndex]).Title == "Y-Felder: " + mapYTiles)
            {
                config.yTiles = mapYTiles;
            }
            //Config mit neuen Werten Speichern
            config.SaveConfig();
        }
        public void ChangeIntValueOptionValues()
        {
            if (entries[currentMenuItemIndex].GetType() == typeof(VisualMenuEntryChangeIntValueOption))
            {
                if (((VisualMenuEntryChangeIntValueOption)entries[currentMenuItemIndex]).ValueTitle == "X-Felder")
                    mapXTiles = ((VisualMenuEntryChangeIntValueOption)entries[currentMenuItemIndex]).Value;
                else if ((((VisualMenuEntryChangeIntValueOption)entries[currentMenuItemIndex]).ValueTitle == "Y-Felder"))
                    mapYTiles = ((VisualMenuEntryChangeIntValueOption)entries[currentMenuItemIndex]).Value;
            }
        }
        public Player ResetCreatedPlayer()
        {
            CreatePlayer();
            return createdPlayer;
        }
        private void CreatePlayer()
        {
            createdPlayer = new Player(tempName, false, tempBitmap);
        }
        public int GetMapXTiles()
        {
            ChangeIntValueOptionValues();
            return mapXTiles;
        }
        public int GetMapYTiles()
        {
            ChangeIntValueOptionValues();
            return mapYTiles;
        }
        internal Player CreatedPlayer { get => createdPlayer; set => createdPlayer = value; }
    }
}
