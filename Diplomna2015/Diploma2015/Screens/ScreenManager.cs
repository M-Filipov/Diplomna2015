using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.Screens
{
    public class ScreenManager
    {
        public Vector2 Dimensions { set; get; }
        private static ScreenManager instance;
        public ContentManager Content { private set; get; }

        Screen currentScreen;
        
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }
        private ScreenManager()
        { 
            Dimensions = new Vector2(640, 480);
            currentScreen = new GameScreen();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }
        
        public void UnloadConent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Begin();
            currentScreen.Draw(spriteBatch);
           // spriteBatch.End();
        }

    }
}
