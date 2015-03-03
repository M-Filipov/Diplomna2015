using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.GameLogic
{
    public class Node
    {
        public Texture2D nodeTex;
     
        public Vector2 NodePos;
        public List<Node> connectors = new List<Node>();
        public int H;
        public bool fallNode = false;

        public Node(Vector2 pos)
        {
            NodePos = pos;
        }

        public Node(Vector2 pos, Texture2D tex)
        {
            NodePos = pos;
            nodeTex = tex;
        }

        public void getH(Node end)
        {
            this.H = 10 * (Math.Abs((int)this.NodePos.X - (int)end.NodePos.X) + Math.Abs((int)this.NodePos.Y - (int)end.NodePos.Y));
        }

    }
}
