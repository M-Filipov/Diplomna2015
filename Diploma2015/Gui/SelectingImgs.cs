using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diploma2015.Gui;
using Microsoft.Xna.Framework.Input;

namespace Diploma2015.GameLogic
{
    public class SelectingImgs : GUIComponent
    {
        public int X, Y, Width, Height;
        public Animations selectingAnim;
        public Texture2D selectingSprite;
        public bool selected;
        public Rectangle imgBackgroundSelectionRect;
        Texture2D imgBackgrSelect;
        string oldLeftButtonState;
       
        public SelectingImgs(int x, int y, int width, int height, Texture2D sprite, Texture2D selectTex)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            selectingSprite = sprite;
            selectingAnim = new Animations(selectingSprite);
            selectingAnim.destRect.X = x;
            selectingAnim.destRect.Y = y;
            selectingAnim.destRect.Width = width;
            selectingAnim.destRect.Height = height;
            selected = false;

            imgBackgroundSelectionRect.X = (int)(x - x * 0.2f);
            imgBackgroundSelectionRect.Y = (int)(y - y * 0.2f);
            imgBackgroundSelectionRect.Width = (int)(width + width * 0.4f);
            imgBackgroundSelectionRect.Height= (int)(height + height * 0.4f);
            imgBackgrSelect = selectTex;
            oldLeftButtonState = "released";
        }

        public void LoadSelectingImgs()
        {
        }

        public bool ifSelected(MouseState mouseState)
        {
            Vector2 mousePos = new Vector2(mouseState.X, mouseState.Y);

            if (mousePos.X > this.X &&
                mousePos.X < this.X + this.Width &&
                mousePos.Y > this.Y &&
                mousePos.Y < this.Y + this.Height &&
                mouseState.LeftButton == ButtonState.Pressed)
            {
                selected = true;
            }
            else
                selected = false; 

            return selected;
        }

        public void UpdateSelectingImgs(string animName, MouseState mouseState)
        {
            base.Update();
            selectingAnim.PlayAnim(animName);
//            SetSelected(mouseState);
        }

        public void DrawSelectingImgs(SpriteBatch spriteBatch)
        {
            if (this.selected)
                spriteBatch.Draw(imgBackgrSelect, imgBackgroundSelectionRect, Color.White * 0.5f);
        }
        

    }
}
