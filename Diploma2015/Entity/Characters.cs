using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;
using Diploma2015.Abilities;
using Microsoft.Xna.Framework;
namespace Diploma2015.Entity
{
    public abstract class Characters : GameObject
    {
        public bool hasJumped;
        public bool grounded;
//      public int jumpPower;
        protected int hp;
        protected int meleeRange;
        protected int magicRange;

        protected Node currentNodeOn;

        public Vector2 velocity;

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
                position.Y += velocity.Y ;
                velocity.Y += 1;
            }
           
            if (velocity.Y >= 0)
            {
                hasJumped = false;
                velocity.Y = 0;
            }
        }

    }
}
