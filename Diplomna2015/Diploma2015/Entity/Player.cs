using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.Entity
{
    public class Player : Moveable
    {
        public Player(float x, float y, int w, int h, Texture2D text)
        {
            posX = x;
            posY = y;
            height = h;
            width = w;
            objTexture = text;
            //foreach( Texture2D text in objTextures )
            //{
            //    int i = 0;
            //    objTextures[i] = text;
            //    i += 1;
            //}
        }


    }
}
