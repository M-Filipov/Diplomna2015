using Diploma2015.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Diploma2015.Screens
{
    class HelpScreen : Screen
    {
        private GUIManager gManager;

        private Texture2D exitTexture;
        public override void Initialize()
        {
            base.Initialize();            
        }
        public override void LoadContent()
        {
            base.LoadContent();

            gManager = new GUIManager();
            gManager.AddComponent(new Button(-100, (GameConsts.ScreenHeight - GameConsts.MediumButtonHeight), true, "Rectangle"));
            gManager.components[0].isResizable = false;

            foreach (Button button in gManager.components)
            {
                button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out");
                button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out");
            }

            exitTexture = content.Load<Texture2D>("assets/2d/gui/symb_leftarrow");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (gManager.components[0].isMouseClicked)
            {
                ScreenManager.Instance.ChangeToScreen(new StartScreen());
            }

            gManager.Update(mouseState);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
//            spriteBatch.Begin();

            foreach (GUIComponent gComponent in gManager.components)
            {
                if(gComponent.isVisible)
                {
                    spriteBatch.Draw(gComponent.currentTexture, gComponent.componentRectangle, Color.White);
                }
            }

            spriteBatch.Draw(exitTexture, new Rectangle(gManager.components[0].componentRectangle.X + 105 , GameConsts.ScreenHeight - 52, 80, 40), Color.White);

//            spriteBatch.End();

            base.Draw(spriteBatch);
        }

    }
}
