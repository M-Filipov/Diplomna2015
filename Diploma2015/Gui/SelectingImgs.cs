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
        //Texture2D imgBackgrSelect;
        //string oldLeftButtonState;
       
        public SelectingImgs(Texture2D sprite, int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            selectingSprite = sprite;
            selectingAnim = new Animations(selectingSprite, 7);
            selectingAnim.destRect.X = x;
            selectingAnim.destRect.Y = y;
            selectingAnim.destRect.Width = width;
            selectingAnim.destRect.Height = height;
            selected = false;
        }
         
        public SelectingImgs(int x, int y, int width, int height, Texture2D sprite)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.selectingSprite = sprite;
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
            //SetSelected(mouseState);
        }

        public void DrawSelectingImgs(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.selectingSprite, new Rectangle(X, Y, Width, Height), Color.White);
        }
        

    }
}
