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
        private Game mainGame;

        private void timerGameTick_Tick(object sender, EventArgs e)
        {
            mainGame.GameTick(pbMainGame);
           // pbMainGame.Image = mainGame.DrawGame();
        }

        private void ConcenReact_KeyDown(object sender, KeyEventArgs e)
        {
            //Tastendruck weiterreichen an die Spieleinstanz
            mainGame.KeyHandler(e.KeyCode);
        }

        private void pbMainGame_MouseClick(object sender, MouseEventArgs e)
        {
            //Klick-Koordinaten weiterreichen an die Spieleinstanz
            mainGame.ClickGame(e.Location);
        }

        public ConcenReact()
        {
            //Initialize Block
            InitializeComponent();

            InitializeForm();

            //Debug TODO: Normale methode für richtige Runde
            InitializeDebugGame();

            mainGame.DrawGame();
            pbMainGame.Image = mainGame.GetGesamtBitmap();

            timerGameTick.Start();

        }
        private void InitializeDebugGame()
        {
            mainGame = new Game(new Player("Player1",false), new Player("Player2",true), tileSize, gamePbWidth, gamePbHeight);
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
    }
}
