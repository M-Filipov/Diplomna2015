using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;
using Microsoft.Xna.Framework;

using Diploma2015.Abilities;

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

        public void LoadCharOneAnims(Animations playerAnim)
        {
            playerAnim.AddAnimation(3, 0, 40, "walkRight", 69, 76);
            playerAnim.AddAnimation(4, 318, 40, "walkLeft", 69, 76);
        }

        public void LoadCharTwoAnims(Animations playerAnim)
        {
            playerAnim.AddAnimation(8, 0, 217, "walkRight", 80, 97);
            playerAnim.AddAnimation(8, 680, 217, "walkLeft", 80, 97);
        }

        public void LoadCharThreeAnim(Animations playerAnim)
        {
            playerAnim.AddAnimation(7, 250, 120, "walkRight", 60, 100);
            playerAnim.AddAnimation(6, 740, 120, "walkLeft", 60, 100);
            
        }

        public void LoadCharFourAnims(Animations playerAnim)
        {
            playerAnim.AddAnimation(4, 0, 170, "walkRight", 70, 70);
            playerAnim.AddAnimation(4, 0, 250, "walkLeft", 70, 70);

            playerAnim.PlayAnim("walkLeft");
        }

        public void Update(List<InputHandler.Movement> moves, Animations playerAnim)
        {
            HandleMovement(moves);
            HandleAnims(moves, playerAnim);

            base.Jump();
            base.Gravitation();
            base.KillIfOutOfMap();
        }

        private void HandleMovement(List<InputHandler.Movement> moves)
        {
            foreach (InputHandler.Movement move in moves)
            {
                if (move == InputHandler.Movement.Left)
                {
                    position.X -= GameConsts.PlayerSpeed;
                    characterDir = "left";
                }
                if (move == InputHandler.Movement.Right)
                {
                    position.X += GameConsts.PlayerSpeed;
                    characterDir = "right";
                }
                if (move == InputHandler.Movement.Jump && !hasJumped && grounded)
                {
                    base.hasJumped = true;
                    base.grounded = false;
                    velocity.Y = -GameConsts.playerJumpPower;
                }
            }
        }

        private void HandleAnims(List<InputHandler.Movement> moves, Animations playerAnim)
        {
            playerAnim.destRect.X = (int)this.position.X;
            playerAnim.destRect.Y = (int)this.position.Y;
            playerAnim.destRect.Width = this.width;
            playerAnim.destRect.Height = this.height;
            foreach (InputHandler.Movement move in moves)
            {
                if (move == InputHandler.Movement.Left)
                {
                    playerAnim.PlayAnim("walkLeft");
                }
                if (move == InputHandler.Movement.Right)
                {
                    playerAnim.PlayAnim("walkRight");
                }
                if (move == InputHandler.Movement.Jump && !hasJumped && grounded)
                {
                    
                }
            }
            if (!base.grounded)
                playerAnim.PlayAnim("walkRight");
 
        }

        private void HandleSkills(List<InputHandler.Movement> moves)
        {
        }

        public void UpdateAbils(Animations anim)
        {
            if (rangedAbility.Count > 0)
            {
                for (int i = 0; i < abilityAnimations.Count; i++)
                {
                    if (abilityAnimations.ElementAt(i).currentFrame >= abilityAnimations.ElementAt(i).totalFrames ||
                        rangedAbility.ElementAt(i).position.X <= 0 ||
                        rangedAbility.ElementAt(i).position.X >= GameConsts.ScreenWidth)
                    {
                        abilityAnimations.Remove(abilityAnimations.ElementAt(i));
                        rangedAbility.Remove(rangedAbility.ElementAt(i));
                    }
                    else
                    {
                        rangedAbility.ElementAt(i).Update();
                        anim.destRect.X = (int)rangedAbility.ElementAt(i).position.X;
                        anim.destRect.Y = (int)rangedAbility.ElementAt(i).position.Y;
                        anim.destRect.Width = rangedAbility.ElementAt(i).width;
                        anim.destRect.Height = rangedAbility.ElementAt(i).height;
                        anim.PlayAnim("rightFireBall");
                    }
                }
            }
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
