using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllSprite
{
    class GameSprite : ICloneable
    {
        public bool isActive;
        protected Texture2D _texture;
        public Vector2 position;
        protected Color colour;
        protected float rotation;
        protected Vector2 origin;
        public Vector2 scale;
        protected int movespeed;
        public float hit_point;
        public Vector2 hitbox_size;

        public enum Side_ID
        {
            Wisdom_player,
            Luck_player,
            Wisdom_obstacle,
            Luck_obstacle,
        }
        public Side_ID side;

        public enum Status
        {
            normal,
            isBurn,
            isColosal,
        }
        public Status _status;

        public GameSprite(Texture2D texture)
        {
            isActive = true;
            _texture = texture;
            position = Vector2.Zero;
            colour = Color.White;
            rotation = 0;
            origin = Vector2.Zero;
            scale = new Vector2(1, 1);
            movespeed = 5;
            hit_point = 100;
            hitbox_size = new Vector2(Singleton.Instance.TEXTURE_SIZE, Singleton.Instance.TEXTURE_SIZE);
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
                return new Rectangle((int)position.X, (int)position.Y, (int)(hitbox_size.X * scale.X), (int)(hitbox_size.Y * scale.Y));
            }
        }
    }
}
