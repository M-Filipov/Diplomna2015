using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;

namespace Diploma2015.Abilities
{
    public abstract class Ranged : Ability
    {
        protected float speed,
                      range;

        public override void Update()
        {
            if (direction == Direction.LeftDir)
                position.X -= speed;

            if (direction == Direction.RightDir)
                position.X += speed;
        }

        public override void LoadAnims(Animations anim)
        {
            Console.WriteLine(anim.ToString()); 
        }
    }
}
