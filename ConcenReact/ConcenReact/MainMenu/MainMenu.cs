using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    class MainMenu
    {
        Bitmap background;
        Bitmap gesamt;
        Graphics gesamtGraphics;
        Brush menuBrush;
        Brush highlightBrush;
        private int currentMenuEntry;
        private int menuEntries;

        private int entryHeight;
        private int entryWidth;
        private int padding;

        private bool inVisualEntry;

        private List<MainMenuEntry> entries;

        public MainMenu(DebugForm debugForm, int pbWidth, int pbHeight)
        {

            background = new Bitmap(pbWidth, pbHeight);
            gesamt = new Bitmap(background);
            
            //Liste für Einträge
            Entries = new List<MainMenuEntry>();
            currentMenuEntry = 0;

            //Höhe der einträge + Padding
            entryHeight = pbHeight / 8;
            entryWidth = pbWidth / 4;
            padding = entryHeight / 4;

            //Aktion
            InVisualEntry = false;

            //Brushes für Menu-Einträge und wenn eins markiert ist
            menuBrush = new SolidBrush(Color.FromArgb(100,Color.Blue));
            highlightBrush = new SolidBrush(Color.FromArgb(100, Color.LightBlue));

            //DEBUG
            AddEntry(new StartGameMenuEntry(debugForm,"Start"));
            AddEntry(new CloseMenuEntry(debugForm,"Beenden"));
            AddEntry(new VisualMenuEntry(debugForm,pbWidth, pbHeight, menuBrush, "VisualTest", "TITELZEILE"));

            CreateBackground();
        }
        public void ResetPressed()
        {
            foreach(MainMenuEntry e in entries)
            {
                e.Pressed = false;
            }

        }
        public bool StartGameMenuEntryPressed()
        {
            bool temp = false;
            foreach(MainMenuEntry e in entries)
            {
                if(e.GetType()==typeof(StartGameMenuEntry))
                {
                    if (e.Pressed)
                        temp = true;
                }
            }
            return temp;
        }
        public bool CloseMenuEntryPressed()
        {
            bool temp = false;
            foreach (MainMenuEntry e in entries)
            {
                if (e.GetType() == typeof(CloseMenuEntry))
                {
                    if (e.Pressed)
                        temp = true;
                }
            }
            return temp;
        }
        public bool VisualMenuEntryPressed()
        {
            bool temp = false;
            
            foreach(MainMenuEntry e in entries)
            {
                if(e.GetType()==typeof(VisualMenuEntry))
                {
                    if (e.Pressed)
                    {
                        temp = true;
                        InVisualEntry = true;
                    }
                }
            }
            
            return temp;
        }

        public void AddEntry(MainMenuEntry entry)
        {
            Entries.Add(entry);
            menuEntries++;
            currentMenuEntry = 0;
        }
        public void MainMenuTick(PictureBox pb)
        {

            DrawMainMenu();
            DrawEntries();
            DrawMarkedField();

            pb.Image = GetGesamt();
        }

        private Bitmap GetGesamt()
        {
            return gesamt;
        }
        private void DrawMarkedField()
        {
            if(currentMenuEntry>-1)
            {
                gesamtGraphics.FillRectangle(highlightBrush, new Rectangle(background.Width / 4,(background.Height/2)+(currentMenuEntry*entryHeight)+entryHeight, entryWidth,entryHeight));
            }
        }
        private void DrawEntries()
        {
            for(int i=0;i<Entries.Count;i++)
            {
                gesamtGraphics.FillRectangle(menuBrush, new Rectangle(background.Width / 4, (background.Height / 2) + (i* entryHeight)+entryHeight, entryWidth, entryHeight));
                gesamtGraphics.DrawString(Entries[i].Name, new Font("Arial", 35, FontStyle.Bold), Brushes.White, new PointF(background.Width / 4, (background.Height / 2) + (i * entryHeight)+entryHeight));
            }
        }
        private void DrawMainMenu()
        {
            gesamtGraphics = Graphics.FromImage(gesamt);
            gesamtGraphics.DrawImage(background, 0, 0,background.Width,background.Height);


            
        }
        private void CreateBackground()
        {
            gesamtGraphics = Graphics.FromImage(background);

            gesamtGraphics.FillRectangle(Brushes.DarkGray, new Rectangle(0, 0, background.Width, background.Height));
            gesamtGraphics.DrawString("ConcenReact", new Font("Arial",25,FontStyle.Bold), Brushes.White, (background.Width / 2 ) - "ConcenReact".Length, background.Height / 4);

            gesamtGraphics.Dispose();
        }
        public void KeyHandler(Keys key)
        {
            if(key == Keys.Down)
            {
                if(InVisualEntry)
                {
                    ((VisualMenuEntry)entries[currentMenuEntry]).KeyHandler(key);
                }
                else
                {
                    if (currentMenuEntry + 1 < Entries.Count && !InVisualEntry)
                        currentMenuEntry++;

                }
            }
            if(key==Keys.Up)
            {
                if(InVisualEntry)
                {
                    ((VisualMenuEntry)entries[currentMenuEntry]).KeyHandler(key);
                }
                else
                {
                    if (currentMenuEntry - 1 >= 0 && !InVisualEntry)
                        currentMenuEntry--;

                }
            }
            if(key==Keys.Enter)
            {
                if(currentMenuEntry>-1)
                {
                    entries[currentMenuEntry].Press();
                }
            }
        }
        public void Close()
        {
            gesamt.Dispose();
            background.Dispose();
            gesamtGraphics.Dispose();

        }
        public int MenuEntries { get => menuEntries; set => menuEntries = value; }
        public int CurrentMenuEntry { get => currentMenuEntry; set => currentMenuEntry = value; }
        public int EntryHeight { get => entryHeight; set => entryHeight = value; }
        public int EntryWidth { get => entryWidth; set => entryWidth = value; }
        public int Padding { get => padding; set => padding = value; }
        internal List<MainMenuEntry> Entries { get => entries; set => entries = value; }
        public bool InVisualEntry { get => inVisualEntry; set => inVisualEntry = value; }
    }
}
