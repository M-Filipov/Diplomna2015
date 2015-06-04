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
        public Texture2D background, soundTex, easyModeTex, hardModeTex;
        public GUIManager gManager;

        private bool isMute = false;
        private int buttonTimer;

        private SelectingImgs gameMode;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            gManager = new GUIManager();
            gManager.AddComponent(new Button((int)(GameVars.ScreenWidth * 0.4), (int)(GameVars.ScreenHeight * 0.40), true, "Rectangle"));
            gManager.AddComponent(new Button((int)(GameVars.ScreenWidth * 0.4), (int)(GameVars.ScreenHeight * 0.55), true, "Rectangle"));
           // gManager.AddComponent(new Button((int)(GameConsts.ScreenWidth * 0.4), (int)(GameConsts.ScreenHeight * 0.70), true, "Rectangle"));
            gManager.AddComponent(new Button(0, GameVars.ScreenHeight - GameVars.CircleButtonRadius, true, "Circle"));

            foreach (Button button in gManager.components)
            {
                if (button.shape.Equals("Rectangle") && gManager.components.IndexOf(button) == 0)
                {
                    button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out_start");
                    button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_in_start");
                    button.isResizable = true;
                }
                else if (button.shape.Equals("Rectangle") && gManager.components.IndexOf(button) == 1)
                {
                    button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out_help");
                    button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_in_help");
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
            easyModeTex = content.Load<Texture2D>("assets/2d/gui/easyMode");
            hardModeTex = content.Load<Texture2D>("assets/2d/gui/hardMode");

            gameMode = new SelectingImgs((int)(GameVars.ScreenWidth * 0.40), (int)(GameVars.ScreenHeight * 0.85), 250, 60, easyModeTex);

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
                ScreenManager.Instance.ChangeToScreen(new MapScreen());
            }
            else if (gManager.components[1].isMouseClicked)
            {
                ScreenManager.Instance.ChangeToScreen(new HelpScreen());
            }
            //else if (gManager.components[2].isMouseClicked) {
               // Game1.game.Exit();            }

            buttonTimer++;
            if (buttonTimer > 120) buttonTimer = 120;

            if (gManager.components[2].isMouseClicked)
            {
                muteSound(120);
            }
            else
            {
                buttonTimer = 120;
            }

            if(gameMode.ifSelected(mouseState, prevMouseState))
            {
                if(GameVars.Difficulty == "easy")
                {
                    GameVars.Difficulty = "hard";
                    gameMode.selectingSprite = hardModeTex;
                }
                else
                {
                    GameVars.Difficulty = "easy";
                    gameMode.selectingSprite = easyModeTex;
                }
            }

            gManager.Update(mouseState);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameVars.ScreenWidth, GameVars.ScreenHeight), Color.White);

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

                spriteBatch.Draw(soundTex, new Rectangle(0 + 20, GameVars.ScreenHeight - GameVars.CircleButtonRadius + 20, 50, 50), Color.White);
            }

            gameMode.DrawSelectingImgs(spriteBatch);

            base.Draw(spriteBatch);
        }

        private void muteSound(int buttonTimer)
        {
            if (!isMute && this.buttonTimer == buttonTimer)
            {
                isMute = true;
                soundTex = content.Load<Texture2D>("assets/2d/gui/symb_mute");
                this.buttonTimer = 0;
                GameVars.IsSoundOn = false;
            }
            else if (isMute && this.buttonTimer == buttonTimer)
            {
                isMute = false;
                soundTex = content.Load<Texture2D>("assets/2d/gui/symb_volume");
                this.buttonTimer = 0;
                GameVars.IsSoundOn = true;
            }
        }
    }
}
