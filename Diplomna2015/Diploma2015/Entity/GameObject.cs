using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Diploma2015.Entity
{
    public abstract class GameObject
    {
        public float posX { get; set; }
        public float posY { get; set; }

        public int width { get; set; }
        public int height { get; set; }

        public Texture2D objTexture;
       // public Texture2D[] objTextures { get; set; }

        public  void Draw()
        { }


    }
}
