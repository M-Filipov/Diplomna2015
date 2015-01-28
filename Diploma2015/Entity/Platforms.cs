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
        private int nodeInterval = 50;

        public static List<Node> nodeList = new List<Node>();

        public Platforms(int x, int y, int w, int h)
        {
            position.X = x;
            position.Y= y;
            width = w;
            height = h;
        }

        public void initPlatforms(List<Platforms> platforms, string mapName) 
        {
            switch (mapName)
            {
                case "Dust":
                    platforms.Add(new Platforms(50, GameConsts.ScreenHeight-50, GameConsts.ScreenWidth - 100, platformH));
                    platforms.Add(new Platforms(200, 400, 250, platformH));
                    platforms.Add(new Platforms(700, 400, 200, platformH));
                    platforms.Add(new Platforms(300, 200, 400, platformH));
                    break;

                case "Haha":
                    platforms.Add(new Platforms(50, GameConsts.ScreenHeight-50, GameConsts.ScreenWidth - 100, platformH));
                    platforms.Add(new Platforms(200, 400, 600, platformH));
                    platforms.Add(new Platforms(700, 200, 200, platformH));
                    platforms.Add(new Platforms(300, 200, 200, platformH));
                    break;


         //...     case "":
              
            }            
        }


        public void CreateNodes(List<Platforms> platforms, Texture2D nodeTex)
        {
            foreach (Platforms pl in platforms)
            {
                for (float plX = pl.position.X - nodeInterval; plX <= pl.position.X + pl.width + nodeInterval; plX++)
                {
                    if (plX % nodeInterval == 0)
                    {
                        Node node = new Node(new Vector2(plX, pl.position.Y), nodeTex);
                        nodeList.Add(node);
                    }
                }
            }
            foreach (Node n in nodeList)
            {
                foreach (Platforms plat in platforms)
                {
                    if ((n.NodePos.X == plat.position.X - nodeInterval || n.NodePos.X == plat.position.X + plat.width + nodeInterval) && n.NodePos.Y == plat.position.Y)
                        n.fallNode = true;
                }
            }
            makeConnections(nodeTex);
        }

        public void makeConnections(Texture2D nodeTex)
        {
            foreach (Node currentNode in nodeList)
            {
                foreach(Node connectorNode in nodeList)
                {
                    Node checkNode = new Node(new Vector2(1000, 1000));
                    if (connectorNode != currentNode || connectorNode.NodePos == currentNode.NodePos)
                    {
                        if (connectorNode.NodePos.Y == currentNode.NodePos.Y)
                        {
                            if (connectorNode.NodePos.X == currentNode.NodePos.X + nodeInterval || connectorNode.NodePos.X == currentNode.NodePos.X - nodeInterval)
                            {
                                Node newNode = new Node(new Vector2(connectorNode.NodePos.X, connectorNode.NodePos.Y), nodeTex);
                                currentNode.connectors.Add(connectorNode);
                            }
                        }
                        else
                        {
                            if (connectorNode.NodePos.X == currentNode.NodePos.X && connectorNode.NodePos.Y > currentNode.NodePos.Y - 301 && connectorNode.NodePos.Y < currentNode.NodePos.Y && !connectorNode.fallNode)
                            {
                                currentNode.connectors.Add(connectorNode);
                            }
                            if (currentNode.fallNode && connectorNode.NodePos.X == currentNode.NodePos.X && connectorNode.NodePos.Y > currentNode.NodePos.Y)
                            {
                                if (currentNode.connectors.Count <= 0)
                                {
                                    currentNode.connectors.Add(connectorNode);
                                }
                                else
                                {
                                    for (int i = 0; i < currentNode.connectors.Count; i++)
                                    {
                                        if (connectorNode.NodePos.Y < currentNode.connectors[i].NodePos.Y)
                                        {
                                            currentNode.connectors.Remove(currentNode.connectors.Find(node => node.NodePos == currentNode.connectors[i].NodePos));
                                            currentNode.connectors.Add(connectorNode);
                                        }
                                    }
                                }
                            
                            }
                        }
                    }
                }
            }
        }


        public List<Node> Astar(Node start, Node end)
        {
            int bugFlag = 0;
            List<Node> path = new List<Node>();

            List<Node> closedList = new List<Node>();

            List<Node> openList = new List<Node>();

            path.Add(start);      

            Node currentNode = nodeList.Find(node => node.NodePos == start.NodePos); // new Node(new Vector2(start.NodePos.X, start.NodePos.Y), nodeTex);
            Node minCostNode = new Node(new Vector2(0, 0));

            while (currentNode.NodePos != end.NodePos)
            {
                bugFlag++;
                    minCostNode.H = 10000;
                    if (bugFlag > 100)
                        break;
                for(int i = 0; i < currentNode.connectors.Count; i++)
                {
                    bool inClosed = false;
                    foreach (Node p in path)
                    {
                        if (p.NodePos == currentNode.connectors.ElementAt(i).NodePos)
                        {
                            inClosed = true;
                            break;
                        }
                    }
                    if (!inClosed)
                    {
                        currentNode.connectors.ElementAt(i).getH(end);
                        if (minCostNode.H >= currentNode.connectors.ElementAt(i).H)
                            minCostNode = currentNode.connectors.ElementAt(i);
                    }
                }
                if (minCostNode != currentNode)
                {
                    path.Add(minCostNode);
                    currentNode = nodeList.Find(node => node.NodePos == minCostNode.NodePos); 
                }
            }
            return path;
        }
    }
}
