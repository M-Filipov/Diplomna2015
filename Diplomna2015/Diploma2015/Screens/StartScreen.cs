using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Diploma2015.Screens
{
    class StartScreen : Screen
    {
        Texture2D background;
        Texture2D startButton;
        public int startBposX = 350;
        int startBposY = 250;

        public override void LoadContent()
        {
            base.LoadContent();
            background = content.Load<Texture2D>("StartScreen/mysticBackground");
            startButton = content.Load<Texture2D>("StartScreen/startButton");
            //startButtonPos = new Vector2(350, 250);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if( mouseState.LeftButton == ButtonState.Pressed && (mouseState.X >= startBposX && 
                                                                mouseState.X <= startBposX + startButton.Width &&
                                                                mouseState.Y >= startBposY &&
                                                                mouseState.Y <= startBposY + startButton.Height))
            {
                ScreenManager.Instance.changeToGameScreen();
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw( background, new Rectangle(0, 0, 640, 480), Color.White );
            spriteBatch.Draw( startButton, new Rectangle(startBposX, startBposY, 110, 60), Color.White );
            spriteBatch.End();
            base.Draw(spriteBatch);
            
        }
    }


}
