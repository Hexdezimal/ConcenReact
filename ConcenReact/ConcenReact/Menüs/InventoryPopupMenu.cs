using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcenReact
{
    class InventoryPopupMenu : PopupMenu
    {
        private Player p;
        int yPos, xPadding;
        float xPosItemBitmap, yPosItemBitmap;

        //Inventar-Steuerung / Abfrage für Detail-Box
        private int currentSelectedItemIndex;
        private int lastSelectedItemIndex;
        private bool itemSelected;
        private int detailBoxWidth, detailBoxHeight;
        private Item currentSelectedItem;

        public InventoryPopupMenu(AssetHandler assetHandler, Player p,Brush mB, Brush iconBg, int wX, int wY, Graphics g, DebugForm df) : base(assetHandler, mB,iconBg, wX, wY,g,df)
        {
            this.p = p;
            IconBackgroundBrush = iconBg;

            //Auswahl für Details etc
            currentSelectedItemIndex = 0;
            itemSelected = false;
            
        }

        private void SetIconPosition(string s)
        {
            xPosItemBitmap = WindowSizeX / 2 + 0 + PopupSizeX * 0.15f + xPadding;
            yPosItemBitmap = yPos * (2.5f * TileSize) + (WindowSizeY / ItemBitmapSize * 2) + HeaderPositionY;
        }
        private void FillIconBackground()
        {
            Context.FillRectangle(IconBackgroundBrush, xPosItemBitmap, yPosItemBitmap , ItemBitmapSize, ItemBitmapSize);
        }
        private void DrawIconBorder(Item item)
        {
            Context.DrawRectangle(AssetHandler.Assets.RarityPens[item.Rarity], xPosItemBitmap, yPosItemBitmap , ItemBitmapSize, ItemBitmapSize);
        }
        private void DrawItemIcon(Item item)
        {
            FillIconBackground();
            Context.DrawImage(item.ItemBitmap, xPosItemBitmap, yPosItemBitmap, ItemBitmapSize, ItemBitmapSize);
            DrawIconBorder(item);
        }
        private void DrawItem(Item item)
        {
            SetIconPosition(item.Prefix+item.Name);
            if(item.GetType()==typeof(Weapon))
            {
                //TODO WAFFENPARAMTER ANZEIGEN
            }
            else if(item.GetType()==typeof(Armor))
            {
                //TODO RÜSTUNGSPARAMETER ANZEIGEN
            }

            Context.DrawString(item.Prefix + item.Name + ": ", MethodLib.GetResizedFont(item.Prefix + item.Name + ": ", TextFont,(int)((double)PopupSizeX*0.55)), Brushes.White, xPosItemBitmap - PopupSizeX * 0.55f, yPosItemBitmap);
            DrawItemIcon(item);
        }
        public override void SetBitmapAndTileSize(Bitmap bmp)
        {
            TileSize = bmp.Width;
            ItemBitmapSize = (float)(PopupSizeX * 0.06);
        }
        public override void DrawPopup()
        {
            //PopUp-Menü zeichnen
            base.DrawPopup();


            //Überprüfen ob Waffe/Rüstung vorhanden, ansonsten leeres-Objekt erzeugen
            Item tempWeap;
            if (P.Weapon != null)
                tempWeap = P.Weapon;
            else
                tempWeap = new EmptyItem();

            Item tempArmor;
            if (P.Armor != null)
                tempArmor = P.Armor;
            else
                tempArmor = new EmptyItem();

            //Festlegen der Größe
            SetBitmapAndTileSize(tempWeap.ItemBitmap);

            DrawCurrentSelectedItemHighlight();
            //Daten für Position/Größe
            yPos = 1;

            xPadding = TileSize;

            //Überschrift des Pop-Ups zeichnen
            DrawHeader("Inventar");


            //Ausgerüstete Waffe inklusive Equip-Zeichen zeichnen
            DrawItem(tempWeap);
            Context.DrawString("E", SystemFonts.DefaultFont, Brushes.Yellow, xPosItemBitmap+ItemBitmapSize*0.65f, yPosItemBitmap+ItemBitmapSize-(ItemBitmapSize/2));

            yPos++;

            DrawHorizontalSeperatorLine(new Pen(IconBackgroundBrush, ItemBitmapSize / 16), yPosItemBitmap-ItemBitmapSize*0.75f);

            //Ausgerüstete Armor inklusive Equip-Zeichen zeichnen
            DrawItem(tempArmor);
            Context.DrawString("E", SystemFonts.DefaultFont, Brushes.Yellow, xPosItemBitmap + ItemBitmapSize * 0.65f, yPosItemBitmap + ItemBitmapSize - (ItemBitmapSize / 2));

            yPos++;

            DrawHorizontalSeperatorLine(new Pen(IconBackgroundBrush, ItemBitmapSize / 16), yPosItemBitmap + ItemBitmapSize * 1.5f);

            yPos++;
            for(int i=0;i<P.InventorySpace;i++)
            {
                Item tempItem;

                if(P.Items.Count>0 && i<P.Items.Count)
                {
                    
                    if (P.Items[i] != null)
                        tempItem = P.Items[i];
                    else
                        tempItem = new EmptyItem();

                }
                else
                    tempItem = new EmptyItem();

                DrawItem(tempItem);
                yPos++;
            }


            //Zeichnen der Detail-Box
            if (itemSelected)
            {
                DrawItemDetailBox();
                DrawVerticalSeperatorLine(new Pen(IconBackgroundBrush, ItemBitmapSize / 16), GetMenuRectangleF().Right-1);
                DrawCurrentItemName();
            }
        }

        public override void KeyHandler(Keys key)
        {
            //
            //DEBUG
            //
            if (key == Keys.NumPad1)
                p.Weapon = AssetHandler.Assets.PrefabUniqueWeapons[(int)ItemUniqueWeapons.Eckesachs];
            if (key == Keys.R)
            {
                DebugForm.WriteLine("drin");
                if (currentSelectedItemIndex < p.EquipmentCount)
                {
                    if(currentSelectedItemIndex==0)
                    {
                        p.Weapon = Weapon.GetRandomWeapon(AssetHandler);

                    }

                }
                else if(currentSelectedItemIndex - p.EquipmentCount < p.Items.Count)
                {
                    //Abfrage auf Inventar-Slots
                    if (currentSelectedItemIndex - p.EquipmentCount < p.Items.Count)
                    {
                        if (p.Items[currentSelectedItemIndex - p.EquipmentCount] != null)
                        {
                            p.Items[currentSelectedItemIndex - p.EquipmentCount] = Weapon.GetRandomWeapon(AssetHandler);

                        }
                    }
                }
                itemSelected = false;
            }
            if (key==Keys.Up || key == Keys.W)
            {
                if(currentSelectedItemIndex-1>=0)
                {
                    currentSelectedItemIndex--;
                    if (DebugForm != null)
                        DebugForm.WriteLine("Up: " + currentSelectedItemIndex);

                    if (currentSelectedItemIndex != lastSelectedItemIndex)
                        itemSelected = false;
                }
            }
            if(key==Keys.Down || key == Keys.S)
            {
                if(currentSelectedItemIndex+1<P.InventorySpace+2)
                {
                    currentSelectedItemIndex++;
                    if (DebugForm != null)
                        DebugForm.WriteLine("Down: " + currentSelectedItemIndex);

                    if (currentSelectedItemIndex != lastSelectedItemIndex)
                        itemSelected = false;
                }
            }
            if(key==Keys.Left || key == Keys.A)
            {
                
            }
            if(key==Keys.Right || key == Keys.D)
            {

            }
            if(key==Keys.Enter)
            {
                if(currentSelectedItemIndex<p.EquipmentCount)
                {
                    //Abfrage auf Ausrüstungs-Slots
                    if (currentSelectedItemIndex == 0 && p.Weapon!=null )//Waffe
                    {
                        if (DebugForm != null)
                            DebugForm.WriteLine("Waffe gefunden!");

                        currentSelectedItem = p.Weapon;
                        ChangeItemSelectedState();
                        lastSelectedItemIndex = currentSelectedItemIndex;
                    }
                    else if (currentSelectedItemIndex == 1 && p.Armor!=null)//Rüstung
                    {
                        if (DebugForm != null)
                            DebugForm.WriteLine("Rüstung gefunden!");

                        currentSelectedItem = p.Armor;
                        ChangeItemSelectedState();
                        lastSelectedItemIndex = currentSelectedItemIndex;
                    }
                }
                else
                {
                    //Abfrage auf Inventar-Slots
                    if(currentSelectedItemIndex-p.EquipmentCount<p.Items.Count)
                    {
                        if(p.Items[currentSelectedItemIndex-p.EquipmentCount] != null )
                        {
                            if (DebugForm != null)
                                DebugForm.WriteLine("Item gefunden!");

                            currentSelectedItem = p.Items[currentSelectedItemIndex - p.EquipmentCount];
                            ChangeItemSelectedState();
                            lastSelectedItemIndex = currentSelectedItemIndex;
                        }
                    }


                }
            }
           
        }
        
        private void DrawCurrentItemName()
        {
            currentSelectedItem.DrawDataString(TextFont,Context, detailBoxWidth, detailBoxHeight,GetMenuRectangle().Right,GetMenuRectangle().Top);
           // Context.DrawString(currentSelectedItem.GetDataAsString()+"\n"+currentSelectedItem.Description, TextFont, Brushes.White, GetMenuRectangleF().Right, GetMenuRectangleF().Top + HeaderFont.Size / 2);
        }
        private void ChangeItemSelectedState()
        {
            if (itemSelected)
            {
                itemSelected = false;
                currentSelectedItem = null;
            }
            else itemSelected = true;
        }
        private void SetItemDetailBoxSize()
        {
            detailBoxHeight = (int)GetMenuRectangleF().Height/ 2;
            detailBoxWidth = (int)GetMenuRectangleF().Width / 2;
        }
        private void DrawItemDetailBox()
        {
            SetItemDetailBoxSize();
            Context.FillRectangle(MenuBrush, new RectangleF(GetMenuRectangleF().Right, GetMenuRectangleF().Top, detailBoxWidth, detailBoxHeight));
        }
        private void DrawCurrentSelectedItemHighlight()
        {
            //Abfrage, ob Seperator übersprungen werden muss
            int addForBorder = 1;
            if (currentSelectedItemIndex > 1)
                addForBorder = 2;


            Context.FillRectangle(HighlightBrush, new RectangleF(GetMenuRectangle().Left, (currentSelectedItemIndex+addForBorder) * (2.5f * TileSize) + (WindowSizeY / ItemBitmapSize * 2) + HeaderPositionY, PopupSizeX, ItemBitmapSize));
        }
        internal Player P { get => p;  }

    }
}
