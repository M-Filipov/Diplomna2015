using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Diploma2015.Entity
{
    public abstract class GameObject
    {
        public Vector2 position;

        public int width { get; set; }
        public int height { get; set; }

    }
}
