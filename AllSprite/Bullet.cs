using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllSprite
{
    class Bullet : GameSprite
    {
        private Texture2D test;

        public float mass;
        public Vector2 velocity;
        public float damage;

        public Bullet(Texture2D texture) : base(texture)
        {
            test = texture;
            damage = 30;
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    position.X = gameObject[0].position.X + Singleton.Instance.TEXTURE_SIZE;
                    position.Y = gameObject[0].position.Y;

                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        Singleton.Instance._gameState = Singleton.GameState.WisdomShooting;
                        velocity = new Vector2(
                                (float)(1 * movespeed * Math.Cos((float)Math.Atan2((double)(Singleton.Instance.CurrentMouse.Y - position.Y), (double)(Singleton.Instance.CurrentMouse.X - position.X)))),
                                (float)(1 * movespeed * Math.Sin((float)Math.Atan2((double)(Singleton.Instance.CurrentMouse.Y - position.Y), (double)(Singleton.Instance.CurrentMouse.X - position.X))))
                                );
                    }

                    break;
                case Singleton.GameState.LuckTurn:
                    position.X = gameObject[1].position.X - Singleton.Instance.TEXTURE_SIZE;
                    position.Y = gameObject[1].position.Y;

                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        Singleton.Instance._gameState = Singleton.GameState.LuckShooting;
                        velocity = new Vector2(
                                (float)(1 * movespeed * Math.Cos((float)Math.Atan2((double)(Singleton.Instance.CurrentMouse.Y - position.Y), (double)(Singleton.Instance.CurrentMouse.X - position.X)))),
                                (float)(1 * movespeed * Math.Sin((float)Math.Atan2((double)(Singleton.Instance.CurrentMouse.Y - position.Y), (double)(Singleton.Instance.CurrentMouse.X - position.X))))
                                );
                    }

                    break;
                case Singleton.GameState.WisdomShooting:
                    position.X += velocity.X;
                    position.Y += velocity.Y;

                    if (isBulletHit(gameObject))
                    {
                        Singleton.Instance._gameState = Singleton.GameState.WisdomEndTurn;
                        side = Side_ID.Luck_player;
                    }

                    if (position.X < 0 ||
                        position.X > Singleton.WINDOWS_SIZE_X ||
                        position.Y < 0 ||
                        position.Y > Singleton.WINDOWS_SIZE_Y)
                    {
                        Singleton.Instance._gameState = Singleton.GameState.WisdomEndTurn;
                        side = Side_ID.Luck_player;
                    }

                    break;
                case Singleton.GameState.LuckShooting:
                    position.X += velocity.X;
                    position.Y += velocity.Y;

                    if (isBulletHit(gameObject))
                    {
                        Singleton.Instance._gameState = Singleton.GameState.LuckEndTurn;
                        side = Side_ID.Wisdom_player;
                    }

                    if (position.X < 0 ||
                        position.X > Singleton.WINDOWS_SIZE_X - Singleton.Instance.TEXTURE_SIZE ||
                        position.Y < 0 ||
                        position.Y > Singleton.WINDOWS_SIZE_Y - Singleton.Instance.TEXTURE_SIZE)
                    {
                        Singleton.Instance._gameState = Singleton.GameState.LuckEndTurn;
                        side = Side_ID.Wisdom_player;
                    }

                    break;
                case Singleton.GameState.WisdomEndTurn:
                        break;
                case Singleton.GameState.LuckEndTurn:
                        break;
            }

            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(test, new Vector2(position.X, position.Y), null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }

        protected bool isBulletHit(List<GameSprite> gameObject)
        {
            foreach (var obj in gameObject)
            {
                if (obj.hitbox.Intersects(hitbox))
                {
                    if (this.side == Side_ID.Wisdom_player && (obj.side == Side_ID.Luck_player || obj.side == Side_ID.Luck_obstacle))
                    {
                        obj.hit_point -= this.damage;
                        return true;
                    }
                    else if (this.side == Side_ID.Luck_player && (obj.side == Side_ID.Wisdom_player || obj.side == Side_ID.Wisdom_obstacle))
                    {
                        obj.hit_point -= this.damage;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
