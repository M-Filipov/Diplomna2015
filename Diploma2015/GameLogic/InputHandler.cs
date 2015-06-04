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

            if(state.IsKeyDown(Keys.A))
                moves.Add(Movement.Left);
            if(state.IsKeyDown(Keys.D))
                moves.Add(Movement.Right);
            if ((state.IsKeyDown(Keys.W)) && !(oldState.IsKeyDown(Keys.W)))
                moves.Add(Movement.Jump);

            oldState = state;
            return moves;    
        }

    }
}
