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
        protected Main _game;
        public String label;

        public Screen(Main game, Texture2D texture)
        {
            test = texture;
            _game = game;
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
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
