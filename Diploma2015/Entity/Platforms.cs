using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Diploma2015.Entity
{
    public class Platforms : GameObject
    {
        public int platformH = 30;

        public static List<Node> nodeList = new List<Node>();

        public Platforms(int x, int y, int w, int h)
        {
            position.X = x;
            position.Y= y;
            width = w;
            height = h;
        }


   
    }
}
