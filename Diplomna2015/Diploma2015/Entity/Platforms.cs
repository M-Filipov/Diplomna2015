﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Diploma2015.GameLogic;

namespace Diploma2015.Entity
{
    public class Platforms : GameObject
    {
        public Platforms(int x, int y, int w, int h)
        {
            posX = x;
            posY = y;
            width = w;
            height = h;
        }

        //will become smarter!
        public static void initPlatforms(List<Platforms> platforms) 
        {
            Random rand = new Random();
            int X = 0;
            int Y = GameConsts.ScreenHeight - 40;
            int oldY = GameConsts.ScreenHeight - 40;
            int W = GameConsts.ScreenWidth;
            int H = 15;
            for (int i = 0; i < 7; i++)
            { 
                Platforms pl = new Platforms(X, Y, W, H);
                platforms.Add(pl);
                X = rand.Next(0, GameConsts.ScreenWidth - 200);
                Y = oldY - GameConsts.JumpHeight; //rand.Next((oldY - GameConsts.JumpHeight ), oldY+70);
                oldY = Y;
                W = rand.Next(150, 250);
            }
        }
    }
}
