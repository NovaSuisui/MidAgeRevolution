using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllButton
{
    class Button : ICloneable
    {
        protected Texture2D _default, _hover, _disable, _clicked;
        protected string b_lable;
        public Vector2 position;
        public Vector2 field_size;
        protected Rectangle rect;
        protected Color color;
        protected EventHandler onClick;
        public Button(Texture2D texture)
        {
        }

        public virtual void Update(Button gameButton)
        {
        }

        public virtual void Update(List<Button> gameButton)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}