using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;

namespace Diploma2015.Abilities
{
    public class FireBall : Ranged
    {
        int explosionRad,
            outerDamage;

        public FireBall()
        { }

        public FireBall(float x, float y, int width, int height, string dir, float dmg, float speed)
        {
            this.position.X = x;
            this.position.Y = y;
            this.width = width;
            this.height = height;
            this.damage = dmg;
            this.speed = speed;

            if (dir == "left")
                this.direction = Direction.LeftDir;
            if (dir == "right")
                this.direction = Direction.RightDir;
        }

        public override void LoadAnims(Animations anim)
        {
            anim.AddAnimations(12, 0, 0, "rightFireBall", 60, 60);
        }



    }
}
