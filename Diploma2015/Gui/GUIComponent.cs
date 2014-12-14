using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Diploma2015.Gui
{
    public abstract class GUIComponent
    {
        public Rectangle componentRectangle { get; set; }
        public Texture2D currentTexture { get; set; }
        public string path { get; set; }
        public bool isVisible { get; set; }
        public bool isMouseOver { get; set; }
        public bool isMouseClicked { get; set; }
        public bool isResizable { get; set; }
        protected int time { get; set; }

        public virtual void OnMouseClick()
        {
            this.isMouseClicked = true;
            this.isMouseOver = false;
        }
        public virtual void OnMouseOver()
        {
            this.isMouseClicked = false;
            this.isMouseOver = true;
        }
        public virtual void OnMouseFree()
        {
            this.isMouseClicked = false;
            this.isMouseOver = false;
        }
        public virtual void Update()
        {

        }

    }
}
