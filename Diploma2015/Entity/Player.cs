using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;
using Microsoft.Xna.Framework;

namespace Diploma2015.Entity
{
    public class Player : Characters
    {
        public Player(Vector2 position, int width, int height)
        {
            this.position.X = position.X;
            this.position.Y = position.Y;
            this.velocity.X = 0;
            this.velocity.Y = 10;
            this.width = width;
            this.height = height;

            hp = 100;
        }

        public void LoadPlayerAnims(Animations playerAnim)
        {
            playerAnim.AddAnimations(3, 0, 0, "walkLeft", 50, 50);
            playerAnim.AddAnimations(3, 0, 1, "walkRight", 50, 50);

            playerAnim.PlayAnim("walkLeft");
        }

        public void updatePlayer(List<InputHandler.Movement> moves, Animations playerAnim )
        {
            playerAnim.destRect.X = (int)this.position.X;
            playerAnim.destRect.Y = (int)this.position.Y;
            playerAnim.destRect.Width = this.width;
            playerAnim.destRect.Height = this.height;

            foreach( InputHandler.Movement move in moves )
            {
                if (move == InputHandler.Movement.Left)
                {
                    playerAnim.PlayAnim("walkLeft");
                    position.X -= GameConsts.PlayerSpeed;

                }
                if (move == InputHandler.Movement.Right)
                {
                    playerAnim.PlayAnim("walkRight");
                    position.X += GameConsts.PlayerSpeed;

                }
                if (move == InputHandler.Movement.Jump && !hasJumped && grounded)
                {
                    base.hasJumped = true;
                    base.grounded = false;
                    velocity.Y = -30;
                }
            }
            if(!grounded)
                playerAnim.PlayAnim("walkRight");

            base.Jump();

            base.Gravitation();    
        }
        
        public void fall()
        {
            base.Gravitation();
        }

        public void drawPlayer(SpriteBatch spriteBatch)
        {
          //  spriteBatch.Draw( objTexture, new Rectangle( (int)posX, (int)position.Y, width, height ), Color.White );
        }

    }
}
