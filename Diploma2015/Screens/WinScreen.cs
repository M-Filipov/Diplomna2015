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
    public class WinScreen : Screen
    {
        Texture2D background, exitTexture;
        private GUIManager gManager;

        public override void LoadContent()
        {
            base.LoadContent();

            gManager = new GUIManager();
            gManager.AddComponent(new Button((int)(GameConsts.ScreenWidth * 0.37), ((int)(GameConsts.ScreenHeight * 0.7)), true, "Rectangle"));

            foreach (Button button in gManager.components)
            {
                button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out");
                button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out");
            }
            background = content.Load<Texture2D>("assets/2d/gui/winScreen");
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

            gManager.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameConsts.ScreenWidth, GameConsts.ScreenHeight), Color.White);

            foreach (GUIComponent gComponent in gManager.components)
            {
                if (gComponent.isVisible)
                {
                    spriteBatch.Draw(gComponent.currentTexture, gComponent.componentRectangle, Color.White);
                }
            }

            base.Draw(spriteBatch);
        }
    }
}
