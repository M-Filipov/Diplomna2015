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
        protected int hp;
        protected int energy;
        protected int meleeRange;
        protected int magicRange;

        protected Node currentNodeOn;

        public Vector2 velocity;
        public String characterDir;

        public List<Ability> abilitySet;
        public List<Ability> rangedAbility = new List<Ability>();
        public List<Animations> abilityAnimations = new List<Animations>();

        public virtual void Gravitation()
        {
            position.Y += GameConsts.gravity;
        }

        public virtual void RegenEnergy()
        {
            energy += 1;
            if (energy >= 100)
                energy = 100;
        }

        public virtual void KillIfOutOfMap()
        {
            if(this.position.X < 0 - 100 ||
                this.position.X > GameConsts.ScreenWidth + 100 ||
                this.position.Y > GameConsts.ScreenHeight)
            {
                Kill();
            }
        }

        public void Kill()
        {
            this.position.X = 400;
            this.position.Y = 400;

            hp = 0;
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
