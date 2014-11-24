using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.GameLogic
{
    public class InputHandler
    {
        public enum Movement
        {
            Left,
            Right,
            Jump,
            Stand,
        }

        public static Movement getInput()
        {
            KeyboardState state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
                return Movement.Left;
            if(state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
                return Movement.Right;
            if(state.IsKeyDown(Keys.Space))
                return Movement.Jump;

            return Movement.Stand;    
        }


    }
}
