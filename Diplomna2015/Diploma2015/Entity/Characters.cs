using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;
namespace Diploma2015.Entity
{
    public abstract class Characters : GameObject
    {
        public bool hasJumped;
        public float oldY;
        public int jumpPower;
        public virtual void Gravitation()
        {
            posY += GameConsts.gravity;
 //         Console.WriteLine("gravity");
        }

        public virtual void Jump()
        {
            if (hasJumped == true)
            {
                posY -= jumpPower ;
                jumpPower -= 1;
            }
           
            if (jumpPower <= 0)
            {
                hasJumped = false;
                jumpPower = 0;
            }
        }

    }
}
