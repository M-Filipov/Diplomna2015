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
        }

        public void update(GameTime gameTime)
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
            base.Jump();
            base.Gravitation();
        }

        public void AI(Player player, List<Platforms> platforms)
        {
            //TODO: set state blabla
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
                attackPlayer(player);
            }

        }
        public void attackPlayer(Player player)
        {
 
        }
        private List<InputHandler.Movement> chasePlayer(Player player, List<Platforms> platforms)
        {
            moves = new List<InputHandler.Movement>();

            if (player.posX == posX)
                moves.Add(InputHandler.Movement.Stand);
            if (player.posX > posX)
                moves.Add(InputHandler.Movement.Right);
            if (player.posX < posX)
                moves.Add(InputHandler.Movement.Left);
            if (player.posY < posY && thereIsPlatform(platforms))
                moves.Add(InputHandler.Movement.Jump);
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
