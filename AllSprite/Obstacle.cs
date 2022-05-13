using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllSprite
{
    class Obstacle : GameSprite
    {
        Texture2D test;

        public Obstacle(Texture2D texture) : base(texture)
        {
            test = texture;
            colour = Color.Gray;
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
                    break;
                case Singleton.GameState.WisdomShooting:
                    break;
                case Singleton.GameState.LuckShooting:
                    break;
                case Singleton.GameState.WisdomEndTurn:
                    if (hit_point < 33)
                    {
                        colour = Color.Red;
                    }
                    else if (hit_point < 66)
                    {
                        colour = Color.Orange;
                    }

                    if (hit_point < 1)
                    {
                        isActive = false;
                    }
                    break;
                case Singleton.GameState.LuckEndTurn:
                    if (hit_point < 33)
                    {
                        colour = Color.Red;
                    }
                    else if (hit_point < 66)
                    {
                        colour = Color.Orange;
                    }

                    if (hit_point < 1)
                    {
                        isActive = false;
                    }
                    break;
            }

            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(test, new Vector2(position.X, position.Y), null, colour, rotation, origin, scale, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }
    }
}
