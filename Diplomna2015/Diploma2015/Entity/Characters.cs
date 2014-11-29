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
        public virtual void Gravitation()
        {
            posY += 10; 
        }

        public virtual void Jump()
        {

            if (hasJumped == true)
            {
                posY -= GameConsts.gravity + GameConsts.JumpSpeed ;
            }

            if (oldY - posY  == GameConsts.JumpHeight)
                hasJumped = false;
        }

    }
}
