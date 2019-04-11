using System;
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

        //Konfiguration / Technik
        private Game mainGame;
        private DebugForm debugForm;
        private AssetHandler assetHandler;
        private GameConfig config;


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

            //GameConfig Laden
            InitConfig();

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
        private void InitConfig()
        {
            config = new GameConfig();
            try
            {
                config = GameConfig.LoadConfig();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error beim Laden der Config!\nVerwende Standard-Einstellungen..");
                config.CreateConfig();
            }
            //config = ;
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
            
            mainMenu.AddEntry(new VisualProfileEditorEntry(config, assetHandler, debugForm, Size.Width-border*3, Size.Height-border*5, mainMenu.MenuBrush, "Profil", "Profil"));
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
            mainMenu = new MainMenu(config, assetHandler, debugForm,gamePbWidth, gamePbHeight);
        }
        //Initialisieren der Form für das Hauptmenü
        private void InitializeFormMainMenu()
        {
            border = config.border;
            tileSize = config.tilesize;

            gamePbWidth = 800;
            gamePbHeight = 600;

            this.Size = new Size(gamePbWidth + config.border * 3, gamePbHeight + config.border * 5);
            pbMainGame.Location = new Point(config.border, config.border);
        }
        private void InitializeForm()
        {
            //Form initialisieren mit Daten aus Config

            border = config.border;
            tileSize = config.tilesize;

            xTiles = config.borderXAmount;
            yTiles = config.borderYAmount;

            gamePbWidth = xTiles*config.tilesize;
            gamePbHeight = yTiles*config.tilesize;
            this.Size = new Size(gamePbWidth+config.border*3, gamePbHeight+config.border*5);

            pbMainGame.Location = new Point(border,border);
            
        }

        private void RefreshFormLayout()
        {
            border = config.border;
            tileSize = config.tilesize;

            this.Size = new Size(gamePbWidth + config.border * 3, gamePbHeight + config.border * 5);
            pbMainGame.Location = new Point(config.border, config.border);
        }
    }
}
