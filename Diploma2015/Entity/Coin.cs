using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.Entity
{
    public class Coin : GameObject
    {
        private Texture2D coinTex;

        public Coin(Vector2 pos, int w, int h, Texture2D texture)
        {
            this.position.X = pos.X;
            this.position.Y = pos.Y;
            this.width= w;
            this.height= h;
            this.coinTex = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.coinTex, new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), Color.White);
        }

    }
}
