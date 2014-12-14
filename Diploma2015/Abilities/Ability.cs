using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Diploma2015.Entity;

namespace Diploma2015.Abilities
{
    public abstract class Ability : GameObject
    {

        private Texture2D spriteSheet, icon;

        int direction,
            damage,
            cooldown;

        string effect;

    }
}
