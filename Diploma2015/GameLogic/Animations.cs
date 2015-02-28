using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.GameLogic
{
    public class Animations
    {
        private Texture2D texture { get; set; }
        public int currentFrame;
        private int animPerS;
        private int count;
        public Rectangle frameToDraw;
        public Rectangle destRect;
        public int totalFrames;

        public Dictionary<string, List<Rectangle>> spriteAnimations = new Dictionary<string,List<Rectangle>>();
 
        public Animations(Texture2D sprite, int perSec)
        {
            texture = sprite;
            currentFrame = 0;
            animPerS = perSec;
            count = 0;
        }

        public void AddAnimation(int frames, int xStartPos, int yStartPos, string name, int width, int height)
        {
            totalFrames = frames;
            List<Rectangle> anims = new List<Rectangle>();
            for (int i = 0; i < totalFrames; i++)
            {
                Rectangle anim = new Rectangle(i * width + xStartPos, yStartPos, width, height);
                anims.Add(anim);
            }

            spriteAnimations.Add(name, anims);
        }


        //public void AddAnimationNew(int frames, int xStartFrame, int yStartFrame, string name, int width, int height)
        //{
        //    totalFrames = frames;
        //    List<Rectangle> anims = new List<Rectangle>();
        //    for(int i = 0; i < totalFrames; i++)
        //    {
        //        Rectangle anim = new Rectangle((xStartFrame + i) * width, yStartFrame * height, width, height);
        //        anims.Add(anim);
        //    }
        //    spriteAnimations.Add(name, anims);            
        //}

        public void PlayAnim(string name)
        {
            if(spriteAnimations.ContainsKey(name))
            {
                if (currentFrame >= spriteAnimations[name].Count)
                    currentFrame = 0;
                if ( count <= this.animPerS)
                    count++;
                else
                {
                    frameToDraw = spriteAnimations[name].ElementAt(currentFrame);
                    currentFrame++;
                    count = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, frameToDraw, Color.White);
        }
    }
}
