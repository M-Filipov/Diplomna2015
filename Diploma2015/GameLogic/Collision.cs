﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.Entity;
namespace Diploma2015.GameLogic
{
    public static class Collision
    {
        static bool colLeft = false, 
                    colRight = false, 
                    colUp = false, 
                    colDown = false;

        public static void Collide(Characters obj, List<Platforms> platforms)
        {
            foreach (Platforms obj2 in platforms)
            {
                IsColliding(obj, obj2);

                if (colDown)
                {
                    obj.position.Y = obj2.position.Y - obj.height;
                    obj.grounded = true;
                }
                resetCols();
            }
        }

        private static void resetCols()
        {
            colDown = false;
            colLeft = false;
            colRight = false;
        }
            
        private static void IsColliding(GameObject obj1, GameObject obj2)
        {
            if (obj1.position.X + obj1.width >= obj2.position.X &&
                obj1.position.Y >= obj2.position.Y &&
                obj1.position.Y <= obj2.position.Y + obj2.height
               )
                colRight = true;
            if (obj1.position.X >= obj2.position.X + obj2.width &&
                obj1.position.Y >= obj2.position.Y &&
                obj1.position.Y >= obj2.position.Y + obj2.height
               )
                colLeft = true;
            if (obj1.position.X + obj1.width >= obj2.position.X &&
                obj1.position.X <= obj2.position.X + obj2.width &&
                (obj1.position.Y + obj1.height) >= obj2.position.Y &&
                obj1.position.Y + obj1.height <= obj2.position.Y + obj2.height
               )
                colDown = true;
        }
        
    }
    
}

