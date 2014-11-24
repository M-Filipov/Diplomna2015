using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Diploma2015.Entity;
using Diploma2015.GameLogic;
namespace Diploma2015.Screens
{
    public class GameScreen : Screen
    {
        Player player;

        public override void Initialize()
        {
            base.Initialize();
            player = new Player(100, 100, GameConsts.PlayerWidth, GameConsts.PlayerHeight);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            player.objTexture = content.Load<Texture2D>("GameScreen/player");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.Movement direction = InputHandler.Movement.Stand;
            direction = InputHandler.getInput();
            player.updatePlayer(direction);

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            player.drawPlayer(spriteBatch);

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
     
        

    }
}
