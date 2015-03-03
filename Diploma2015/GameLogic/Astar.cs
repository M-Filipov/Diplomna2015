using Diploma2015.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.GameLogic
{
    public static class Astar
    {
        private static int nodeInterval = 50;

        public static void CreateNodes(List<Platforms> platforms, Texture2D nodeTex)
        {
            foreach (Platforms pl in platforms)
            {
                for (float plX = pl.position.X - nodeInterval; plX <= pl.position.X + pl.width + nodeInterval; plX++)
                {
                    if (plX % nodeInterval == 0)
                    {
                        Node node = new Node(new Vector2(plX, pl.position.Y), nodeTex);
                        Platforms.nodeList.Add(node);
                    }
                }
            }
            foreach (Node n in Platforms.nodeList)
            {
                foreach (Platforms plat in platforms)
                {
                    if ((n.NodePos.X == plat.position.X - nodeInterval || n.NodePos.X == plat.position.X + plat.width + nodeInterval) && n.NodePos.Y == plat.position.Y)
                        n.fallNode = true;
                }
            }
            makeConnections(nodeTex);
        }

        public static void makeConnections(Texture2D nodeTex)
        {
            foreach (Node currentNode in Platforms.nodeList)
            {
                foreach (Node connectorNode in Platforms.nodeList)
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


        public static List<Node> PathFind(Node start, Node end)
        {
            int bugFlag = 0;
            List<Node> path = new List<Node>();
            path.Add(start);

            Node currentNode = Platforms.nodeList.Find(node => node.NodePos == start.NodePos); // new Node(new Vector2(start.NodePos.X, start.NodePos.Y), nodeTex);
            Node minCostNode = new Node(new Vector2(0, 0));

            if (currentNode.connectors.ElementAt(0) != null && end != null)
            {
                while (currentNode.NodePos != end.NodePos)
                {
                    bugFlag++;
                    minCostNode.H = GameConsts.veryBigVal;
                    if (bugFlag > GameConsts.bugVal)
                        break;
                    for (int i = 0; i < currentNode.connectors.Count; i++)
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
                        currentNode = Platforms.nodeList.Find(node => node.NodePos == minCostNode.NodePos);
                    }
                }
            }
            return path;
        }


    }
}
