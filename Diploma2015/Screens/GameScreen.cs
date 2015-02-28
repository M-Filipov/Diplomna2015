using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Diploma2015.Entity;
using Diploma2015.GameLogic;
using Diploma2015.Abilities;
namespace Diploma2015.Screens
{
    public class GameScreen : Screen
    {
        Texture2D playerSprite, background, groundTex, npc1Sprite, nodeTex, playerTex, fireBallTex, fireBallSprite;
        Player player;
        List<NPC> NpcList = new List<NPC>();
        Animations playerAnim, npc1Anim, fireBallAnim;
        List<Platforms> platforms = new List<Platforms>();
        Platforms pl = new Platforms(0,0,0,0);

        public override void Initialize()
        {
            base.Initialize();
            player = new Player(new Vector2(630, 470), GameConsts.PlayerWidth, GameConsts.PlayerHeight);

            InitPlatforms(platforms, GameConsts.chosenMap);

            playerAnim = new Animations(playerSprite, 5);

            nodeTex = content.Load<Texture2D>("assets/2d/nodeTex");

            Astar.CreateNodes(platforms, nodeTex);
            player.LoadPlayerAnims(playerAnim);

            for (int i = 0; i < 2; i++)
            {
                NPC npc = new NPC(new Vector2( (i + 1) * 200, 520), GameConsts.Npc1W, GameConsts.Npc1H, 100, 40, 200, 5);
                NpcList.Add(npc);
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //player.objTexture = content.Load<Texture2D>("GameScreen/player");
            playerSprite = content.Load<Texture2D>("assets/2d/characters/" + GameConsts.chosenPlayer);
            background = content.Load<Texture2D>("assets/2d/terrain/" + GameConsts.chosenMap);
            groundTex = content.Load<Texture2D>("assets/2d/terrain/platform");
            npc1Sprite = content.Load<Texture2D>("assets/2d/characters/npc1");
            playerTex = content.Load<Texture2D>("assets/2d/characters/npc1");
            fireBallSprite = content.Load<Texture2D>("assets/2d/abilities/fireBallAnim");
            fireBallTex = content.Load<Texture2D>("assets/2d/abilities/fireBall");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            List<InputHandler.Movement> moves = InputHandler.getInput();
            Collision.coll(player, platforms);

            player.Update(moves, playerAnim);

            for (int i = 0; i < player.abilityAnimations.Count; i++ )
            {            
                player.UpdateAbils(player.abilityAnimations.ElementAt(i));
            }

            foreach (NPC npc in NpcList)
            {
                npc.AI(player, platforms, gameTime);
                npc.Update(gameTime);
                Collision.coll(npc, platforms);
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameConsts.ScreenWidth, GameConsts.ScreenHeight), Color.White);
            foreach (NPC npc in NpcList)
            {
                spriteBatch.Draw(npc1Sprite, new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), Color.White);
            }
            playerAnim.Draw(spriteBatch);

            foreach (Animations a in player.abilityAnimations)
            {
                a.Draw(spriteBatch);
            }
            
            foreach (Platforms plat in platforms)
            {
                spriteBatch.Draw(groundTex, new Rectangle((int)plat.position.X, (int)plat.position.Y, plat.width, plat.height), Color.White);
            }

            foreach (Node n in Platforms.nodeList)
            {
                spriteBatch.Draw(n.nodeTex, new Rectangle((int)n.NodePos.X, (int)n.NodePos.Y, 5, 10), Color.Wheat);
            }
            base.Draw(spriteBatch);
        }



        public void InitPlatforms(List<Platforms> platforms, string mapName)
        {
            int platformH = 15;
            switch (mapName)
            {
                case "forest":
                    platforms.Add(new Platforms(50, GameConsts.ScreenHeight - GameConsts.ScreenHeight + 200, 300, platformH));
                    platforms.Add(new Platforms(200, 400, 250, platformH));
                    platforms.Add(new Platforms(650, 400, 200, platformH));
                    platforms.Add(new Platforms(300, 550, 400, platformH));
                    break;
                case "snow":
                    platforms.Add(new Platforms(50, GameConsts.ScreenHeight - 50, GameConsts.ScreenWidth - 100, platformH));
                    platforms.Add(new Platforms(200, 400, 600, platformH));
                    platforms.Add(new Platforms(700, 200, 200, platformH));
                    platforms.Add(new Platforms(300, 200, 200, platformH));
                    break;
                case "mountains":
                    platforms.Add(new Platforms(50, GameConsts.ScreenHeight - 50, 600, platformH));
                    platforms.Add(new Platforms(150, 400, 350, platformH));
                    platforms.Add(new Platforms(400, 280, 250, platformH));
                    platforms.Add(new Platforms(600, 150, 100, platformH));
                    break;
                case "city":
                    platforms.Add(new Platforms(50, GameConsts.ScreenHeight - 50, GameConsts.ScreenWidth - 100, platformH));
                    platforms.Add(new Platforms(200, 400, 600, platformH));
                    platforms.Add(new Platforms(700, 200, 200, platformH));
                    platforms.Add(new Platforms(300, 200, 200, platformH));
                    break;


                //...     case "":

            }
        }


    }



}
