using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllSprite.AllPlayer
{
    class Luck : Player
    {
        Texture2D test;
        Color hp_color;

        public Luck(Texture2D texture) : base(texture)
        {
            test = texture;
            colour = Color.Yellow;
            hp_color = Color.Green;
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    break;
                case Singleton.GameState.LuckTurn:
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
                        if (position.Y < 840) position.Y += movespeed;
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
                        position.Y < 840)
                    {
                        position.Y += movespeed;
                        if (isHit(gameObject))
                        {
                            position.Y -= movespeed;
                        }
                    }
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.D) &&
                        position.X < 1540)
                    {
                        position.X += movespeed;
                        if (isHit(gameObject))
                        {
                            position.X -= movespeed;
                        }
                    }

                    break;
                case Singleton.GameState.WisdomShooting:
                    position.Y += movespeed;
                    if (isHit(gameObject) ||
                        position.Y < 1)
                    {
                        position.Y -= movespeed;
                    }

                    break;
                case Singleton.GameState.LuckShooting:
                    position.Y += movespeed;
                    if (isHit(gameObject) ||
                        position.Y < 1)
                    {
                        position.Y -= movespeed;
                    }

                    break;
                case Singleton.GameState.WisdomEndTurn:
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
                case Singleton.GameState.LuckEndTurn:
                    break;
            }


            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(test, new Vector2(1400, 100), null, hp_color, rotation, new Vector2( 60, 0), new Vector2(5 * (hit_point / 100), 1), SpriteEffects.None, 0f);
            spriteBatch.Draw(test, new Vector2(position.X, position.Y), null, colour, rotation, origin, scale, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }
    }
}
