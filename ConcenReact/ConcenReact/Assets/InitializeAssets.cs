using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConcenReact
{
    class InitializeAssets
    {
        
        //Listen
        List<Bitmap> tileIcons;
        List<Bitmap> weaponIcons;
        List<string> weaponNames;
        List<string> uniqueWeaponNames;
        List<Bitmap> uniqueWeaponIcons;
        List<Bitmap> characterBitmaps;
        //Rarity
        List<List<string>> rarityPrefix;

        //Prefabs/ Vorgefertigte Items
        List<Weapon> prefabWeapons;
        List<Weapon> prefabUniqueWeapons;

        //Brushes und Pens
        List<Pen> rarityPens;
        List<Brush> rarityBrushes;

        //AssetHandler
        AssetHandler assetHandler;

        public InitializeAssets(AssetHandler assetHandler)
        {
            this.assetHandler = assetHandler;

            WeaponIcons = new List<Bitmap>();

            TileIcons = new List<Bitmap>();
            WeaponNames = new List<string>();
            uniqueWeaponNames = new List<string>();
            rarityPrefix = new List<List<string>>();
            uniqueWeaponIcons = new List<Bitmap>();
            characterBitmaps = new List<Bitmap>();

            PrefabWeapons = new List<Weapon>();
            prefabUniqueWeapons = new List<Weapon>();

            rarityPens = new List<Pen>();
            rarityBrushes = new List<Brush>();

            Initialize();
        }
        private void Initialize()
        {
            AddWeaponIcons();
            AddUniqueWeaponIcons();
            AddTiles();
            AddWeaponNames();
            AddUniqueWeaponNames();
            AddRarityPrefixes();
            AddCharacterBitmaps();
            AddPrefabUniqueWeapons();

            AddRarityPens();
            AddRarityBrushes();
        }
        private void AddRarityBrushes()
        {
            /*
            rarityBrushes.Add(new SolidBrush(Color.FromArgb(128, Color.Gray)));
            rarityBrushes.Add(new SolidBrush(Color.FromArgb(128, Color.Green)));
            rarityBrushes.Add(new SolidBrush(Color.FromArgb(128, Color.Blue)));
            rarityBrushes.Add(new SolidBrush(Color.FromArgb(128, Color.Violet)));
            rarityBrushes.Add(new SolidBrush(Color.FromArgb(128, Color.Yellow)));
            rarityBrushes.Add(new SolidBrush(Color.FromArgb(128, Color.Orange)));
            */
            rarityBrushes.Add(Brushes.Gray);
            rarityBrushes.Add(Brushes.Green);
            rarityBrushes.Add(Brushes.LightBlue);
            rarityBrushes.Add(Brushes.Violet);
            rarityBrushes.Add(Brushes.Yellow);
            rarityBrushes.Add(Brushes.Orange);
        }
        private void AddRarityPens()
        {
            rarityPens.Add(Pens.Gray);
            rarityPens.Add(Pens.Green);
            rarityPens.Add(Pens.LightBlue);
            rarityPens.Add(Pens.Violet);
            rarityPens.Add(Pens.Yellow);
            rarityPens.Add(Pens.Orange);
        }
        private void AddCharacterBitmaps()
        {
            characterBitmaps.Add(Properties.Resources.Character_0_Myrmim);
            characterBitmaps.Add(Properties.Resources.Character_1_Lord);
            characterBitmaps.Add(Properties.Resources.Character_2_LordLyn);
            characterBitmaps.Add(Properties.Resources.Character_3_Mercenary);
            CharacterBitmaps.Add(Properties.Resources.Character_4_Cleric);
        }
        private void AddPrefabWeapons()
        {
            PrefabWeapons.Add(new Weapon(assetHandler, WeaponIcons[(int)ItemWeapons.Eisenschwert], false, true, WeaponNames[(int)ItemWeapons.Eisenschwert],"", 0.2, 0.3, 0));
        }
        private void AddPrefabUniqueWeapons()
        {
            PrefabUniqueWeapons.Add(new Weapon(assetHandler, UniqueWeaponIcons[(int)ItemUniqueWeapons.Eckesachs], false, true, UniqueWeaponNames[(int)ItemUniqueWeapons.Eckesachs], "", 8, 8, 5));
        }
        private void AddRarityPrefixes()
        {
            
            rarityPrefix.Add(new List<string>());   //0 Common
            rarityPrefix.Add(new List<string>());   //1 Rare
            rarityPrefix.Add(new List<string>());   //2 Very Rare
            rarityPrefix.Add(new List<string>());   //3 Epic
            rarityPrefix.Add(new List<string>());   //4 Legendary
            rarityPrefix.Add(new List<string>());   //5 Unique

            //Common
            rarityPrefix[0].Add("Eisen-");
            rarityPrefix[0].Add("Schwaches ");
            rarityPrefix[0].Add("Stark Beschädigtes ");
            //Rare
            rarityPrefix[1].Add("Stahl-");
            rarityPrefix[1].Add("Leicht Beschädigtes ");
            //Very Rare
            rarityPrefix[2].Add("Neues ");
            rarityPrefix[2].Add("Starkes ");
            //epic
            rarityPrefix[3].Add("Gehärtetes ");
            //Legendary
            rarityPrefix[4].Add("Makelloses ");
            rarityPrefix[4].Add("Strahlendes ");
            //Unique
            rarityPrefix[5].Add("Verfluchtes ");
            rarityPrefix[5].Add("Einzigartiges ");
        }
        private void AddWeaponNames()
        {

            WeaponNames.Add("Schwert");
            WeaponNames.Add("Breitschwert");
            WeaponNames.Add("Shamshir");
            WeaponNames.Add("Katana");
            WeaponNames.Add("Leichtschwert");
            WeaponNames.Add("Breitschwert");
        }
        private void AddUniqueWeaponNames()
        {
            UniqueWeaponNames.Add("Eckesachs");
            UniqueWeaponNames.Add("Mani Katti");
        }
        private void AddWeaponIcons()
        {
            weaponIcons.Add(Properties.Resources.Item_Sword_1_IronSword);
            WeaponIcons.Add(Properties.Resources.Item_Sword_2_LanceReaver);
            WeaponIcons.Add(Properties.Resources.Item_Sword_3_Shamshir);
            WeaponIcons.Add(Properties.Resources.Item_Sword_Katana);

            WeaponIcons.Add(Properties.Resources.Item_Sword_Slim);
            WeaponIcons.Add(Properties.Resources.Item_Sword_Steel);
        }
        private void AddUniqueWeaponIcons()
        {
            UniqueWeaponIcons.Add(Properties.Resources.Item_Sword_Eckesachs);
            UniqueWeaponIcons.Add(Properties.Resources.Item_Sword_0_ManniKatt);
            UniqueWeaponIcons.Add(Properties.Resources.Item_Sword_Rune);
        }
        private void AddTiles()
        {
            TileIcons.Add(Properties.Resources.Tile_Dummy_0);
            TileIcons.Add(Properties.Resources.Tile_Plain_0);
            TileIcons.Add(Properties.Resources.Tile_Plain_1);
            TileIcons.Add(Properties.Resources.Tile_Plain_2);
            TileIcons.Add(Properties.Resources.Tile_Plain_3);
            TileIcons.Add(Properties.Resources.Tile_Plain_House_0);
            TileIcons.Add(Properties.Resources.Tile_Plain_House_1);
            TileIcons.Add(Properties.Resources.Tile_Plain_Castle_0);


        }

        public List<Bitmap> WeaponIcons { get => weaponIcons; set => weaponIcons = value; }
        public List<Bitmap> TileIcons { get => tileIcons; set => tileIcons = value; }
        public List<string> WeaponNames { get => weaponNames; set => weaponNames = value; }
        public List<string> UniqueWeaponNames { get => uniqueWeaponNames; set => uniqueWeaponNames = value; }
        public List<List<string>> RarityPrefix { get => rarityPrefix; set => rarityPrefix = value; }
        public List<Bitmap> UniqueWeaponIcons { get => uniqueWeaponIcons; set => uniqueWeaponIcons = value; }
        public List<Bitmap> CharacterBitmaps { get => characterBitmaps; set => characterBitmaps = value; }
        internal List<Weapon> PrefabWeapons { get => prefabWeapons; set => prefabWeapons = value; }
        internal List<Weapon> PrefabUniqueWeapons { get => prefabUniqueWeapons; set => prefabUniqueWeapons = value; }
        public List<Pen> RarityPens { get => rarityPens; set => rarityPens = value; }
        public List<Brush> RarityBrushes { get => rarityBrushes; set => rarityBrushes = value; }
    }
}
