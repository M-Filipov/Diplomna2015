using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;
using Diploma2015.Abilities;
namespace Diploma2015.Entity
{
    public abstract class Characters : GameObject
    {
        public bool hasJumped;
        public float oldY;
        public int jumpPower;

        public List<Ability> abilitySet;

        public virtual void Gravitation()
        {
            position.Y += GameConsts.gravity;
 //         Console.WriteLine("gravity");
        }

        public virtual void Jump()
        {
            if (hasJumped == true)
            {
                position.Y -= jumpPower ;
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
