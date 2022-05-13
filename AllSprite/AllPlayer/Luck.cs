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

        public Luck(Texture2D texture) : base(texture)
        {
            test = texture;
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


                    break;
                case Singleton.GameState.LuckShooting:


                    break;
            }


            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(test, new Vector2(position.X, position.Y), null, Color.Yellow, 0, origin, scale, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }
    }
}
