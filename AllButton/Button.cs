using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MidAgeRevolution.AllScreen;

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
        public EventHandler onClick;
        public Button(Texture2D texture)
        {
        }

        public virtual void Update(Button gameButton)
        {
        }

        public virtual void Update(List<Button> gameButton)
        {
        }
        public virtual void Update(Screen gameScreen, GameTime gameTime)
        {
        }
        public virtual void Update(GameTime gameTime)
        {
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
        public virtual void Draw(SpriteBatch spritebatch, GameTime GameTime)
        {
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}