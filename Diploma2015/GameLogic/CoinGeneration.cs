using Diploma2015.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.GameLogic
{
    public static class CoinGeneration
    {

        public static List<Coin> GenerateCoins(List<Platforms> platforms, Texture2D coinTex)
        {
            List<Coin> CoinsList = new List<Coin>();

            foreach (Platforms pl in platforms)
            {
                for (float plX = pl.position.X - GameConsts.CoinInterval; plX <= pl.position.X + pl.width + GameConsts.CoinInterval; plX++)
                {
                    if (plX % GameConsts.CoinInterval == 0)
                    {
                        Coin coin = new Coin(new Vector2(plX, pl.position.Y - GameConsts.CoinInterval), GameConsts.CoinW, GameConsts.CoinH, coinTex);
                        CoinsList.Add(coin);
                    }
                }
            }
            return CoinsList;
        }

    }
}