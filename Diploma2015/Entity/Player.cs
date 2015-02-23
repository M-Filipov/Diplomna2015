﻿using Microsoft.Xna.Framework.Graphics;
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

        public void LoadPlayerAnims(Animations playerAnim)
        {
            playerAnim.AddAnimations(3, 0, 0, "walkLeft", 50, 50);
            playerAnim.AddAnimations(3, 0, 1, "walkRight", 50, 50);

            playerAnim.PlayAnim("walkLeft");
        }

        public void Update(List<InputHandler.Movement> moves, Animations playerAnim, Texture2D fireBallSprite)
        {
            playerAnim.destRect.X = (int)this.position.X;
            playerAnim.destRect.Y = (int)this.position.Y;
            playerAnim.destRect.Width = this.width;
            playerAnim.destRect.Height = this.height;

            MovePlayerAndPlayAnims(moves, playerAnim, fireBallSprite);
       
            if(!grounded)
                playerAnim.PlayAnim("walkRight");
            base.Jump();
            base.Gravitation();
            base.IfOutOfMap();
        }

        public void MovePlayerAndPlayAnims(List<InputHandler.Movement> moves, Animations playerAnim, Texture2D fireBallSprite)
        {
            foreach (InputHandler.Movement move in moves)
            {
                if (move == InputHandler.Movement.Left)
                {
                    playerAnim.PlayAnim("walkLeft");
                    position.X -= GameConsts.PlayerSpeed;
                    characterDir = "left";
                }
                if (move == InputHandler.Movement.Right)
                {
                    playerAnim.PlayAnim("walkRight");
                    position.X += GameConsts.PlayerSpeed;
                    characterDir = "right";
                }
                if (move == InputHandler.Movement.Jump && !hasJumped && grounded)
                {
                    base.hasJumped = true;
                    base.grounded = false;
                    velocity.Y = -30;
                }
                if (move == InputHandler.Movement.AbilityOne && rangedAbility.Count < 1)
                {
                    Animations newFireBallAnim = new Animations(fireBallSprite);
                    FireBall ball = new FireBall(this.position.X, this.position.Y, 50, 50, this.characterDir, 10f, 7f);
                    ball.LoadAnims(newFireBallAnim);
                    abilityAnimations.Add(newFireBallAnim);
                    rangedAbility.Add(ball);
                }
            }
        
        
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

        public void drawPlayer(SpriteBatch spriteBatch)
        {
          //  spriteBatch.Draw( objTexture, new Rectangle( (int)posX, (int)position.Y, width, height ), Color.White );
        }

    }
}
