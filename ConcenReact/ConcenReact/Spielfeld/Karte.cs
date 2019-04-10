using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class Karte
    {
        private Tile[,] tiles;
        private int xTiles,yTiles, tileSize;
        public Karte(int xTiles, int yTiles, int tileSize)
        {
            //Festlegen der Tile-Anzahl
            this.XTiles = xTiles;
            this.YTiles = yTiles;
            this.tileSize = tileSize;
            Tiles = new Tile[xTiles, yTiles];
        }
        public static Tile[,] GenerateRandomTiles(AssetHandler assetHandler, int xTiles,int yTiles, DebugForm debugForm)
        {
            Tile[,] tiles = new Tile[xTiles,yTiles];
            Random rSalt = new Random();
            for(int y=0;y<yTiles;y++)
            {
                for(int x=0;x<xTiles;x++)
                {
                    tiles[x, y] = Tile.GetRandomTile(assetHandler, debugForm,rSalt.Next()+assetHandler.Rand.Next());

                }
            }

            return tiles;
        }

        public Bitmap GetMapBitmap()
        {
            Bitmap karte = new Bitmap(xTiles * tileSize, yTiles * tileSize);
            Graphics gKarte = Graphics.FromImage(karte);

            for (int y = 0; y < yTiles; y++)
            {
                for (int x = 0; x < xTiles; x++)
                {
                    gKarte.DrawImage(tiles[x, y].TileBitmap, x * TileSize, y * TileSize, tileSize, tileSize);
                }
            }


            return karte;
        }


        //Properties
        public int XTiles { get => xTiles; set => xTiles = value; }
        public int YTiles { get => yTiles; set => yTiles = value; }

        internal Tile[,] Tiles { get => tiles; set => tiles = value; }
        public int TileSize { get => tileSize; set => tileSize = value; }
    }
}
