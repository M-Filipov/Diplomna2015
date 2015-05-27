using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diploma2015.Entity;

using Diploma2015.GameLogic;

namespace Diploma2015.Abilities
{
    public abstract class Ability : GameObject
    {
        private Texture2D spriteSheet, icon;

        protected enum Direction
        {
            LeftDir,
            RightDir,
        }

        protected Direction direction;

        protected float speed,
                        damage,
                        cooldown;

        string effect;

        public abstract void Update();

        public abstract void LoadAnims(Animations anim);

    }
}
