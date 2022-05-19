using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace MidAgeRevolution.AllSprite
{
    class Obstacle : GameSprite
    {
        public float hit_point = 100f;
        public Obstacle(Texture2D texture,World world) : base(texture)
        {
            colour = Color.White;

            float height = 98f;
            float width = 30f;

            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1, bodyType: BodyType.Dynamic);
            body.FixedRotation = true;
            body.OnCollision += collisionHandler;
            body.Tag = this;
        }

        bool collisionHandler(Fixture sender, Fixture other, Contact contact)
        {
            
            return true;
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    if(Singleton.Instance._prvGameState != Singleton.GameState.WisdomTurn)
                    {
                        body.ResetDynamics();
                        body.BodyType = BodyType.Kinematic;
                        body.FixedRotation = true;
                    }
                    break;
                case Singleton.GameState.LuckTurn:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.LuckTurn)
                    {
                        body.ResetDynamics();
                        body.BodyType = BodyType.Kinematic;
                        body.FixedRotation = true;
                    }
                    break;
                case Singleton.GameState.WisdomShooting:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.WisdomShooting)
                    {
                        body.ResetDynamics();
                        body.BodyType = BodyType.Dynamic;
                        body.FixedRotation = false;
                    }
                    break;
                case Singleton.GameState.LuckShooting:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.LuckShooting)
                    {
                        body.ResetDynamics();
                        body.BodyType = BodyType.Dynamic;
                        body.FixedRotation = false;
                    }
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

            if(hit_point<=0)
            {
                isActive = false;
            }

            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
