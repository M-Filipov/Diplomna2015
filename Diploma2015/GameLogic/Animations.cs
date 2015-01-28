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
        private int currentFrame;
        public Rectangle frameToDraw;
        public Rectangle destRect;

        public Dictionary<string, List<Rectangle>> spriteAnimations = new Dictionary<string,List<Rectangle>>();
 
        public Animations(Texture2D sprite)
        {
            texture = sprite;
            currentFrame = 0;
        }

        public void AddAnimations(int frames, int xStartFrame, int yStartFrame, string name, int width, int height)
        {
            List<Rectangle> anims = new List<Rectangle>();
            for(int i = 0; i < frames; i++)
            {
                Rectangle anim = new Rectangle((xStartFrame + i) * width, yStartFrame * height, width, height);
                anims.Add(anim);
            }

            spriteAnimations.Add(name, anims);            
        }

        public void PlayAnim(string name)
        {
            if(spriteAnimations.ContainsKey(name))
            {
                if (currentFrame >= spriteAnimations[name].Count)
                    currentFrame = 0;
                else
                {
                    frameToDraw = spriteAnimations[name].ElementAt(currentFrame);
                    currentFrame++;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, frameToDraw, Color.White);
        }
    }
}
