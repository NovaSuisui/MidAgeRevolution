using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace MidAgeRevolution.AllSprite.AllPlayer
{
    class Luck : Player
    {
        Color hp_color;

        public Luck(Texture2D texture,World world) : base(texture,world)
        {
            side = Side.Luck;
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
                    controlHandler(gameObject, gameTime);
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
            /*
                        switch (Singleton.Instance._gameState)
                        {
                            case Singleton.GameState.Setup:
                                break;
                            case Singleton.GameState.WisdomTurn:
                                *//*position.Y += movespeed;
                                if (isHit(gameObject) ||
                                    position.Y > Singleton.WINDOWS_SIZE_Y - hitbox_size.Y)
                                {
                                    position.Y -= movespeed;
                                }*//*

                                break;
                            case Singleton.GameState.LuckTurn:
                                if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.W) &&
                                    position.Y > 0)
                                {
                                    position.Y -= movespeed * 2;
                                    if (isHit(gameObject))
                                    {
                                        position.Y += movespeed;
                                    }
                                }
                                *//*else
                                {
                                    if (position.Y < Singleton.WINDOWS_SIZE_Y - hitbox_size.Y) position.Y += movespeed;
                                    if (isHit(gameObject))
                                    {
                                        position.Y -= movespeed;
                                    }
                                }*//*
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
                            case Singleton.GameState.WisdomShooting:
                                *//*position.Y += movespeed;
                                if (isHit(gameObject) ||
                                    position.Y > Singleton.WINDOWS_SIZE_Y - hitbox_size.Y)
                                {
                                    position.Y -= movespeed;
                                }*//*

                                break;
                            case Singleton.GameState.LuckShooting:
                                *//*position.Y += movespeed;
                                if (isHit(gameObject) ||
                                    position.Y > Singleton.WINDOWS_SIZE_Y - hitbox_size.Y)
                                {
                                    position.Y -= movespeed;
                                }*//*

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
                        }
            */
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            DrawHP(spriteBatch, Singleton.Instance.rl_hp_bar, new Vector2(997, 35), new Vector2(324, 31), new Vector2(1028, 57),true);
            base.Draw(spriteBatch);
        }
    }
}
