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
        List<Bitmap> tileIcons;
        
        //Für Zufallsgenerator
        List<Bitmap> weaponIcons;
        List<string> weaponNames;
        List<string> uniqueWeaponNames;
        List<Bitmap> uniqueWeaponIcons;
        //Rarity
        List<List<string>> rarityPrefix;

        //Prefabs/ Vorgefertigte Items
        List<Weapon> prefabWeapons;


        public InitializeAssets()
        {
            WeaponIcons = new List<Bitmap>();

            TileIcons = new List<Bitmap>();
            WeaponNames = new List<string>();
            uniqueWeaponNames = new List<string>();
            rarityPrefix = new List<List<string>>();
            uniqueWeaponIcons = new List<Bitmap>();
            prefabWeapons = new List<Weapon>();

            Initialize();
        }
        private void Initialize()
        {
            AddWeaponIcons();
            AddTiles();
            AddWeaponNames();
            AddUniqueWeaponNames();
            AddRarityPrefixes();
        }
        private void AddPrefabWeapons()
        {
            prefabWeapons.Add(new Weapon(null, WeaponIcons[(int)ItemWeapons.Eisenschwert], false, true, WeaponNames[(int)ItemWeapons.Eisenschwert],"", 0.02, 0.03, 0));
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
            rarityPrefix[4].Add("Markelloses ");
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
            UniqueWeaponNames.Add("Mani Katti");
            UniqueWeaponNames.Add("Exodus");
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
            UniqueWeaponIcons.Add(Properties.Resources.Item_Sword_0_ManniKatt);
            UniqueWeaponIcons.Add(Properties.Resources.Item_Sword_Exodus);
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
    }
}
