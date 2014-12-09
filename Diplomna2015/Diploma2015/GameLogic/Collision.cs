using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.Entity;
namespace Diploma2015.GameLogic
{
    public static class Collision
    {
        static bool colLeft = false, colRight = false, colUp = false, colDown = false;
        public static void coll(GameObject obj, List<Platforms> platforms)
        {
            foreach (Platforms plat in platforms)
            {
                isColliding(obj, plat);
             //   if (colLeft)
              //      obj.posX -= GameConsts.PlayerSpeed;
             //   if (colRight)
             //       obj.posX += GameConsts.PlayerSpeed;
                if (colDown)
                {
                    obj.posY -= GameConsts.gravity;
                }
                Console.WriteLine(colDown);
                colDown = false;
//            resetCols();
            }
        }

        private static void resetCols()
        {
            colDown = false;
            colLeft = false;
            colRight = false;
        }

        private static void isColliding(GameObject obj, Platforms plat)
        {
            if (obj.posX + obj.width >= plat.posX &&
                obj.posY >= plat.posY &&
                obj.posY <= plat.posY + plat.height
              )
                colRight = true;
            if (obj.posX >= plat.posX + plat.width &&
                obj.posY >= plat.posY &&
                obj.posY >= plat.posY + plat.height
                )
                colLeft = true;
            if (obj.posX + obj.width >= plat.posX &&
                obj.posX <= plat.posX + plat.width &&
                (obj.posY + obj.height) >= plat.posY &&
                obj.posY + obj.height <= plat.posY + plat.height
                )
                colDown = true;
        }
        
    }
    
}

