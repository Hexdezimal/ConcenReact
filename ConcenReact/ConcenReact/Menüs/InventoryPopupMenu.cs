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

        private int currentSelectedItem;


        public InventoryPopupMenu(Player p,Brush mB, Brush iconBg,List<Pen> rarityPens, int wX, int wY, Graphics g, DebugForm df) : base(mB,iconBg,rarityPens, wX, wY,g,df)
        {
            this.p = p;
            IconBackgroundBrush = iconBg;

            currentSelectedItem = 0;
            
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
            Context.DrawRectangle(RarityPens[item.Rarity], xPosItemBitmap, yPosItemBitmap , ItemBitmapSize, ItemBitmapSize);
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

            Context.DrawString(item.Prefix+item.Name + ": ", TextFont, Brushes.White, xPosItemBitmap - PopupSizeX * 0.55f, yPosItemBitmap);
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

            DrawSeperatorLine(new Pen(IconBackgroundBrush, ItemBitmapSize / 16), yPosItemBitmap-ItemBitmapSize*0.75f);

            //Ausgerüstete Armor inklusive Equip-Zeichen zeichnen
            DrawItem(tempArmor);
            Context.DrawString("E", SystemFonts.DefaultFont, Brushes.Yellow, xPosItemBitmap + ItemBitmapSize * 0.65f, yPosItemBitmap + ItemBitmapSize - (ItemBitmapSize / 2));


            yPos++;

            
            DrawSeperatorLine(new Pen(IconBackgroundBrush, ItemBitmapSize / 16), yPosItemBitmap + ItemBitmapSize * 1.5f);

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

        }

        public override void KeyHandler(Keys key)
        {
            if(key==Keys.Up || key == Keys.W)
            {
                if(currentSelectedItem-1>=0)
                {
                    currentSelectedItem--;
                    if (DebugForm != null)
                        DebugForm.WriteLine("Up: " + currentSelectedItem);
                }
            }
            if(key==Keys.Down || key == Keys.S)
            {
                if(currentSelectedItem+1<P.InventorySpace+2)
                {
                    currentSelectedItem++;
                    if (DebugForm != null)
                        DebugForm.WriteLine("Down: " + currentSelectedItem);
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
                if(currentSelectedItem<p.EquipmentCount)
                {
                    //Abfrage auf Ausrüstungs-Slots
                    if (currentSelectedItem == 0 && p.Weapon!=null )//Waffe
                    {
                        if (DebugForm != null)
                            DebugForm.WriteLine("Waffe gefunden!");
                    }
                    else if (currentSelectedItem == 1 && p.Armor!=null)//Rüstung
                    {
                        if (DebugForm != null)
                            DebugForm.WriteLine("Rüstung gefunden!");
                    }
                }
                else
                {
                    //Abfrage auf Inventar-Slots
                    if(currentSelectedItem-p.EquipmentCount<p.Items.Count)
                    {
                        if(p.Items[currentSelectedItem-p.EquipmentCount] != null )
                        {
                            if (DebugForm != null)
                                DebugForm.WriteLine("Item gefunden!");
                        }
                    }


                }
            }
        }
        private void DrawCurrentSelectedItemHighlight()
        {
            //Abfrage, ob Seperator übersprungen werden muss
            int addForBorder = 1;
            if (currentSelectedItem > 1)
                addForBorder = 2;


            Context.FillRectangle(HighlightBrush, new RectangleF(GetMenuRectangle().Left, (currentSelectedItem+addForBorder) * (2.5f * TileSize) + (WindowSizeY / ItemBitmapSize * 2) + HeaderPositionY, PopupSizeX, ItemBitmapSize));
        }
        internal Player P { get => p;  }

    }
}
