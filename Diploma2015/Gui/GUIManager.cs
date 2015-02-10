using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Diploma2015.Gui
{
    public class GUIManager
    {
        public List<GUIComponent> components = new List<GUIComponent>();

        private Vector2 mousePosition;
        public GUIManager()
        {
            components = new List<GUIComponent>();
        }

        public void Update(MouseState mouseState)
        {
            mousePosition = new Vector2(mouseState.X, mouseState.Y);

            foreach (GUIComponent gComponent in components)
            {
                if (gComponent.isVisible)
                {
                    if (gComponent.componentRectangle.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        gComponent.OnMouseClick();
                    }
                    else if (gComponent.componentRectangle.Contains(mousePosition))
                    {
                        gComponent.OnMouseOver();
                    }
                    else
                    {
                        gComponent.OnMouseFree();
                    }
                    
                }
                gComponent.Update();
            }
        }

        public void AddComponent(GUIComponent gComponent)
        {
            components.Add(gComponent);
        }
    }
}
