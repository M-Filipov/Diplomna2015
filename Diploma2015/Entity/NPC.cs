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
        private int npcSpeed;
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
        private int fallRadius = 45;
        public Platforms plat = new Platforms(0, 0, 0, 0);

        private List<Node> road = new List<Node>();
        private List<InputHandler.Movement> moves = new List<InputHandler.Movement>();
        public enum States
        {
            standing,
            rangeAttacking,
            meleeAttacking,
            runningAway,
            following,
        }
        
        public NPC(Vector2 position, int width, int height, int hp, int meleeR, int magicR, int speed)
        {
            base.position.X = position.X;
            base.position.Y = position.Y;
            base.velocity.Y = 10;
            base.width = width;
            base.height = height;

            base.hp = hp;
            base.meleeRange = meleeR;
            base.magicRange = magicR;
            this.npcSpeed = speed;
            base.currentNodeOn = FindClosestNode(base.position, height);
            currentStartNode = currentNodeOn;
            currentEndNode = currentStartNode;
            checkX = position.X;
        }


        public void Update(GameTime gameTime)
        {
            handleMovement();
            base.Jump();
            base.Gravitation();
            base.KillIfOutOfMap();
        }

        public void handleMovement()
        {
            foreach (InputHandler.Movement move in moves)
            {
                if (move == InputHandler.Movement.Stand)
                    continue;
                if (move == InputHandler.Movement.Left)
                {
                    position.X -= this.npcSpeed;
                }
                if (move == InputHandler.Movement.Right)
                {
                    position.X += this.npcSpeed;    
                }
                if (move == InputHandler.Movement.Jump && hasJumped == false && grounded)
                {
                    velocity.Y = -30;
                    base.hasJumped = true;
                    base.grounded = false;
                }
            }
        }

        public void AI(Player player, List<Platforms> platforms, GameTime gameTime)
        {
            bool passiveForTooLong = CheckIfPassiveForTooLong(gameTime, 10);

            ConsiderNpcState(player);
            
            switch (currentState)
            {
                case States.meleeAttacking:
                    meleeAttackPlayer(player);
                    break;
                case States.rangeAttacking:
                    
                    break;
                case States.runningAway:
                    HandleDestination(player, passiveForTooLong, gameTime);
                    break;
                case States.following:
                    HandleDestination(player, passiveForTooLong, gameTime);
                    break;
            }

            if( (currentEndNode.NodePos != new Vector2(0, 0) && !IfEndNodeReached()) &&
                passiveForTooLong)
            {
                CleanOldPathCallAstar();
            }

            CompletePath(road);
            //CallChasePlayerEveryXSeconds(player, road, gameTime);
        }

        private void HandleDestination(Player player, bool passiveForTooLong, GameTime gameTime)    //IfEndReachedOrPassiveCleanOldPathCallAStar
        {
            if ( IfEndNodeReached() || passiveForTooLong)
            {
                currentStartNode = FindClosestNode(this.position, this.height);

                ConsiderNpcDest(player, gameTime);

                if ((currentEndNode.NodePos.X != 0 && currentEndNode.NodePos.Y != 0 && !IfEndNodeReached()) ||
                    passiveForTooLong)
                {
                    CleanOldPathCallAstar();
                }
            }
        }

        private void ConsiderNpcDest(Player player, GameTime gameTime)               // Considers the end node for the pathfinding;  if hp - low => furthest node; ELSE  If Level difficulty = easy  ||  hard
        {
            if (currentState == States.runningAway)
                currentEndNode = FindFurthestNodeRand(player);
            else                                                                              // IF NPC's HP is low =>
                if (GameConsts.Difficulty == "easy")                                          // run to the furthest part of the map
                    HandleEasyModeFollowing(gameTime, player);
                else
                {
                    currentEndNode = FindClosestNode(player.position, player.height);
                }
        }

        public void ConsiderNpcState(Player player)
        {
            if (hp < 15)
            {
                currentState = States.runningAway;
            }
            else
            {
                if (IfInMagicRange(player) /*npcHasRangeSkill && !hasCoolDown() && */)
                    currentState = States.rangeAttacking;
                else if (IfInMeleeRange(player) /*npcHasMeleeSkill && !hasCoolDown() && */)
                    currentState = States.meleeAttacking;
                else 
                      currentState = States.following;
            }
        }

        private void HandleEasyModeFollowing(GameTime gameTime, Player player)
        {
            timeElapsed = gameTime.TotalGameTime.Seconds;
            if (timeElapsed - timeToUpdate < 10)
            {
                currentEndNode = FindNodeInPlayerRange(player, 5);
            }
            else
            {
                if (timeElapsed - timeToUpdate < 20)
                {
                    currentEndNode = FindFurthestNodeRand(player);
                }
                else
                    timeToUpdate = timeElapsed;
            }
        }

        private void CallChasePlayerEveryXSeconds(Player player, List<Node> road, GameTime gameTime)
        {
            timeElapsed = gameTime.TotalGameTime.Seconds;

            if (timeElapsed - timeToUpdate < timeForChasing)
            {
                
            }
            else
            {
                if(timeElapsed - timeToUpdate > timeForChasing) 
                    timeToUpdate = timeElapsed;
                
                moves = new List<InputHandler.Movement>();
            }
        }
 
        private void CompletePath(List<Node> path)
        {
            moves = new List<InputHandler.Movement>();
            if( currentIndexInPath < path.Count)
            {
                if (path.ElementAt(currentIndexInPath).fallNode == true)
                {
                    if (path.ElementAt(currentIndexInPath).NodePos.X + fallRadius >= position.X &&
                        path.ElementAt(currentIndexInPath).NodePos.X - fallRadius <= position.X)
                    {
                        currentNodeOn = path.ElementAt(currentIndexInPath);
                        currentIndexInPath++;
                    }
                }
                else
                {
                    if (path.ElementAt(currentIndexInPath).NodePos.X == position.X &&
                        position.Y + height > path.ElementAt(currentIndexInPath).NodePos.Y - fallRadius &&
                        position.Y + height < path.ElementAt(currentIndexInPath).NodePos.Y + fallRadius)
                    {
                        currentNodeOn = path.ElementAt(currentIndexInPath);
                        currentIndexInPath++;
                    }

                    if (currentIndexInPath < path.Count)
                    {
                        AddMoves(path);
                    }
                }
            }
        }

        private void AddMoves(List<Node> path)
        {
            if (path.ElementAt(currentIndexInPath).NodePos.X > position.X)
                moves.Add(InputHandler.Movement.Right);
            if (path.ElementAt(currentIndexInPath).NodePos.X < position.X)
                moves.Add(InputHandler.Movement.Left);
            if (path.ElementAt(currentIndexInPath).fallNode == false)
            {
                if (path.ElementAt(currentIndexInPath).NodePos.Y < position.Y)
                    moves.Add(InputHandler.Movement.Jump);
            }
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

        private Node FindFarrestNode(Player player)
        {
            Node farrestNode = Platforms.nodeList.ElementAt(1);
            int playerOpposQuad = PlayersOppositeScreenQuad(player);

            foreach (Node n in Platforms.nodeList)
            {
                switch (playerOpposQuad)
                {
                    case 1:
                        if (n.NodePos.X > farrestNode.NodePos.X || n.NodePos.Y < farrestNode.NodePos.Y)
                            farrestNode = n;
                        break;
                    case 2:
                        if (n.NodePos.X < farrestNode.NodePos.X || n.NodePos.Y < farrestNode.NodePos.Y)
                            farrestNode = n;
                        break;
                    case 3:
                        if (n.NodePos.X < farrestNode.NodePos.X || n.NodePos.Y > farrestNode.NodePos.Y)
                            farrestNode = n;
                        break;
                    case 4:
                        if (n.NodePos.X > farrestNode.NodePos.X || n.NodePos.Y > farrestNode.NodePos.Y)
                            farrestNode = n;
                        break;
                    default:
                        break;
                }
            }
            return farrestNode;
        }

        //public Node RandFindFarrestNode(Player player)
        //{
        //    Random rand = new Random();
        //    int randNodeInd = rand.Next(Platforms.nodeList.Count());
        //    Node farrest = new Node(new Vector2(0, 0));
        //    int currLongestPath = 0;
        //    List<Node> currPath = new List<Node>();

           
        //}

        private Node FindFurthestNodeRand(Player player)
        {
            Random rand = new Random();
            int index = rand.Next(Platforms.nodeList.Count);
            while (Platforms.nodeList.ElementAt(index).NodePos.Y == FindClosestNode(player.position, player.height).NodePos.Y &&
                        Platforms.nodeList.ElementAt(index).fallNode == false)
            {
                index = rand.Next(1, Platforms.nodeList.Count - 1);
            }
            return Platforms.nodeList.ElementAt(index);
        }

        public Node FindNodeInPlayerRange(Player player, int range)
        {
            Random rand = new Random();
            var playersNodeInd = Platforms.nodeList.FindIndex(node => node.NodePos == FindClosestNode(player.position, player.height).NodePos);
            int randNodeInd = rand.Next(playersNodeInd - range, playersNodeInd + range);
            while (randNodeInd < 0 || randNodeInd >= Platforms.nodeList.Count)
                randNodeInd = rand.Next(playersNodeInd - range, playersNodeInd + range);

            return Platforms.nodeList.ElementAt(randNodeInd);
        }

        private bool CheckIfPassiveForTooLong(GameTime gameTime, int seconds)
        {
            bool passiveForTooLong = false;
            tElapsed = gameTime.TotalGameTime.Seconds;

            if (checkX != position.X)
            {
                tToUpdate = tElapsed;
                checkX = position.X;
            }

            if (Math.Abs(tElapsed - tToUpdate) > seconds)
            {
                passiveForTooLong = true;
                checkX = 0;
            }

            return passiveForTooLong;
        }

        public int PlayersOppositeScreenQuad(Player player)    // screen - coord system - gives plyer's opposite one 1-3; 2-4
        {
            if (player.position.X > GameConsts.ScreenWidth / 2)
            {
                if (player.position.Y > GameConsts.ScreenHeight / 2)
                    return 2;
                else
                    return 3;
            }
            else
            {
                if (player.position.Y > GameConsts.ScreenHeight / 2)
                    return 1;
                else
                    return 4;
            }
        }

        private void meleeAttackPlayer(Player player)
        {
            //Console.WriteLine("MELEEE ATTACKING ");
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

        private bool InMeleeRangeRight(Player player)
        {
            if (position.X + meleeRange >= player.position.X && position.X < player.position.X)
                return true;
            return false;
        }

        private bool IfInMagicRange(Player player)
        {
            if (position.X - magicRange < player.position.X && position.X + magicRange > player.position.X &&
                FindClosestNode(player.position, player.height).NodePos.Y == FindClosestNode(this.position, this.height).NodePos.Y)
                return true;
            return false;
        }

        private bool IfInMeleeRange(Player player)
        {
            if (position.X - meleeRange < player.position.X && position.X + meleeRange > player.position.X)
                return true;
            return false;
        }

        private bool InMeleeRangeLeft(Player player)
        {
            if (position.X - meleeRange <= player.position.X && player.position.X < position.X)
                return true;

            return false;
        }

        private bool IfEndNodeReached()
        {
            return currentNodeOn.NodePos == currentEndNode.NodePos;
        }

        private void CleanOldPathCallAstar()
        {
            currentIndexInPath = 0;
            road = new List<Node>();
            road = Astar.PathFind(currentStartNode, currentEndNode);
        }

    }
}