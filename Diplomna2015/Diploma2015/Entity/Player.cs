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
        public Player(Vector2 position, int width, int height)
        {
            this.position.X = position.X;
            this.position.Y = position.Y;
            this.width = width;
            this.height = height;
        }

        public void updatePlayer(List<InputHandler.Movement> moves )
        {
            foreach( InputHandler.Movement move in moves )
            {
                if(move == InputHandler.Movement.Left)
                    position.X -= GameConsts.PlayerSpeed;
                if(move == InputHandler.Movement.Right)
                    position.X += GameConsts.PlayerSpeed;
                if (move == InputHandler.Movement.Jump && hasJumped == false)
                {
 //                   base.oldY = position.Y;
                    base.hasJumped = true;
                    base.jumpPower = 30;
                }
            }
            base.Jump();

            base.Gravitation();    
        }
        
        public void fall()
        {
            base.Gravitation();
        }

        public void drawPlayer(SpriteBatch spriteBatch)
        {
          //  spriteBatch.Draw( objTexture, new Rectangle( (int)posX, (int)position.Y, width, height ), Color.White );
        }

    }
}
