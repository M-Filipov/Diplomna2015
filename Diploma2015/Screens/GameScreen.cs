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
        Texture2D playerSprite, background, groundTex, npc1Sprite, nodeTex, playerTex;
        List<Platforms> platforms;
        Platforms pl = new Platforms(0,0,0,0);

        public override void Initialize()
        {
            base.Initialize();
            player = new Player(new Vector2(630, 520), GameConsts.PlayerWidth, GameConsts.PlayerHeight);  
            //playerAnim = new Animation(playerSprite, 4, 4);
            //npc1Anim = new Animation(npc1Sprite, 6, 2);
            platforms = new List<Platforms>();
            //ground = new Platforms(0, 550, GameConsts.ScreenWidth, 50);
            pl.initPlatforms(platforms, "Dust");

            nodeTex = content.Load<Texture2D>("assets/2d/nodeTex");
            pl.CreateNodes(platforms, nodeTex);

            Console.WriteLine(Platforms.nodeList.ElementAt(10).NodePos);

            npc1 = new NPC(new Vector2(250, 520), GameConsts.Npc1W, GameConsts.Npc1H, 100, 40, 200);
            Console.WriteLine("npc1Pos = " + npc1.position);


            foreach (Node n in Platforms.nodeList)
            {
                    Console.Write(" x " + n.NodePos.X);
                    Console.Write(" y " + n.NodePos.Y + "||");
                    Console.WriteLine(n.connectors.ElementAt(0).NodePos);
                    //if(n.connectors.ElementAt(1) != )
                    //    Console.WriteLine(n.connectors.ElementAt(1).NodePos);
                    Console.WriteLine(" ");
            }
            List<Node> path = new List<Node>();
            //path = pl.Astar(Platforms.nodeList.Find(node => node.NodePos == new Vector2(550, 150)), Platforms.nodeList.Find(node => node.NodePos == new Vector2(250, 350)));

            //foreach (Node n in path)
            //{
            //    Console.WriteLine(n.NodePos);

            //    if (n.fallNode)
            //        Console.WriteLine(" HAHAHAHHAHAHAH " + n.NodePos);
            //}
            //Console.WriteLine(Platforms.nodeList.Find(node => node.NodePos == new Vector2(500, 350)).connectors[0].NodePos);
            //Console.WriteLine(Platforms.nodeList.Find(node => node.NodePos == new Vector2(500, 350)).connectors[1].NodePos);

            //Console.WriteLine(Platforms.nodeList.Find(node => node.NodePos == new Vector2(450, 150)).connectors[0].NodePos);
            //Console.WriteLine(Platforms.nodeList.Find(node => node.NodePos == new Vector2(450, 150)).connectors[1].NodePos);
            //Console.WriteLine(Platforms.nodeList.Find(node => node.NodePos == new Vector2(450, 150)).connectors[2].NodePos);
            
            //Console.WriteLine(pl.nodeList.ElementAt(21).connectors.ElementAt(2).NodePos);

            }

        public override void LoadContent()
        {
            base.LoadContent();
            //player.objTexture = content.Load<Texture2D>("GameScreen/player");
            playerSprite = content.Load<Texture2D>("assets/2d/characters/smiley");
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

            player.updatePlayer(moves);
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

            spriteBatch.Draw(playerTex, new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.White);

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
