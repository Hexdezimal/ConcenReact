﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class GetItemPopupMenu : PopupMenu
    {
        private GetItemInteraction interaction; //Item-Interaktion für das Menü
        
        public GetItemPopupMenu(GetItemInteraction gii, Brush mB,Brush iconBg, List<Pen> rarityPens, int wX, int wY,Graphics g,DebugForm df) : base(mB,iconBg,rarityPens, wX, wY, g,df)
        {
            interaction = gii;

        }
        public string GetItemInteractionString()
        {
            string interactString;
            interactString = interaction.InteractionName + "\n\n\nItem: "+ interaction.GetItem + "\n\n\n";
            interactString += interaction.InteractionText;

            return interactString;
        }

        public override void SetBitmapAndTileSize(Bitmap bmp)
        {
            TileSize = bmp.Width;
            ItemBitmapSize = (float)(TileSize + PopupSizeX * 0.06);
        }
        public override void DrawPopup()
        {
            base.DrawPopup();   //Zeichnen der Basisklasse

            DrawHeader(interaction.InteractionName);

            SetBitmapAndTileSize(interaction.GetItem.ItemBitmap);
            DrawItemIcon(interaction.GetItem.ItemBitmap);

        }
        private void DrawReceivedItem()
        {
            
        }
        private void FillIconBackground()
        {
            Context.FillRectangle(IconBackgroundBrush, (WindowSizeX/2),PopupSizeY/2, ItemBitmapSize, ItemBitmapSize);
            
        }
        private void DrawIconBorder()
        {
            Context.DrawRectangle(Pens.Black,PopupSizeX/2, PopupSizeY/2, ItemBitmapSize, ItemBitmapSize);
        }
        private void DrawItemIcon(Bitmap itemBmp)
        {
            FillIconBackground();
            DrawIconBorder();
            Context.DrawImage(itemBmp, PopupSizeX/2, PopupSizeY/2, ItemBitmapSize, ItemBitmapSize);
            Context.DrawString(ItemBitmapSize.ToString(), SystemFonts.CaptionFont, Brushes.Black, 100, 100);
        }
    }
}
