using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ConcenReact
{

    public class GameConfig
    {
        //Zu serialisierende Einstellungen
        public int windowWidth, windowHeight, border, tilesize;
        public int borderXAmount, borderYAmount,xTiles,yTiles;

        private const string path = "Config\\options.cfg";
        public GameConfig()
        {
            
        }
        public void CreateConfig()
        {
            //Default-Config
            WindowWidth = 800;
            windowHeight = 600;
            border = 15;
            tilesize = 32;
            borderXAmount = 3;
            borderYAmount = 5;
            xTiles = 35;
            yTiles = 20;

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            SaveConfig();
        }
        public static GameConfig LoadConfig()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            StreamReader reader = new StreamReader(path);
            GameConfig temp;
            using (reader)
                temp= (GameConfig)serializer.Deserialize(reader);

            reader.Close();
            return temp;
        }
        public void SaveConfig()
        {

            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream);
            using (writer)
                serializer.Serialize(writer, this);

            writer.Close();
            stream.Close();
        }
        
        public int WindowWidth { get => windowWidth; set => windowWidth = value; }
        public int WindowHeight { get => windowHeight; set => windowHeight = value; }
        public int Border { get => border; set => border = value; }
        public int Tilesize { get => tilesize; set => tilesize = value; }

    }

}
