using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diploma2015.GameLogic;
using Diploma2015.Gui;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Diploma2015.Screens
{
    public class CharacterScreen : Screen
    {
        Texture2D background, characterOneTex, characterTwoTex, characterThreeTex, imgBackgroundSelectionTex, characterFourTex;
        SelectingImgs characterOne, characterTwo, characterThree, characterFour;
        public Rectangle imgBackgroundSelectionRect;
        public string currentSelectedPlayer;
        public GUIManager gManager;

        public Song sound;
        

        public override void Initialize()
        {
            characterOne = new SelectingImgs(characterOneTex, 350, 100, 150, 150);
            characterTwo = new SelectingImgs(characterTwoTex, 150, 100, 150, 150);
            characterThree = new SelectingImgs(characterThreeTex, 550, 100, 150, 150);
            characterFour = new SelectingImgs(characterFourTex, 750, 100, 150, 150);

            characterOne.selectingAnim.AddAnimation(3, 10, 130, "idle", 90, 95);
            characterTwo.selectingAnim.AddAnimation(3, 5, 8, "idle", 55, 105);
            characterThree.selectingAnim.AddAnimation(7, 250, 120, "idle", 60, 100);
            characterFour.selectingAnim.AddAnimation(4, 0, 170, "idle", 70, 70);       //5, 250, 80, "idle", 80, 80  shoot anim

            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            gManager = new GUIManager();
            gManager.AddComponent(new Button((int)(GameVars.ScreenWidth * 0.4), (int)(GameVars.ScreenHeight * 0.8), true, "Rectangle"));
            foreach (Button button in gManager.components)
            {
                if (button.shape.Equals("Rectangle"))
                {
                    button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out_choosePlayer");
                    button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_in_choosePlayer");
                    button.isResizable = true;
                }
            }

            characterOneTex = content.Load<Texture2D>("assets/2d/characters/characterOne");
            characterTwoTex = content.Load<Texture2D>("assets/2d/characters/characterTwo");
            characterThreeTex = content.Load<Texture2D>("assets/2d/characters/characterThree");
            characterFourTex = content.Load<Texture2D>("assets/2d/characters/characterFour");
            background = content.Load<Texture2D>("assets/2d/gui/mysticBackground");
            imgBackgroundSelectionTex = content.Load<Texture2D>("assets/2d/gui/imgSelectingBackground");

            sound = content.Load<Song>("assets/audio/Click22-Sebastian-759472264");
           
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            gManager.Update(mouseState);
            setSelected();
         
            characterOne.UpdateSelectingImgs("idle", mouseState);
            characterTwo.UpdateSelectingImgs("idle", mouseState);
            characterThree.UpdateSelectingImgs("idle", mouseState);
            characterFour.UpdateSelectingImgs("idle", mouseState);

            if (gManager.components[0].isMouseClicked)
            {
                if (currentSelectedPlayer != null)
                    GameVars.chosenPlayer = currentSelectedPlayer;
                else
                    GameVars.chosenPlayer = "characterTwo";

                ScreenManager.Instance.ChangeToScreen(new GameScreen());
            }
            base.Update(gameTime);
        }

        public void setSelected()
        {
            if (characterOne.ifSelected(mouseState, prevMouseState))
            {
                currentSelectedPlayer = "characterOne";
                setSelectionBackgroundPos(characterOne);
                soundManager.PlaySound("click");
            }
            if (characterTwo.ifSelected(mouseState, prevMouseState))
            {
                currentSelectedPlayer = "characterTwo";
                setSelectionBackgroundPos(characterTwo);
                soundManager.PlaySound("click");
            }
            if (characterThree.ifSelected(mouseState, prevMouseState))
            {
                currentSelectedPlayer = "characterThree";
                setSelectionBackgroundPos(characterThree);
                soundManager.PlaySound("click");
            }
            if (characterFour.ifSelected(mouseState, prevMouseState))
            {
                currentSelectedPlayer = "characterFour";
                setSelectionBackgroundPos(characterFour);
                soundManager.PlaySound("click");
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

            spriteBatch.Draw(background, new Rectangle(0, 0, GameVars.ScreenWidth, GameVars.ScreenHeight), Color.White);

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
