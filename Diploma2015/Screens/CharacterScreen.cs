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
        Texture2D background, characterOneTex, imgBackgroundSelectionTex, characterFourTex;
        SelectingImgs characterOne, characterTwo, characterThree, characterFour;
        public Rectangle imgBackgroundSelectionRect;
        public string currentSelectedPlayer;
        public GUIManager gManager;

        public override void Initialize()
        {
            characterOne = new SelectingImgs(characterFourTex, 150, 100, 150, 150);
            characterTwo = new SelectingImgs(characterOneTex, 350, 100, 150, 150);
            characterThree = new SelectingImgs(characterFourTex, 550, 100, 150, 150);
            characterFour = new SelectingImgs(characterFourTex, 750, 100, 150, 150);

            characterOne.selectingAnim.AddAnimations(5, 250, 80, "idle", 80, 80);
            characterTwo.selectingAnim.AddAnimationsNew(5, 250, 80, "idle", 80, 80);
            characterThree.selectingAnim.AddAnimationsNew(5, 250, 80, "idle", 80, 80);
            characterFour.selectingAnim.AddAnimationsNew(5, 250, 80, "idle", 80, 80);

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

            characterOneTex = content.Load<Texture2D>("assets/2d/characters/bronzeChar");
            characterFourTex = content.Load<Texture2D>("assets/2d/characters/bronzeChar");
            background = content.Load<Texture2D>("assets/2d/gui/mysticBackground");
            imgBackgroundSelectionTex = content.Load<Texture2D>("assets/2d/gui/imgSelectingBackground");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
//            characterOne.selectingAnim.PlayAnim("idle");
            gManager.Update(mouseState);
            setSelected();

            characterOne.UpdateSelectingImgs("idle", mouseState);
            characterTwo.UpdateSelectingImgs("idle", mouseState);
            characterThree.UpdateSelectingImgs("idle", mouseState);

            characterFour.UpdateSelectingImgs("idle", mouseState);

            if (gManager.components[0].isMouseClicked)
            {
                GameConsts.chosenPlayer = currentSelectedPlayer;                
                ScreenManager.Instance.ChangeToScreen(new GameScreen());
            }
            base.Update(gameTime);
        }

        public void setSelected()
        {
            if (characterOne.ifSelected(mouseState))
            {
                currentSelectedPlayer = "characterOne";
                setSelectionBackgroundPos(characterOne);
            }
            if (characterTwo.ifSelected(mouseState))
            {
                currentSelectedPlayer = "characterTwo";
                setSelectionBackgroundPos(characterTwo);
            }
            if (characterThree.ifSelected(mouseState))
            {
                currentSelectedPlayer = "characterThree";
                setSelectionBackgroundPos(characterThree);
            }
            if (characterFour.ifSelected(mouseState))
            {
                currentSelectedPlayer = "bronzeChar";
                setSelectionBackgroundPos(characterFour);
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

            characterOne.selectingAnim.Draw(spriteBatch);
            characterTwo.selectingAnim.Draw(spriteBatch);
            characterThree.selectingAnim.Draw(spriteBatch);
            characterFour.selectingAnim.Draw(spriteBatch);

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
