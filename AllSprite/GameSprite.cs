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
        public Vector2 scale;
        protected int movespeed;
        public int hitbox_size;

        public enum Side_ID
        {
            Wisdom_player,
            Luck_player,
            Wisdom_obstacle,
            Luck_obstacle,

        }
        public Side_ID side;

        public GameSprite(Texture2D texture)
        {
            _texture = texture;
            position = Vector2.Zero;
            origin = Vector2.Zero;
            scale = new Vector2(1, 1);
            movespeed = 5;
            hitbox_size = (int)Singleton.Instance.TEXTURE_SIZE;
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

        public Rectangle hitbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, hitbox_size * (int)scale.X, hitbox_size * (int)scale.Y);
            }
        }
    }
}
