using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;
using Microsoft.Xna.Framework;

namespace Diploma2015.Entity
{
    public class Player : Characters
    {
        public Player(float x, float y, int w, int h)
        {
            posX = x;
            posY = y;
            height = h;
            width = w;
        }

        public void updatePlayer(List<InputHandler.Movement> moves )
        {
            foreach( InputHandler.Movement move in moves )
            {
                if(move == InputHandler.Movement.Left)
                    posX -= GameConsts.PlayerSpeed;
                if(move == InputHandler.Movement.Right)
                    posX += GameConsts.PlayerSpeed;
                if (move == InputHandler.Movement.Jump && hasJumped == false)
                {
                    base.oldY = posY;
                    base.hasJumped = true;
                }
            }
            base.Jump();
            
            base.Gravitation();
            
            //if(dir == InputHandler.Movement.Stand
        }

        public void fall()
        {
            base.Gravitation();
        }

        public void drawPlayer(SpriteBatch spriteBatch)
        {
          //  spriteBatch.Draw( objTexture, new Rectangle( (int)posX, (int)posY, width, height ), Color.White );
        }

    }
}
