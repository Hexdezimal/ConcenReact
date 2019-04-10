﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
    ConcenReact IT17a BBS1-KL

    Dennis Ebel,
    Kevin Groß,
    Oliver Hoffmann
    Richard McDonald,
    Richard Sodke,
*/
namespace ConcenReact
{
    public partial class ConcenReact : Form
    {
        private int gamePbWidth, gamePbHeight, border, tileSize;
        private int xTiles, yTiles;

        private Game mainGame;
        private DebugForm debugForm;
        private AssetHandler assetHandler;

        private MainMenu mainMenu;
        private MainMenuEntry lastClickedEntry;

        private bool inMainMenu;
        private bool gameInitialized;
        private bool mainMenuInitialized;

        private bool debug;
        public ConcenReact()
        {
            //Initialize Assets
            assetHandler = new AssetHandler();

            //Initialize Block
            InitializeComponent();

            //DEBUG
            debug = true;
            DebugMode();

            inMainMenu = true;
            gameInitialized = false; //Spiellogik erst nach Menü initialisieren
            mainMenuInitialized = false;



            timerGameTick.Start();

        }
        private void DebugMode()
        {
            if (debug)
            {
                debugForm = new DebugForm();
                debugForm.Show();

            }
            else debugForm = null;
        }
        private void timerGameTick_Tick(object sender, EventArgs e)
        {
            //Abfrage ob in Hauptmenü
            if(!inMainMenu)
            {
                //Abfrage, ob erster Game-Start
                if(!gameInitialized)
                {
                    StartGame();
                    this.Focus();
                }
                mainGame.GameTick(pbMainGame);
            }
            else
            {
                //Abfrage, ob erster Menü-Start
                if(!mainMenuInitialized)
                {
                    StartMainMenu();
                    
                }
                mainMenu.MainMenuTick(pbMainGame);


                if (mainMenu.StartGameMenuEntryPressed() )
                {
                    inMainMenu = false;
                    lastClickedEntry = mainMenu.Entries[mainMenu.CurrentMenuEntry];


                }
                if(mainMenu.CloseMenuEntryPressed() )
                {
                    this.Close();
                    lastClickedEntry = mainMenu.Entries[mainMenu.CurrentMenuEntry];
                }
                if(mainMenu.VisualProfileEditorEntryPressed())
                {
                    ((VisualProfileEditorEntry)mainMenu.Entries[mainMenu.CurrentMenuEntry]).DrawVisualMenuEntry();
                    pbMainGame.Image = ((VisualProfileEditorEntry)mainMenu.Entries[mainMenu.CurrentMenuEntry]).Gesamt;

                    lastClickedEntry = mainMenu.Entries[mainMenu.CurrentMenuEntry];

                }


            }

        }
        public void Reset()
        {
            inMainMenu = true;
            gameInitialized = false; //Spiellogik erst nach Menü initialisieren

            //Reset MainMenu Settings


            //mainMenuInitialized = false;
            InitializeFormMainMenu();

            if (mainMenu!=null)
            {
                mainMenu.InVisualProfileEditor1 = false;
                mainMenu.ResetPressed();
            }
            
            if (lastClickedEntry.GetType()==typeof(VisualProfileEditorEntry))
            {
                ((VisualProfileEditorEntry)lastClickedEntry).Close();    //Visuelles Menü schließen
            }


        }
        private void ConcenReact_KeyDown(object sender, KeyEventArgs e)
        {
            //Tastendruck weiterreichen an die Spieleinstanz
            if (e.KeyCode != Keys.Escape )
            {
                if (!inMainMenu)
                {
                    mainGame.KeyHandler(e.KeyCode);
                }
                else
                {
                    if(mainMenu.InVisualProfileEditor1)
                    {
                        ((VisualProfileEditorEntry)lastClickedEntry).KeyHandler(e, ((VisualProfileEditorEntry)lastClickedEntry));
                    }
                    else
                        mainMenu.KeyHandler(e);
                }
            }
            else
            {
                    Reset();
            }
        }

        private void pbMainGame_MouseClick(object sender, MouseEventArgs e)
        {
            //Klick-Koordinaten weiterreichen an die Spieleinstanz
            if(!inMainMenu)
            {
                mainGame.ClickGame(e.Location);

            }
        }
        private void StartGame()
        {
            InitializeForm();
            InitializeDebugGame();
            //pbMainGame.Image = mainGame.GetGesamtBitmap();
            gameInitialized = true;
        }
        private void StartMainMenu()
        {
            InitializeFormMainMenu();
            InitializeDebugMainMenu();
            mainMenu.AddEntry(new VisualProfileEditorEntry(assetHandler, debugForm, Size.Width, Size.Height, mainMenu.MenuBrush, "Profil", "Profil"));
            mainMenuInitialized = true;

        }
        private void InitializeDebugGame()
        {
            //Wenn Spiel vorhanden war -> Bitmaps freigeben
            if(mainGame!=null)
                mainGame.DisposeBitmaps();

            //Abfrage, ob Menü geöffnet wurde, ansonsten Default-WErter
            if(mainMenu.GetXYTiles().Width>0 && mainMenu.GetXYTiles().Height>0)
            {
                gamePbWidth = mainMenu.GetXYTiles().Width * tileSize;
                gamePbHeight = mainMenu.GetXYTiles().Height * tileSize;
                RefreshFormLayout();

            }
            //Spiel-Objekt erstellen
            mainGame = new Game(assetHandler, debugForm,debug,mainMenu.GetCreatedPlayer(), new Player("Player2",true), tileSize, gamePbWidth, gamePbHeight);

        }
        private void InitializeDebugMainMenu()
        {
            mainMenu = new MainMenu(assetHandler, debugForm,gamePbWidth, gamePbHeight);
        }
        //Initialisieren der Form für das Hauptmenü
        private void InitializeFormMainMenu()
        {
            border = 15;
            tileSize = 32;

            gamePbWidth = 800;
            gamePbHeight = 600;

            this.Size = new Size(gamePbWidth + border * 3, gamePbHeight + border * 5);
            pbMainGame.Location = new Point(border, border);
        }
        private void InitializeForm()
        {
            //Festlegen der default Fenstergrößen - Anpassung für komplettes Game

            border = 15;
            tileSize = 32;

            xTiles = 35;
            yTiles = 20;

            gamePbWidth = xTiles*tileSize;
            gamePbHeight = yTiles*tileSize;
            this.Size = new Size(gamePbWidth+border*3, gamePbHeight+border*5);

            pbMainGame.Location = new Point(border,border);
            
        }
        private void InitializeForm(int xTiles, int yTiles)
        {
            border = 15;
            tileSize = 32;

            gamePbWidth = xTiles * tileSize;
            gamePbHeight = yTiles * tileSize;
            this.Size = new Size(gamePbWidth + border * 3, gamePbHeight + border * 5);

            pbMainGame.Location = new Point(border, border);
        }
        private void RefreshFormLayout()
        {
            border = 15;
            tileSize = 32;

            this.Size = new Size(gamePbWidth + border * 3, gamePbHeight + border * 5);
            pbMainGame.Location = new Point(border, border);
        }
    }
}
