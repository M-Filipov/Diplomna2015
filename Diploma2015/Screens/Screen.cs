using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diploma2015.Gui;
using Microsoft.Xna.Framework.Input;

namespace Diploma2015.Screens
{
    public abstract class Screen
    {
        protected ContentManager content;

        private Texture2D cursorTex;

        public MouseState mouseState;

        protected Point cursorPosition;
        public virtual void Initialize()
        {
            cursorPosition = new Point(mouseState.X, mouseState.Y);
        }
        public virtual void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
            cursorTex = content.Load<Texture2D>("assets/2d/gui/crs_arrow");
        }
        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            cursorPosition = mouseState.Position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            spriteBatch.Draw(cursorTex, new Rectangle(cursorPosition.X, cursorPosition.Y, 50, 50), Color.White);
            //spriteBatch.End();
        }


    }
}
