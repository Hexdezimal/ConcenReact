using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class InventoryPopupMenu : PopupMenu
    {
        private Player p;
        int yPos, xPadding, tileSize;
        float xPosItemBitmap, yPosItemBitmap;
        float itemBitmapSize;
        Brush iconBackgroundBrush;

        public InventoryPopupMenu(Player p, Brush iconBg, Brush mB, int wX, int wY, Graphics g, DebugForm df) : base(mB, wX, wY,g,df)
        {
            this.p = p;
            iconBackgroundBrush = iconBg;
        }
        public InventoryPopupMenu(Player p, Brush mB, int wX, int wY, Graphics g, DebugForm df) : base(mB, wX, wY, g,df)
        {
            this.p = p;
            iconBackgroundBrush = new SolidBrush(Color.FromArgb(100, Color.Black));
        }
        private void SetIconPosition(string s)
        {
            xPosItemBitmap = WindowSizeX / 2 + 0 + PopupSizeX * 0.15f + xPadding;
            yPosItemBitmap = yPos * (2.5f * tileSize) + (WindowSizeY / itemBitmapSize * 2) + HeaderPositionY;
        }
        private void FillIconBackground()
        {
            Context.FillRectangle(iconBackgroundBrush, xPosItemBitmap, yPosItemBitmap , itemBitmapSize, itemBitmapSize);
        }
        private void DrawIconBorder()
        {
            Context.DrawRectangle(Pens.Black, xPosItemBitmap, yPosItemBitmap , itemBitmapSize, itemBitmapSize);
        }
        private void DrawItemIcon(Bitmap itemBmp)
        {
            FillIconBackground();
            DrawIconBorder();
            Context.DrawImage(itemBmp, xPosItemBitmap, yPosItemBitmap, itemBitmapSize, itemBitmapSize);
        }
        private void DrawItem(Item item)
        {
            SetIconPosition(item.Name);
            if(item.GetType()==typeof(Weapon))
            {
                //TODO WAFFENPARAMTER ANZEIGEN
            }
            else if(item.GetType()==typeof(Armor))
            {
                //TODO RÜSTUNGSPARAMETER ANZEIGEN
            }

            Context.DrawString(item.Name + ": ", TextFont, Brushes.White, xPosItemBitmap - PopupSizeX * 0.55f, yPosItemBitmap);
            DrawItemIcon(item.ItemBitmap);
        }
        public override void DrawPopup()
        {
            //PopUp-Menü zeichnen
            base.DrawPopup();

            itemBitmapSize = (float)(tileSize + PopupSizeX * 0.06);

            //Überprüfen ob Waffe/Rüstung vorhanden, ansonsten leeres-Objekt erzeugen
            Item tempWeap;
            if (p.Weapon != null)
                tempWeap = p.Weapon;
            else
                tempWeap = new EmptyItem();

            Item tempArmor;
            if (p.Armor != null)
                tempArmor = p.Armor;
            else
                tempArmor = new EmptyItem();

            //Daten für Position/Größe
            yPos = 1;
            tileSize = tempWeap.ItemBitmap.Width;
            xPadding = tileSize;

            //Iconposition Ausgerüstete Waffe
            DrawHeader("Inventar");

            DrawItem(tempWeap);


            yPos++;

            DrawSeperatorLine(new Pen(iconBackgroundBrush, itemBitmapSize / 16), yPosItemBitmap-itemBitmapSize*0.75f);

            //Iconposition ausgerüstete Rüstung
            DrawItem(tempArmor);
            //Context.DrawImage(tempArmor.ItemBitmap, xPosItemBitmap, yPosItemBitmap, itemBitmapSize, itemBitmapSize);

            yPos++;

            //Context.DrawLine(new Pen(Color.DarkGray,itemBitmapSize/16),GetMenuRectangle().X, yPosItemBitmap+itemBitmapSize*1.5f, GetMenuRectangleF().Right, yPosItemBitmap+itemBitmapSize*1.5f);
            DrawSeperatorLine(new Pen(iconBackgroundBrush, itemBitmapSize / 16), yPosItemBitmap + itemBitmapSize * 1.5f);

            yPos++;
            for(int i=0;i<p.InventorySpace;i++)
            {
                Item tempItem;

                if(p.Items.Count>0 && i<p.Items.Count)
                {
                    
                    if (p.Items[i] != null)
                        tempItem = p.Items[i];
                    else
                        tempItem = new EmptyItem();

                }
                else
                    tempItem = new EmptyItem();

                DrawItem(tempItem);
                yPos++;
            }

        }
    }
}
