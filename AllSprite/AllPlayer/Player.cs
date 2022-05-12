using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllSprite.AllPlayer
{
    class Player : GameSprite
    {
        protected int movespeed;

        public Player(Texture2D texture) : base(texture)
        {
            movespeed = 5;
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected bool isHit(List<GameSprite> gameObject)
        {
            foreach (var obj in gameObject)
            {
                if (obj.hitbox.Intersects(hitbox) &&
                    obj.side != this.side)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
