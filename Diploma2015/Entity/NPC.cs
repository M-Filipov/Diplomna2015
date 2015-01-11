using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Diploma2015.GameLogic;

namespace Diploma2015.Entity
{
    public class NPC : Characters
    {
        public States currentState = States.following;
        private double timeElapsed;
        private double timeToUpdate;
        private double tElapsed;
        private double tToUpdate;

        private double timeForChasing = 2.0;
        private double timeForIdle = 1.0;

        private Node currentStartNode;
        private Node currentEndNode;
        private int currentIndexInPath;
        private float checkX;
        public Platforms plat = new Platforms(0, 0, 0, 0);

        List<Node> road = new List<Node>();

        List<InputHandler.Movement> moves = new List<InputHandler.Movement>();
        public enum States
        {
            standing,
            rangeAttacking,
            meleeAttacking,
            runningAway,
            following,

        }
        
        public NPC(Vector2 position, int width, int height, int hp, int meleeR, int magicR)
        {
            base.position.X = position.X;
            base.position.Y = position.Y;
            base.velocity.Y = 10;
            base.width = width;
            base.height = height;

            base.hp = hp;
            base.meleeRange = meleeR;
            base.magicRange = magicR;
            base.currentNodeOn = FindClosestNode(base.position, height);
            currentStartNode = currentNodeOn;
            currentEndNode = currentStartNode;
            checkX = position.X;
        }

        public NPC(Node nodeOn, int w, int h, int hp, int meleeR, int magicR)
        {
            base.position.X = nodeOn.NodePos.X;
            base.position.Y = nodeOn.NodePos.Y;
            base.velocity.Y = 10;
            base.width = width;
            base.height = height;

            base.hp = hp;
            base.meleeRange = meleeR;
            base.magicRange = magicR;
            base.currentNodeOn = nodeOn;
            currentStartNode = currentNodeOn;
            currentEndNode = currentStartNode;

        }

        public void update(GameTime gameTime)
        {
            handleMovement();
            base.Jump();
            base.Gravitation();
        }

        public void handleMovement()
        {
            foreach (InputHandler.Movement move in moves)
            {
                Console.WriteLine("move= " + move);
                if (move == InputHandler.Movement.Stand)
                {
//                    moves = new List<InputHandler.Movement>();
                    continue;
                }
                if (move == InputHandler.Movement.Left)
                    position.X -= 5;
                if (move == InputHandler.Movement.Right)
                    position.X += 5;
                if (move == InputHandler.Movement.Jump && hasJumped == false && grounded)
                {
                    velocity.Y = -30;
                    base.hasJumped = true;
                    base.grounded = false;
                }
            }
        }

        private bool CheckIfPassiveForTooLong(GameTime gameTime)
        {
            bool passiveForTooLong = false;
            tElapsed = gameTime.TotalGameTime.Seconds;

            Console.WriteLine(" CHECK = " + checkX + "X + " + position.X );
            if (checkX != position.X)
            {
                tToUpdate = tElapsed;
                checkX = position.X;
 //               Console.WriteLine(tElapsed + " tToUp " + tToUpdate + " ");
            }
            Console.WriteLine(tElapsed + " tToUp " + tToUpdate + " ");

            if (Math.Abs(tElapsed - tToUpdate) > 10)
            {
                passiveForTooLong = true;
                Console.WriteLine("   DAISKLN ADS LKDAS NLKD SNKDL SKNADL SKNLSKAD NLK DSANLADS KNL DASKND LSANLASD DALNSKLNA SDKDALNS ");
                checkX = 0;
            }

            return passiveForTooLong;
        }

        public void AI(Player player, List<Platforms> platforms, GameTime gameTime)
        {

            bool check = CheckIfPassiveForTooLong(gameTime);
        
            ConsiderState();
//            Console.WriteLine(FindClosestNode(new Vector2(500, 540)).NodePos);
//            Console.WriteLine(" = " + currentStartNode.NodePos);
//            Console.WriteLine(" - " + currentEndNode.NodePos);
            if (currentNodeOn.NodePos == currentEndNode.NodePos || check)
            {
                Console.WriteLine(" VLEEEEEEEEEEZEEEEEEEEEEEEEEEEEE");
                currentStartNode = currentNodeOn;
        //       currentEndNode.NodePos = new Vector2(0, 0);
                
                currentEndNode = FindClosestNode(player.position, player.height);
                
  //              Console.WriteLine("ENDA = " + currentEndNode.NodePos);
                if( (currentEndNode.NodePos.X != 0 && currentEndNode.NodePos.Y != 0 && currentEndNode.NodePos != currentNodeOn.NodePos) || 
                    check)
                {
                    Console.WriteLine(" OTNOVOOOOOOOOOOOOOOOOOOOOOOOO");
                    currentIndexInPath = 0;
                    road = new List<Node>();
 
                    road = plat.Astar(currentNodeOn, currentEndNode);
                    foreach (Node n in road)
                    {
                        Console.WriteLine("___ " + n.NodePos);
                    }
                }
    //            else
  //                  Console.WriteLine(" === IZKARA NULATA ");
            }
 
            switch (currentState)
            {
                case States.meleeAttacking:
                    if (InMeleeRange(player))
                    {
                        meleeAttackPlayer(player);
                    }
                    break;
                case States.rangeAttacking:

                    break;
            }

            CallChase(player, road, gameTime);

        }

        public void ConsiderState()
        {
            if (hp <= 15)
            {
                currentState = States.runningAway;
            }
            else
            {
                // if( npcHasRangeSkill && !hasCoolDown() ) 
                //      currentState = RangeAttacking
                // else if( npcHasMeleeSkill && !hasCoolDown() )
                //          currentState = meleeAttacking
            }
        }

        private void CallChase(Player player, List<Node> road, GameTime gameTime)
        {
            timeElapsed = gameTime.TotalGameTime.Seconds;

            if (timeElapsed - timeToUpdate < timeForChasing)
            {
                chasePlayer(player, road);
            }
            else
            {
                if(timeElapsed - timeToUpdate > timeForChasing) 
                    timeToUpdate = timeElapsed;
                
                moves = new List<InputHandler.Movement>();
            }
//            Console.WriteLine(timeElapsed);
        }

        public void meleeAttackPlayer(Player player)
        {
            if (InMeleeRangeLeft(player))
            {
                //Console.WriteLine("From left");
                // play melee atk anim left
                // deal melee dmg on player
            }
            if (InMeleeRangeRight(player))
            {
                //Console.WriteLine("from right");
                // play melee atk anim left
                // deal melee dmg on player
            }


        }

        public bool InMeleeRangeRight(Player player)
        {
            if (position.X + meleeRange >= player.position.X && position.X < player.position.X)
                return true;

            return false;
        }

        public bool InMeleeRange(Player player)
        {
            if (position.X - meleeRange < player.position.X && position.X + meleeRange > position.X)
                return true;
            return false;
        }

        public bool InMeleeRangeLeft(Player player)
        {
            if (position.X - meleeRange <= player.position.X && player.position.X < position.X)
                return true;

            return false;
        }

        private List<InputHandler.Movement> chasePlayer(Player player, List<Node> path)
        {
            moves = new List<InputHandler.Movement>();
//            Console.WriteLine(currentNodeOn.NodePos);
//            Console.WriteLine(currentIndexInPath);
//            Console.WriteLine(currentIndexInPath);
  //          Console.WriteLine("pos = " + position);
            if( currentIndexInPath < path.Count)
            {
                //Console.WriteLine(path.ElementAt(currentIndexInPath).NodePos);
                //Console.WriteLine(position.X);
                if (path.ElementAt(currentIndexInPath).fallNode == true)
                {
                    if (path.ElementAt(currentIndexInPath).NodePos.X + 45 >= position.X &&
                        path.ElementAt(currentIndexInPath).NodePos.X - 45 <= position.X )
   //                 position.Y + height >= path.ElementAt(currentIndexInPath).NodePos.Y - 40 &&
    //                position.Y + height <= path.ElementAt(currentIndexInPath).NodePos.Y + 40)
                    {
                        currentNodeOn = path.ElementAt(currentIndexInPath);
                        currentIndexInPath++;
                    }
                }
                else
                {
                    if (path.ElementAt(currentIndexInPath).NodePos.X == position.X &&
                        position.Y + height > path.ElementAt(currentIndexInPath).NodePos.Y - 45 &&
                        position.Y + height < path.ElementAt(currentIndexInPath).NodePos.Y + 45)
                    {
                        currentNodeOn = path.ElementAt(currentIndexInPath);
                        currentIndexInPath++;
                    }

                    if (currentIndexInPath < path.Count)
                    {
                        //Console.WriteLine(path.ElementAt(currentIndexInPath).NodePos.X);
                        //Console.WriteLine(position.X);
                        //Console.WriteLine(currentIndexInPath);
                        if (path.ElementAt(currentIndexInPath).NodePos.X > position.X)
                            moves.Add(InputHandler.Movement.Right);
                        if (path.ElementAt(currentIndexInPath).NodePos.X < position.X)
                            moves.Add(InputHandler.Movement.Left);
                        if (path.ElementAt(currentIndexInPath).fallNode == true)
                        {
                            Console.WriteLine("It's a Fall Node ");
                        }
                        else
                        {
                            if (path.ElementAt(currentIndexInPath).NodePos.Y < position.Y)
                                moves.Add(InputHandler.Movement.Jump);
                        }
                    }
                }
   //             Console.WriteLine("curNpos = " + currentNodeOn.NodePos);
  //              Console.WriteLine("aimNodePos" + path.ElementAt(currentIndexInPath).NodePos);

            }
            return moves;
        }

        private Node FindClosestNode(Vector2 plPos, int height)
        {
            int Yradius = 30;
            Node closest = new Node(new Vector2(0, 0));
            foreach (Node node in Platforms.nodeList)
            {
                if (plPos.Y + height >= node.NodePos.Y - Yradius && plPos.Y + height <= node.NodePos.Y + Yradius)
                {
                    if (Math.Abs(plPos.X - node.NodePos.X) < Math.Abs(plPos.X - closest.NodePos.X)) 
                    {
                        closest = node;
                        if(plPos.X == node.NodePos.X)
                            break;
                    }
                }
            }
            if( closest.NodePos != new Vector2(0,0) )
                return closest;
            else
                return currentEndNode;
        }



        //private bool thereIsPlatform(List<Platforms> platforms)
        //{
        //    for (int i = (int)position.Y; i > position.Y - 200; i--)
        //    {
        //        foreach (Platforms platform in platforms)
        //        {
        //            if (i == platform.position.Y &&
        //                position.X >= platform.position.X - 200 &&
        //                position.X <= platform.position.X + platform.width)
        //                return true;
        //        }
        //    }
        //    return false;
        //}

    }
}