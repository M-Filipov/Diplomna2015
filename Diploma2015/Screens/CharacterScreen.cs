using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Diploma2015.GameLogic;
using Diploma2015.Gui;

namespace Diploma2015.Screens
{
    public class CharacterScreen : Screen
    {
        Texture2D background, redPlayerTex, imgBackgroundSelectionTex;
        SelectingImgs redPlayer, bluePlayer, greenPlayer, yellowPlayer;
        public Rectangle imgBackgroundSelectionRect;
        public string currentSelectedPlayer;
        public GUIManager gManager;

        public override void Initialize()
        {
            redPlayer = new SelectingImgs(150, 100, 150, 150, redPlayerTex, imgBackgroundSelectionTex);
            bluePlayer = new SelectingImgs(350, 100, 150, 150, redPlayerTex, imgBackgroundSelectionTex);
            greenPlayer = new SelectingImgs(550, 100, 150, 150, redPlayerTex, imgBackgroundSelectionTex);
            yellowPlayer = new SelectingImgs(750, 100, 150, 150, redPlayerTex, imgBackgroundSelectionTex);

            redPlayer.selectingAnim.AddAnimations(3, 0, 0, "idle", 50, 50);
            bluePlayer.selectingAnim.AddAnimations(3, 0, 0, "idle", 50, 50);
            greenPlayer.selectingAnim.AddAnimations(3, 0, 0, "idle", 50, 50);
            yellowPlayer.selectingAnim.AddAnimations(3, 0, 0, "idle", 50, 50);

            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            gManager = new GUIManager();
            gManager.AddComponent(new Button((int)(GameConsts.ScreenWidth * 0.4), (int)(GameConsts.ScreenHeight * 0.8), true, "Rectangle"));
            foreach (Button button in gManager.components)
            {
                if (button.shape.Equals("Rectangle"))
                {
                    button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out");
                    button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_in");
                    button.isResizable = true;
                }
            }

            redPlayerTex = content.Load<Texture2D>("assets/2d/characters/redPlayer");
            background = content.Load<Texture2D>("assets/2d/gui/mysticBackground");
            imgBackgroundSelectionTex = content.Load<Texture2D>("assets/2d/gui/imgSelectingBackground");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
//            redPlayer.selectingAnim.PlayAnim("idle");
            gManager.Update(mouseState);
            setSelected();

            redPlayer.UpdateSelectingImgs("idle", mouseState);
            bluePlayer.UpdateSelectingImgs("idle", mouseState);
            greenPlayer.UpdateSelectingImgs("idle", mouseState);
            yellowPlayer.UpdateSelectingImgs("idle", mouseState);

            if (gManager.components[0].isMouseClicked)
            {
                GameConsts.chosenPlayer = currentSelectedPlayer;                
                ScreenManager.Instance.ChangeToScreen(new GameScreen());
            }
            base.Update(gameTime);
        }

        public void setSelected()
        {
            if (redPlayer.ifSelected(mouseState))
            {
                currentSelectedPlayer = "redPlayer";
                setSelectionBackgroundPos(redPlayer);
            }
            if (bluePlayer.ifSelected(mouseState))
            {
                currentSelectedPlayer = "bluePlayer";
                setSelectionBackgroundPos(bluePlayer);
            }
            if (greenPlayer.ifSelected(mouseState))
            {
                currentSelectedPlayer = "greenPlayer";
                setSelectionBackgroundPos(greenPlayer);
            }
            if (yellowPlayer.ifSelected(mouseState))
            {
                currentSelectedPlayer = "yellowPlayer";
                setSelectionBackgroundPos(yellowPlayer);
            }

        }

        public void setSelectionBackgroundPos(SelectingImgs currentPlayer)
        {
            imgBackgroundSelectionRect.X = (int)(currentPlayer.X - currentPlayer.Width * 0.2f);
            imgBackgroundSelectionRect.Y = (int)(currentPlayer.Y - currentPlayer.Width * 0.2f);
            imgBackgroundSelectionRect.Width = (int)(currentPlayer.Width + currentPlayer.Width * 0.4f);
            imgBackgroundSelectionRect.Height = (int)(currentPlayer.Height + currentPlayer.Height * 0.4f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(background, new Rectangle(0, 0, GameConsts.ScreenWidth, GameConsts.ScreenHeight), Color.White);

            redPlayer.selectingAnim.Draw(spriteBatch);
            bluePlayer.selectingAnim.Draw(spriteBatch);
            greenPlayer.selectingAnim.Draw(spriteBatch);
            yellowPlayer.selectingAnim.Draw(spriteBatch);
            spriteBatch.Draw(imgBackgroundSelectionTex, imgBackgroundSelectionRect, Color.White * 0.5f);

            foreach (Button gComponent in gManager.components)
            {
                if (gComponent.isVisible && gComponent.shape.Equals("Rectangle"))
                {
                    spriteBatch.Draw(gComponent.currentTexture, gComponent.componentRectangle, Color.White);
                }
            }
            base.Draw(spriteBatch);

        }

        


    }
}
