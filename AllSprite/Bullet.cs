using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllSprite
{
    class Bullet : GameSprite
    {
        public float mass;
        public Vector2 velocity;
        public Vector2 damage;

        public Bullet(Texture2D texture) : base(texture)
        {

        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
