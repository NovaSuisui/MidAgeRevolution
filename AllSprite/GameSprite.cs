using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllSprite
{
    class GameSprite : ICloneable
    {
        protected Texture2D _texture;
        protected bool isActive;
        protected Vector2 position;
        protected Vector2 origin;
        protected Vector2 scale;

        public GameSprite(Texture2D texture)
        {

        }

        public virtual void Update(List<GameSprite> gameObject, GameTime gameTime)
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
