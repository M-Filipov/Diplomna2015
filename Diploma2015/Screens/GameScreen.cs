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
        Texture2D playerSprite, background, groundTex, npc1Sprite, nodeTex, playerTex;
        Player player;
        NPC npc1;
        Animations playerAnim, npc1Anim;

        List<Platforms> platforms;
        Platforms pl = new Platforms(0,0,0,0);

        public override void Initialize()
        {
            base.Initialize();
            player = new Player(new Vector2(630, 470), GameConsts.PlayerWidth, GameConsts.PlayerHeight);
            platforms = new List<Platforms>();

            playerAnim = new Animations(playerSprite);


            Console.WriteLine(playerAnim.spriteAnimations.Count());

            nodeTex = content.Load<Texture2D>("assets/2d/nodeTex");
            pl.initPlatforms(platforms, "Haha");
            pl.CreateNodes(platforms, nodeTex);

            player.LoadPlayerAnims(playerAnim);

            npc1 = new NPC(new Vector2(250, 520), GameConsts.Npc1W, GameConsts.Npc1H, 100, 40, 200);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //player.objTexture = content.Load<Texture2D>("GameScreen/player");
            playerSprite = content.Load<Texture2D>("assets/2d/characters/redPlayer");
            background = content.Load<Texture2D>("assets/2d/terrain/Dust");
            groundTex = content.Load<Texture2D>("assets/2d/terrain/platform");
            npc1Sprite = content.Load<Texture2D>("assets/2d/characters/npc1");
            playerTex = content.Load<Texture2D>("assets/2d/characters/npc1");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            List<InputHandler.Movement> moves = InputHandler.getInput();
            Collision.coll(player, platforms);

            player.updatePlayer(moves, playerAnim);
           // playerAnim.animUpdate();

            npc1.AI(player, platforms, gameTime);
            npc1.update(gameTime);
            Collision.coll(npc1, platforms);
            
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
        
            spriteBatch.Draw(background, new Rectangle(0, 0, GameConsts.ScreenWidth, GameConsts.ScreenHeight), Color.White);
            //spriteBatch.Draw(groundTex, new Rectangle((int)ground.position.X, (int)ground.position.Y, ground.width, ground.height ), Color.White);
            //playerAnim.Draw(spriteBatch, (int)player.position.X, (int)player.position.Y);

            spriteBatch.Draw(npc1Sprite, new Rectangle((int)npc1.position.X, (int)npc1.position.Y, npc1.width, npc1.height), Color.White);

            //spriteBatch.Draw(playerTex, new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.White);
            playerAnim.Draw(spriteBatch);
            foreach (Platforms plat in platforms)
            {
                spriteBatch.Draw(groundTex, new Rectangle((int)plat.position.X, (int)plat.position.Y, plat.width, plat.height), Color.White);
            }

            foreach (Node n in Platforms.nodeList)
            {
                spriteBatch.Draw(n.nodeTex, new Rectangle((int)n.NodePos.X, (int)n.NodePos.Y, 5, 10), Color.Wheat);
            }


            spriteBatch.End();
            base.Draw(spriteBatch);
        }

    }
}
