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



        public VisualProfileEditorEntry(AssetHandler assetHandler, DebugForm debugForm, int windowSizeX, int windowSizeY, Brush menuBrush, string name, string header) : base(assetHandler, debugForm, windowSizeX, windowSizeY, menuBrush, name, header)
        {
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
        }
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
            entries.Add(new VisualMenuEntryCharacterBitmapOption(assetHandler, windowSize));
            entries.Add(new VisualMenuEntryEnterNameOption(assetHandler, windowSize));

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
        public override void KeyHandler(Keys key, VisualMenuEntry sender)
        {
            //Übergabe an base-methode für Debug-Output
            base.KeyHandler(key,sender);

            if(key>=Keys.A && key<=Keys.Z)
            {

                //Übergabe der Keys an AppendName der VisualMenuEntryEnterNameOptions
                if(entries[currentMenuItemIndex]!=null)
                {
                    if (entries[currentMenuItemIndex].GetType()==typeof(VisualMenuEntryEnterNameOption))
                    {
                        ((VisualMenuEntryEnterNameOption)entries[currentMenuItemIndex]).AppendName((char)key);
                        tempName = ((VisualMenuEntryEnterNameOption)entries[currentMenuItemIndex]).Name;
                        CreatePlayer();
                    }

                }
            }


            //Menübewegung
            if(key==Keys.Down)
            {
                if(currentMenuItemIndex+1<entries.Count)
                {
                    currentMenuItemIndex++;
                }
            }
            else if(key==Keys.Up)
            {
                if (currentMenuItemIndex - 1 >=0)
                {
                    currentMenuItemIndex--;
                }
            }


            else if(key==Keys.Enter || key==Keys.Add)
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
                    //Character erstellen
                }
            }
            else if(key==Keys.Back || key==Keys.Subtract)
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
                        CreatePlayer();
                    }


                    //Character erstellen

                    GesamtGraphic.Dispose();
                }
            }
            else if(key==Keys.Escape)
            {
                
                if (entries[currentMenuItemIndex].GetType() == typeof(VisualMenuEntryCharacterBitmapOption))
                {
                    
                }
            }
        }
        private void CreatePlayer()
        {
            createdPlayer = new Player(tempName, false, tempBitmap);
        }
        internal Player CreatedPlayer { get => createdPlayer; set => createdPlayer = value; }
    }
}
