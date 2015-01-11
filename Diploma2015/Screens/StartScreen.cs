using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Diploma2015.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using Diploma2015.GameLogic;
namespace Diploma2015.Screens
{
    class StartScreen : Screen
    {
        public Texture2D background, 
                         soundTex;
        public GUIManager gManager;

        private bool isMute = false;

        private int buttonTimer;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            gManager = new GUIManager();
            gManager.AddComponent(new Button((int)(GameConsts.ScreenWidth * 0.4), (int)(GameConsts.ScreenHeight * 0.40), true, "Rectangle"));
            gManager.AddComponent(new Button((int)(GameConsts.ScreenWidth * 0.4), (int)(GameConsts.ScreenHeight * 0.55), true, "Rectangle"));
            gManager.AddComponent(new Button((int)(GameConsts.ScreenWidth * 0.4), (int)(GameConsts.ScreenHeight * 0.70), true, "Rectangle"));
            gManager.AddComponent(new Button(0, GameConsts.ScreenHeight - GameConsts.CircleButtonRadius, true, "Circle"));

            foreach (Button button in gManager.components)
            {
                if (button.shape.Equals("Rectangle"))
                {
                    button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out");
                    button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_in");
                    button.isResizable = true;
                }
                else
                {
                    button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_circle_out");
                    button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_circle_in");
                }
            }
            background = content.Load<Texture2D>("assets/2d/gui/mysticBackground");

            soundTex = content.Load<Texture2D>("assets/2d/gui/symb_volume");

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
                ScreenManager.Instance.ChangeToScreen(new GameScreen());
            }
            else if (gManager.components[1].isMouseClicked)
            {
                ScreenManager.Instance.ChangeToScreen(new HelpScreen());
            }
            else if (gManager.components[2].isMouseClicked)
            {
                Game1.game.Exit(); //lol
            }
            buttonTimer++;
            if (buttonTimer > 120) buttonTimer = 120;

            if (gManager.components[3].isMouseClicked)
            {
                muteSound(120);
            }
            else
            {
                buttonTimer = 120;
            }

            gManager.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, GameConsts.ScreenWidth, GameConsts.ScreenHeight), Color.White);

            foreach (Button gComponent in gManager.components)
            {
                if (gComponent.isVisible && gComponent.shape.Equals("Rectangle"))
                {
                    spriteBatch.Draw(gComponent.currentTexture, gComponent.componentRectangle, Color.White);
                }
                else
                {
                    spriteBatch.Draw(gComponent.currentTexture, new Vector2(gComponent.componentRectangle.X, gComponent.componentRectangle.Y), Color.White);
                }

                spriteBatch.Draw(soundTex, new Rectangle(0 + 20, GameConsts.ScreenHeight - GameConsts.CircleButtonRadius + 20, 50, 50), Color.White);
            }
            spriteBatch.End();
            base.Draw(spriteBatch);

        }
        private void muteSound(int buttonTimer)
        {
            if (!isMute && this.buttonTimer == buttonTimer)
            {
                isMute = true;
                soundTex = content.Load<Texture2D>("assets/2d/gui/symb_mute");
                this.buttonTimer = 0;
            }
            else if (isMute && this.buttonTimer == buttonTimer)
            {
                isMute = false;
                soundTex = content.Load<Texture2D>("assets/2d/gui/symb_volume");
                this.buttonTimer = 0;
            }
        }
    }
}
