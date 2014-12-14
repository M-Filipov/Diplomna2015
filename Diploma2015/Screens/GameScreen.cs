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
        NPC npc1;
        Animation playerAnim, npc1Anim;
        Texture2D playerSprite, background, groundTex, npc1Sprite;
        List<Platforms> platforms;
        public override void Initialize()
        {
            base.Initialize();
            player = new Player(new Vector2(100, 500), GameConsts.PlayerWidth, GameConsts.PlayerHeight);
            npc1 = new NPC(new Vector2(200, 400), GameConsts.Npc1W, GameConsts.Npc1H);
            playerAnim = new Animation(playerSprite, 4, 4);
            npc1Anim = new Animation(npc1Sprite, 6, 2);
            platforms = new List<Platforms>();
            //ground = new Platforms(0, 550, GameConsts.ScreenWidth, 50);
            Platforms.initPlatforms(platforms);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //player.objTexture = content.Load<Texture2D>("GameScreen/player");
            playerSprite = content.Load<Texture2D>("assets/2d/characters/smiley");
            background = content.Load<Texture2D>("assets/2d/terrain/Dust");
            groundTex = content.Load<Texture2D>("assets/2d/terrain/platform");
            npc1Sprite = content.Load<Texture2D>("assets/2d/characters/npc1");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            List<InputHandler.Movement> moves = InputHandler.getInput();
            player.updatePlayer(moves);

            playerAnim.animUpdate();

            npc1.AI(player, platforms);
            npc1.update(gameTime);

            Collision.coll(npc1, platforms);
            Collision.coll(player, platforms);
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
        
            spriteBatch.Draw(background, new Rectangle(0, 0, GameConsts.ScreenWidth, GameConsts.ScreenHeight), Color.White);
            //spriteBatch.Draw(groundTex, new Rectangle((int)ground.position.X, (int)ground.position.Y, ground.width, ground.height ), Color.White);
            playerAnim.Draw(spriteBatch, (int)player.position.X, (int)player.position.Y);

            spriteBatch.Draw(npc1Sprite, new Rectangle((int)npc1.position.X, (int)npc1.position.Y, npc1.width, npc1.height), Color.White);
            
            
            foreach (Platforms pl in platforms)
            {
                spriteBatch.Draw(groundTex, new Rectangle((int)pl.position.X, (int)pl.position.Y, pl.width, pl.height), Color.White);
            }

            spriteBatch.End();
            base.Draw(spriteBatch);
        }

    }
}
