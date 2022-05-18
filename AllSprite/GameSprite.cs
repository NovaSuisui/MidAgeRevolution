using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using tainicom.Aether.Physics2D.Dynamics;

namespace MidAgeRevolution.AllSprite
{
    class GameSprite : ICloneable
    {
        public bool isActive;
        protected Texture2D _texture;
        public Body body;
        public Vector2 position { get { return (body.Position*Singleton.screenScale); } set { body.Position = (value*Singleton.worldScale); } }
        protected Color colour;
        public SpriteEffects spriteEffects;
        public float rotation { get { return body.Rotation; } set { body.Rotation = value; } }
        protected Vector2 origin;
        public Vector2 scale;
        protected int movespeed;
        public Vector2 hitbox_size;
        public enum Side
        {
            Wisdom,
            Luck,
        }
        public Side side;

        public GameSprite(Texture2D texture)
        {
            isActive = true;
            _texture = texture;
            body = new Body();
            origin = new Vector2(texture.Width/2,texture.Height/2);
            position = Vector2.Zero;
            colour = Color.White;
            rotation = 0;
            scale = new Vector2(1, 1);
            movespeed = 5;
            hitbox_size = new Vector2(texture.Width, texture.Height);
            spriteEffects = SpriteEffects.None;
        }

        public virtual void Remove(World world)
        {
            world.Remove(body);
        }

        public virtual void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position*Singleton.worldScale, null, colour, rotation, origin, scale* Singleton.worldScale , spriteEffects, 0f);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
