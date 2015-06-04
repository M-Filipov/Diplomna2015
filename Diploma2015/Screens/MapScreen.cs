using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using Diploma2015.GameLogic;
using Diploma2015.Gui;
using Microsoft.Xna.Framework.Media;


namespace Diploma2015.Screens
{
    public class MapScreen : Screen
    {
        Texture2D background, backgrSnow, backgrForest, backgrMountain, backgrCity, imgBackgroundSelectionTex;
        SelectingImgs snowImg, forestImg, mountainImg, cityImg;
        public Rectangle imgBackgroundSelectionRect;
        public string currentSelectedPlayer;
        public GUIManager gManager;
        private string currentSelectedMap;

        private Song song, song1;

        public override void Initialize()
        {
            snowImg = new SelectingImgs(50, 100, 200, 300, backgrSnow);
            forestImg = new SelectingImgs(300, 100, 200, 300, backgrForest);
            mountainImg = new SelectingImgs(550, 100, 200, 300, backgrMountain);
            cityImg = new SelectingImgs(800, 100, 200, 300, backgrCity);

            gManager = new GUIManager();
            gManager.AddComponent(new Button((int)(GameVars.ScreenWidth * 0.4), (int)(GameVars.ScreenHeight * 0.8), true, "Rectangle"));
            foreach (Button button in gManager.components)
            {
                if (button.shape.Equals("Rectangle"))
                {
                    button.currentTexture = button.onFreeTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_out_chooseMap");
                    button.onClickTex = content.Load<Texture2D>("assets/2d/gui/blue_rect_in_chooseMap");
                    button.isResizable = true;
                }
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            
            backgrSnow = content.Load<Texture2D>("assets/2d/terrain/snow");
            backgrForest = content.Load<Texture2D>("assets/2d/terrain/forest");
            backgrMountain = content.Load<Texture2D>("assets/2d/terrain/mountains");
            backgrCity = content.Load<Texture2D>("assets/2d/terrain/city");
            
            imgBackgroundSelectionTex = content.Load<Texture2D>("assets/2d/gui/imgSelectingBackground");
            background = content.Load<Texture2D>("assets/2d/gui/mysticBackground");

            //song = content.Load<Song>("happy");  
            //song1 = content.Load<Song>("assets/2d/audio/wind");
            //MediaPlayer.Play(song1);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SetSelected();

            gManager.Update(mouseState);
            if (gManager.components[0].isMouseClicked)
            {
                if (currentSelectedMap == null)
                    GameVars.chosenMap = "snow";
                else
                    GameVars.chosenMap = currentSelectedMap;
                
                ScreenManager.Instance.ChangeToScreen(new CharacterScreen());
            }
            //MediaPlayer.Play(song);
        }

        public void SetSelected()
        {
            if (snowImg.ifSelected(mouseState, prevMouseState))
            {
                currentSelectedMap = "snow";
                setSelectionBackgroundPos(snowImg);
                soundManager.PlaySound("click");
            }
            if (forestImg.ifSelected(mouseState, prevMouseState))
            {
                currentSelectedMap = "forest";
                setSelectionBackgroundPos(forestImg);
                soundManager.PlaySound("click");
            }
            if (mountainImg.ifSelected(mouseState, prevMouseState))
            {
                currentSelectedMap = "mountains";
                setSelectionBackgroundPos(mountainImg);
                soundManager.PlaySound("click");
            }
            if (cityImg.ifSelected(mouseState, prevMouseState))
            {
                currentSelectedMap = "city";
                setSelectionBackgroundPos(cityImg);
                soundManager.PlaySound("click");
            }

        }

        public void setSelectionBackgroundPos(SelectingImgs currentMap)
        {
            imgBackgroundSelectionRect.X = (int)(currentMap.X - currentMap.Width * 0.2f);
            imgBackgroundSelectionRect.Y = (int)(currentMap.Y - currentMap.Width * 0.2f);
            imgBackgroundSelectionRect.Width = (int)(currentMap.Width + currentMap.Width * 0.4f);
            imgBackgroundSelectionRect.Height = (int)(currentMap.Height + currentMap.Height * 0.4f);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameVars.ScreenWidth, GameVars.ScreenHeight), Color.White);

            snowImg.DrawSelectingImgs(spriteBatch);
            forestImg.DrawSelectingImgs(spriteBatch);
            mountainImg.DrawSelectingImgs(spriteBatch);
            cityImg.DrawSelectingImgs(spriteBatch);
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