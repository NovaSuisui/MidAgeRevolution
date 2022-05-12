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
        public Vector2 position;
        protected Vector2 origin;
        protected Vector2 scale;
        public Rectangle hitbox;

        public GameSprite(Texture2D texture)
        {
            _texture = texture;
            position = Vector2.Zero;
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

        public Rectangle _rectangle
        {
            get
            {
                return new Rectangle(5, 5, 5, 5);
            }
        }
    }
}
