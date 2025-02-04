﻿using System;
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
        Texture2D playerSprite, background, groundTex, npc1Sprite, nodeTex, playerTex, coinTex;
        Player player;
        List<NPC> NpcList = new List<NPC>();
        Animations playerAnim, npc1Anim;
        List<Platforms> platforms = new List<Platforms>();
        List<Coin> CoinList = new List<Coin>();
        Platforms pl = new Platforms(0,0,0,0);

        public override void Initialize()
        {
            base.Initialize();
            
            player = new Player(new Vector2(GameVars.ScreenWidth/2, 50), GameVars.PlayerWidth, GameVars.PlayerHeight);
            InitPlatforms(platforms, GameVars.chosenMap);

            playerAnim = new Animations(playerSprite, 5);

            nodeTex = content.Load<Texture2D>("assets/2d/nodeTex");
            Astar.CreateNodes(platforms, nodeTex);
            CoinList = CoinGeneration.GenerateCoins(platforms, coinTex);

            LoadChosenPlayerAnim();

            for (int i = 0; i < 1; i++)
            {
                NPC npc = new NPC(new Vector2( (i + 1) * 200, 520), GameVars.Npc1W, GameVars.Npc1H, 100, 40, 200, 5);
                NpcList.Add(npc);
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //player.objTexture = content.Load<Texture2D>("GameScreen/player");
            playerSprite = content.Load<Texture2D>("assets/2d/characters/" + GameVars.chosenPlayer);
            background = content.Load<Texture2D>("assets/2d/terrain/" + GameVars.chosenMap);
            groundTex = content.Load<Texture2D>("assets/2d/terrain/platform");
            npc1Sprite = content.Load<Texture2D>("assets/2d/characters/npc1");
            playerTex = content.Load<Texture2D>("assets/2d/characters/npc1");
            coinTex = content.Load<Texture2D>("assets/2d/coinTex");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            List<InputHandler.Movement> moves = InputHandler.getInput();
            Collision.Collide(player, platforms);

            player.Update(moves, playerAnim);
            UpdateNpc(gameTime);

            GatherCoins();
            GameOver();
            Win();
            base.Update(gameTime);
        }

        public void UpdateNpc(GameTime gameTime)
        {
            foreach (NPC npc in NpcList)
            {
                npc.AI(player, platforms, gameTime);
                npc.Update(gameTime);
                Collision.Collide(npc, platforms);

                if(Collision.SimpleIsColliding(npc, player))
                {
                    player.Kill();
                }
            }
        }

        public void Win()
        {
            if(CoinList.Count <= 0)
            {
                ScreenManager.Instance.ChangeToScreen(new WinScreen());
            }
        }

        public void GameOver()
        {
            if (player.IsDead())
            {
                ScreenManager.Instance.ChangeToScreen(new GameOverScreen());
            }
        }

        public void GatherCoins()
        {
            for(int i = 0; i < CoinList.Count; i++)
            {
                if(Collision.SimpleIsColliding(CoinList[i], player))
                {
                    CoinList.RemoveAt(i);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, GameVars.ScreenWidth, GameVars.ScreenHeight), Color.White);
            foreach (NPC npc in NpcList)
            {
                spriteBatch.Draw(npc1Sprite, new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), Color.White);
            }

            playerAnim.Draw(spriteBatch);

            foreach (Animations a in player.abilityAnimations)
            {
                a.Draw(spriteBatch);
            }

            foreach (Coin c in CoinList)
            {
                c.Draw(spriteBatch);
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



        public void LoadChosenPlayerAnim()
        {
            switch (GameVars.chosenPlayer)
            {
                case "characterOne":
                    player.LoadCharOneAnims(playerAnim);
                    break;
                case "characterTwo":
                    player.LoadCharTwoAnims(playerAnim);
                    break;
                case "characterThree":
                    player.LoadCharThreeAnim(playerAnim);
                    break;
                case "characterFour":
                    player.LoadCharFourAnims(playerAnim);
                    break;
            }
        }

        public void InitPlatforms(List<Platforms> platforms, string mapName)
        {
            int platformH = 10;
            switch (mapName)
            {
                case "forest":
                    platforms.Add(new Platforms(50, GameVars.ScreenHeight - 50, GameVars.ScreenWidth - 100, platformH));
                    platforms.Add(new Platforms(200, 400, 250, platformH));
                    platforms.Add(new Platforms(650, 350, 250, platformH));
                    break;
                case "snow":
                    platforms.Add(new Platforms(50, GameVars.ScreenHeight - 50, GameVars.ScreenWidth - 100, platformH));
                    platforms.Add(new Platforms(200, 450, 400, platformH));
                    platforms.Add(new Platforms(300, 300, 200, platformH));
                    break;
                case "mountains":
                    platforms.Add(new Platforms(50, GameVars.ScreenHeight - 50, GameVars.ScreenWidth - 50, platformH));
                    platforms.Add(new Platforms(200, 400, 250, platformH));
                    platforms.Add(new Platforms(600, 380, 300, platformH));
                    break;
                case "city":
                    platforms.Add(new Platforms(50, GameVars.ScreenHeight - 50, GameVars.ScreenWidth - 100, platformH));
                    platforms.Add(new Platforms(200, 400, 600, platformH));
                    platforms.Add(new Platforms(300, 250, 350, platformH));
                    //platforms.Add(new Platforms(350, 100, 200, platformH));
                    break;
            }
        }
    }
}
