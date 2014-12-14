using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Diploma2015.Gui
{
    public class Button : GUIComponent
    {
        public Texture2D onClickTex { get; set; }
        public Texture2D onFreeTex { get; set; }

        private int posDiff,
                    sizeDiff,
                    scaleDiff;
        public string shape { get; set; }

        private Rectangle rect, initialRect;
        public Button(int x, int y, bool isVisible, string shape)
        {
            this.posDiff = 1;
            this.sizeDiff = 2;
            this.scaleDiff = 8;
            this.isVisible = isVisible;
            this.shape = shape;

            if (this.shape.Equals("Rectangle"))
            {
                this.componentRectangle = new Rectangle(x, y, GameConsts.MediumButtonWidth, GameConsts.MediumButtonHeight);
            }
            else
            {
                this.componentRectangle = new Rectangle(x, y, GameConsts.CircleButtonRadius, GameConsts.CircleButtonRadius);
            }
            this.initialRect = this.componentRectangle;
            this.rect = changeRect(this.posDiff * this.scaleDiff * -1, this.sizeDiff * this.scaleDiff, this.componentRectangle);
        }

        public override void Update()
        {
            if (this.isMouseClicked)
            {
                this.currentTexture = this.onClickTex;
            }
            else
            {
                this.currentTexture = this.onFreeTex;
            }

        }
        public override void OnMouseOver()
        {
            base.OnMouseOver();
            if (this.shape.Equals("Rectangle") && this.isResizable)
            {
                this.componentRectangle = changeRect(this.posDiff * -1, this.sizeDiff, this.componentRectangle);
                if (this.componentRectangle.Width > rect.Width)
                {
                    this.componentRectangle = rect;
                }
            }
        }

        public override void OnMouseFree()
        {
            base.OnMouseFree();
            if (this.shape.Equals("Rectangle") && this.isResizable)
            {
                this.componentRectangle = changeRect(this.posDiff, this.sizeDiff * -1, this.componentRectangle);
                if (this.componentRectangle.Width < initialRect.Width)
                {
                    this.componentRectangle = initialRect;
                }
            }

        }

        private Rectangle changeRect(int posDiff, int sizeDiff, Rectangle componentRectangle)
        {
            return new Rectangle(componentRectangle.X + posDiff,
                                 componentRectangle.Y + posDiff,
                                 componentRectangle.Width + sizeDiff,
                                 componentRectangle.Height + sizeDiff);
        }
    }
}
