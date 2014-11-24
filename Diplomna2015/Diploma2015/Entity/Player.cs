using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;
using Microsoft.Xna.Framework;

namespace Diploma2015.Entity
{
    public class Player : GameObject
    {
        public Player(float x, float y, int w, int h)
        {
            posX = x;
            posY = y;
            height = h;
            width = w;
           // objTexture = text;
            //foreach( Texture2D text in objTextures )
            //{
            //    int i = 0;
            //    objTextures[i] = text;
            //    i += 1;
            //}
        }

        public void updatePlayer(InputHandler.Movement dir )
        {
            if (dir == InputHandler.Movement.Left)
                posX -= GameConsts.PlayerSpeed;
            if (dir == InputHandler.Movement.Right)
                posX += GameConsts.PlayerSpeed;
            //if(dir == InputHandler.Movement.Stand
        }

        public void drawPlayer(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw( objTexture, new Rectangle( (int)posX, (int)posY, width, height ), Color.White );
        }

    }
}
