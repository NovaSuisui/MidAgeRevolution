using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllScreen
{
    class Screen : ICloneable
    {
        protected Texture2D test;
        public String label;

        public Screen(Texture2D texture)
        {
            test = texture;
        }

        public virtual void Update(Screen gameScreen)
        {
        }

        public virtual void Update(Screen gameScreen, GameTime gameTime)
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
