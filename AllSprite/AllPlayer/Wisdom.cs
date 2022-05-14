using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using MidAgeRevolution.AllSprite;

namespace MidAgeRevolution.AllSprite.AllPlayer
{
    class Wisdom : Player
    {
        Texture2D test;
        public Color hp_color;

        public Wisdom(Texture2D texture) : base(texture)
        {
            test = texture;
            colour = Color.Green;
            hp_color = Color.Green;
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.W) &&
                        position.Y > 0)
                    {
                        position.Y -= movespeed;
                        if (isHit(gameObject))
                        {
                            position.Y += movespeed;
                        }
                    }
                    else
                    {
                        if(position.Y < Singleton.WINDOWS_SIZE_Y - hitbox_size.Y) position.Y += movespeed;
                        if (isHit(gameObject))
                        {
                            position.Y -= movespeed;
                        }
                    }
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.A) &&
                        position.X > 0)
                    {
                        position.X -= movespeed;
                        if (isHit(gameObject))
                        {
                            position.X += movespeed;
                        }
                    }
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.S) &&
                        position.Y < Singleton.WINDOWS_SIZE_Y - hitbox_size.Y)
                    {
                        position.Y += movespeed;
                        if (isHit(gameObject))
                        {
                            position.Y -= movespeed;
                        }
                    }
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.D) &&
                        position.X < Singleton.WINDOWS_SIZE_X - hitbox_size.X)
                    {
                        position.X += movespeed;
                        if (isHit(gameObject))
                        {
                            position.X -= movespeed;
                        }
                    }

                    break;
                case Singleton.GameState.LuckTurn:
                    break;
                case Singleton.GameState.WisdomShooting:
                    position.Y += movespeed;
                    if (isHit(gameObject) ||
                        position.Y > Singleton.WINDOWS_SIZE_Y - hitbox_size.Y)
                    {
                        position.Y -= movespeed;
                    }

                    break;
                case Singleton.GameState.LuckShooting:
                    position.Y += movespeed;
                    if (isHit(gameObject) ||
                        position.Y > Singleton.WINDOWS_SIZE_Y - hitbox_size.Y)
                    {
                        position.Y -= movespeed;
                    }

                    break;
                case Singleton.GameState.WisdomEndTurn:
                    break;
                case Singleton.GameState.LuckEndTurn:
                    if (hit_point < 33)
                    {
                        hp_color = Color.Red;
                    }
                    else if (hit_point < 66)
                    {
                        hp_color = Color.Orange;
                    }

                    if (hit_point < 1)
                    {
                        colour = Color.Red;
                        isActive = false;
                    }
                    break;
            }

            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(test, new Vector2(200, 100), null, hp_color, rotation, origin, new Vector2(5 * (hit_point / 100), 1), SpriteEffects.None, 0f);
            spriteBatch.Draw(test, new Vector2(position.X, position.Y), null, colour, rotation, origin, scale, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }
    }
}
