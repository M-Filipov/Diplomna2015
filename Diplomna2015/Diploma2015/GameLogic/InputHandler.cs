using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.GameLogic
{
    public class InputHandler
    {
        public static KeyboardState oldState = Keyboard.GetState();
        public enum Movement
        {
            Left,
            Right,
            Jump,
            Stand,
        }

        public static List<Movement> getInput()
        {
            List<Movement> moves = new List<Movement>();
            KeyboardState state = Keyboard.GetState();
            if(state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
                moves.Add(Movement.Left);
            if(state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
                moves.Add(Movement.Right);
            if ((state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Up)) && (!(oldState.IsKeyDown(Keys.Space))) && !(oldState.IsKeyDown(Keys.Up)))
            {
                moves.Add(Movement.Jump);
            }
            oldState = state;
            return moves;    
        }

    }
}
