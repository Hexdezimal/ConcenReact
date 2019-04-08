using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
   
    class AssetHandler
    {
        InitializeAssets assets;
        private int weaponIconsCount;
        private int weaponNamesCount;
        
        private int tilesCount;
        private Random rand;
        public AssetHandler()
        {
            Assets = new InitializeAssets();

            weaponIconsCount = assets.WeaponIcons.Count;
            weaponNamesCount = assets.WeaponNames.Count;
            tilesCount = assets.TileIcons.Count;
            rand = new Random();
        }



        public int WeaponIconsCount { get => weaponIconsCount; set => weaponIconsCount = value; }
        public int WeaponNamesCount { get => weaponNamesCount; set => weaponNamesCount = value; }
        public int TilesCount { get => tilesCount; set => tilesCount = value; }
        public Random Rand { get => rand; set => rand = value; }
        internal InitializeAssets Assets { get => assets; set => assets = value; }
    }
    public enum ItemWeapons
    {
        Eisenschwert,
        Lanzenbrecher,
        Shamshir,
        Katana,
        Leichtschwert,
        Breitschwert,
    }
    public enum ItemUniqueWeapons
    {
        ManniKatti,
        Exodus,
        RunenSchwert,
    }

    public enum Tiles
    {
        Dummy,
        Plain_0,
        Plain_1,
        Plain_2,
        Plain_3,
        Plain_House_0,
        Plain_House_1,
        Plain_Castle_0,
    }

    public enum ItemRarity
    {
        Common,
        Rare,
        VeryRare,
        Epic,
        Legendary,
        Unique,
    }
    public enum ItemPrefixCommon
    {
        Eisen,
        Schwaches,
        Stark_Beschaedigtes,
    }
    public enum ItemPrefixRare
    {
        Stahl,
        Leicht_Beschaedigtes,
    }
    public enum ItemPrefixVeryRare
    {
        Neues,
        Starkes,
    }
    public enum ItemPrefixEpic
    {
        Gehaertetes,
    }
    public enum ItemPrefixLegendary
    {
        Markellos,
        Strahlendes,
    }
    public enum ItemPrefixUnique
    {
        Verfluchtes,
        Einzigartiges,
    }
}
