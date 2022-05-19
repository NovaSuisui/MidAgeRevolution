using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using MidAgeRevolution.AllSprite;

using tainicom.Aether.Physics2D.Dynamics;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace MidAgeRevolution.AllSprite.AllPlayer
{
    class Wisdom : Player
    {   
        public Wisdom(Texture2D texture,World world) : base(texture, world)
        {
            side = Side.Wisdom;
            
            // setup body
            body.OnCollision += collisionHandler;
            body.OnSeparation += separationHandler;
        }
        public override bool collisionHandler(Fixture sender, Fixture other, Contact contact)
        {
            return true;
        }

        public override void separationHandler(Fixture sender, Fixture other, Contact contact)
        {
            base.separationHandler(sender, other, contact);
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    controlHandler(gameObject,gameTime);

                    break;
                case Singleton.GameState.LuckTurn:

                    break;
                case Singleton.GameState.WisdomShooting:
                   
                    
                    break;
                case Singleton.GameState.LuckShooting:
                    
                    break;
                case Singleton.GameState.WisdomEndTurn:
                    break;
                case Singleton.GameState.LuckEndTurn:
                    break;
            }

            /*switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.W) &&
                        position.Y > 0)
                    {
                        position.Y -= movespeed * 2;
                        if (isHit(gameObject))
                        {
                            position.Y += movespeed;
                        }
                    }
                    else
                    {
                        if (position.Y < Singleton.WINDOWS_SIZE_Y - hitbox_size.Y) position.Y += movespeed;
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
                    position.Y += movespeed;
                    if (isHit(gameObject) ||
                        position.Y > Singleton.WINDOWS_SIZE_Y - hitbox_size.Y)
                    {
                        position.Y -= movespeed;
                    }

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
                    break;
            }

            position.Y += movespeed;
            if (isHit(gameObject))
            {
                position.Y -= movespeed;
            }
            else if (position.Y > Singleton.WINDOWS_SIZE_Y - hitbox_size.Y)
            {
                position.Y -= movespeed;
                hit_point = 0;
            }

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
            }*/

            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawHP(spriteBatch, Singleton.Instance.sc_hp_bar, new Vector2(100, 35), new Vector2(324, 31), new Vector2(247, 57));
            base.Draw(spriteBatch);
        }
    }
}
