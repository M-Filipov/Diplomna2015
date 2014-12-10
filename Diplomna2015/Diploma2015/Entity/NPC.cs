using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Diploma2015.GameLogic;

namespace Diploma2015.Entity
{
    public class NPC: Characters
    {
        public States State = States.following;
        private double timeElapsed;
        private double timeToUpdate;

        public int meleeRange;

        List<InputHandler.Movement> moves;
        public enum States
        { 
            standing,
            attacking,
            runningAway,
            following,

        }

        public NPC(int x, int y, int w, int h)
        {
            base.posX = x;
            base.posY = y;
            base.width = w;
            base.height = h;

            meleeRange = 80;
        }

        public void update(GameTime gameTime)
        {
 //           Console.WriteLine("___________________________");
            handleMovement();
            base.Jump();
            base.Gravitation();
        }

        public void handleMovement()
        {
            foreach (InputHandler.Movement move in moves)
            {
                if (move == InputHandler.Movement.Stand)
                    posX = posX;
                if (move == InputHandler.Movement.Left)
                    posX -= 3;
                if (move == InputHandler.Movement.Right)
                    posX += 3;
                if (move == InputHandler.Movement.Jump && hasJumped == false)
                {
                    base.jumpPower = 35;
                    hasJumped = true;
                }
            }
        }

        public void AI(Player player, List<Platforms> platforms)
        {
            if (InMeleeRange(player))
            {
                meleeAttackPlayer(player);
            }
            else
            {
                chasePlayer(player, platforms);
            }

            considerState(player, platforms);
        }

        public void considerState(Player player, List<Platforms> platforms)
        {
            if (State == States.following)
            {
                chasePlayer(player, platforms);
            }
            if (State == States.attacking)
            { 
            }
        }

        public void meleeAttackPlayer(Player player)
        {
            if (InMeleeRangeLeft(player))
            {
                Console.WriteLine("From left");
                // play melee atk anim left
                // deal melee dmg on player
            }
            if (InMeleeRangeRight(player))
            {
                Console.WriteLine("from right");
                // play melee atk anim left
                // deal melee dmg on player
            }


        }

        public bool InMeleeRangeRight(Player player)
        {
            if (posX + meleeRange >= player.posX && posX < player.posX)
                return true;
            
            return false;
        }

        public bool InMeleeRange(Player player)
        {
            if (posX - meleeRange < player.posX && posX + meleeRange > posX)
                return true;
            return false;
        }

        public bool InMeleeRangeLeft(Player player)
        {
            if (posX - meleeRange <= player.posX && player.posX < posX)
                return true;

            return false;
        }

        private List<InputHandler.Movement> chasePlayer(Player player, List<Platforms> platforms)
        {
            moves = new List<InputHandler.Movement>();

            if (player.posX == posX && player.posY == posY)
                moves.Add(InputHandler.Movement.Stand);
            else
            {
                if (player.posX > posX)
                    moves.Add(InputHandler.Movement.Right);
                if (player.posX < posX)
                    moves.Add(InputHandler.Movement.Left);
                if (player.posY < posY && thereIsPlatform(platforms))
                    moves.Add(InputHandler.Movement.Jump);
            }
            return moves;
        }

        private bool thereIsPlatform(List<Platforms> platforms)
        {
            for (int i = (int)posY; i > posY-100; i--)
            {
                foreach (Platforms platform in platforms)
                {
                    if (i == platform.posY &&
                        posX >= platform.posX &&
                        posX <= platform.posX + platform.width)
                            return true;
                }
            }
            return false;
        }

    }
}
