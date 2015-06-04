using Diploma2015.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.Screens
{
    public class GameOverScreen : Screen
    {
        Texture2D background, exitTexture;
        private GUIManager gManager;

        public override void LoadContent()
        {
            base.LoadContent();

            gManager = new GUIManager();
            gManager.AddComponent(new Button(-100, (GameVars.ScreenHeight - GameVars.MediumButtonHeight), true, "Rectangle"));
            gManager.AddComponent(new Button((int)(GameVars.ScreenWidth * 0.37), ((int)(GameVars.ScreenHeight * 0.7)), true, "Rectangle"));

            gManager.components[0].isResizable = false;

            foreach (Button button in gManager.components)
            {
                button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out_playAgain");
                button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out_playAgain");
            }

            exitTexture = content.Load<Texture2D>("assets/2d/gui/symb_leftarrow");
            background = content.Load<Texture2D>("assets/2d/gui/loseScreen");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (gManager.components[0].isMouseClicked)
            {
                ScreenManager.Instance.ChangeToScreen(new StartScreen());
            }
            else if (gManager.components[1].isMouseClicked)
            {
                //Game1.game.Exit();
                ScreenManager.Instance.ChangeToScreen(new StartScreen());

                
               
            }
            //else if (gManager.components[2].isMouseClicked)
            //{
            //    Game1.game.Exit(); 
            //}


            gManager.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameVars.ScreenWidth, GameVars.ScreenHeight), Color.White);

            foreach (GUIComponent gComponent in gManager.components)
            {
                if (gComponent.isVisible)
                {
                    spriteBatch.Draw(gComponent.currentTexture, gComponent.componentRectangle, Color.White);
                }
            }

            spriteBatch.Draw(exitTexture, new Rectangle(gManager.components[0].componentRectangle.X + 105, GameVars.ScreenHeight - 52, 80, 40), Color.White);

            base.Draw(spriteBatch);
        }
    }
}
