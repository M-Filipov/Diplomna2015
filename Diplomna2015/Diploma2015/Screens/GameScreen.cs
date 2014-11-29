using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Diploma2015.Entity;
using Diploma2015.GameLogic;

namespace Diploma2015.Screens
{
    public class GameScreen : Screen
    {
        Player player;
        Animation playerAnim;
        Texture2D playerSprite, background, groundTex;
        List<Platforms> platforms;
        public override void Initialize()
        {
            base.Initialize();
            player = new Player(100, 100, GameConsts.PlayerWidth, GameConsts.PlayerHeight);
            playerAnim = new Animation(playerSprite, 4, 4);
            platforms = new List<Platforms>();
            //ground = new Platforms(0, 550, GameConsts.ScreenWidth, 50);
            Platforms.initPlatforms(platforms);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //player.objTexture = content.Load<Texture2D>("GameScreen/player");
            playerSprite = content.Load<Texture2D>("GameScreen/smiley");
            background = content.Load<Texture2D>("MapScreen/Dust");
            groundTex = content.Load<Texture2D>("GameScreen/platform");
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            List<InputHandler.Movement> moves = InputHandler.getInput();
            player.updatePlayer(moves);
            //if(direction != InputHandler.Movement.Stand)
                playerAnim.animUpdate();
            Collision.coll(player, platforms);

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GameConsts.ScreenWidth, GameConsts.ScreenHeight), Color.White);
            //spriteBatch.Draw(groundTex, new Rectangle((int)ground.posX, (int)ground.posY, ground.width, ground.height ), Color.White);
            playerAnim.Draw(spriteBatch, (int)player.posX, (int)player.posY);
            foreach (Platforms pl in platforms)
            {
                spriteBatch.Draw(groundTex, new Rectangle((int)pl.posX, (int)pl.posY, pl.width, pl.height), Color.White);
            }

            spriteBatch.End();
            base.Draw(spriteBatch);
        }

    }
}