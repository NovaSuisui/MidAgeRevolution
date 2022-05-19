using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using MidAgeRevolution.AllSprite.AllPlayer;
using System.Diagnostics;

namespace MidAgeRevolution.AllSprite
{
    class Bullet : GameSprite
    {
        public float damage { get { return baseDamage*damageBuff(); } set { baseDamage = value; } }
        public float baseDamage;

        public Bullet(Texture2D texture) : base(texture)
        {
            damage = 30;
        }

        public Bullet(Texture2D texture,Body bulletBody) : base(texture)
        {
            damage = 30;
            body = bulletBody;
            body.OnCollision += collisionHandler;
            body.OnSeparation += separationHandler;
            body.Tag = this;
        }

        // sender = วัตถุนี้, other = วัตถุที่ถูกชน  (*Fixture เป็นหน่วยย่อยของ Body)
        public virtual bool collisionHandler(Fixture sender, Fixture other, Contact contact)
        {
            isActive = false;

            //check obj contact with this
            Bullet bullet = (Bullet)sender.Body.Tag;
            var b = other.Body.Tag;
            if (b as Player != null)
            {
                /*Debug.WriteLine("itPlayer");*/
                Player player = (Player)b;
                onhitPlayer(player, bullet);
            }
            else if (b as Obstacle != null)
            {
                /*Debug.WriteLine("itObs");*/
                Obstacle obstacle = (Obstacle)b;
                onhitObstacle(obstacle,bullet);
            }

            else if (b as Bullet != null) Debug.WriteLine("itBull");
            else Debug.WriteLine("Unknown");
            
            return false;
        }

        public virtual void separationHandler(Fixture sender, Fixture other, Contact contact)
        {

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
                    break;
                case Singleton.GameState.LuckEndTurn:
                    break;
            }
            /*switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    position.X = gameObject[0].position.X + gameObject[0].hitbox_size.X;
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
                    position.X = gameObject[1].position.X - gameObject[1].hitbox_size.X;
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
                        position.X > Singleton.WINDOWS_SIZE_X - Singleton.Instance.TEXTURE_SIZE ||
                        position.Y < 0 ||
                        position.Y > Singleton.WINDOWS_SIZE_Y - Singleton.Instance.TEXTURE_SIZE)
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
                    Singleton.Instance._gameState = Singleton.GameState.LuckTurn;
                    side = Side_ID.Luck_player;

                    break;
                case Singleton.GameState.LuckEndTurn:
                    Singleton.Instance._gameState = Singleton.GameState.WisdomTurn;
                    side = Side_ID.Wisdom_player;

                    break;
            }
*/
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected void onhitPlayer(Player player,Bullet bullet)
        {
            if (player.side == bullet.side) player.ApplyDamage(bullet.damage / 2f);
            else player.ApplyDamage(bullet.damage);
            
            string side = (player.side == Side.Wisdom) ? "Wisdom" : "Luck";
            Debug.WriteLine($"{side} HP = {player.hit_point}");
        }

        protected void onhitObstacle(Obstacle obstacle,Bullet bullet)
        {
            obstacle.hit_point -= bullet.damage;
            string side = (obstacle.side == Side.Wisdom) ? "Wisdom" : "Luck";
            obstacle.hit_point = Math.Clamp(obstacle.hit_point, 0, 100);
            Debug.WriteLine($"{side}'bock HP = {obstacle.hit_point}");
        }

        private float damageBuff()
        {
            switch(Singleton.Instance.ammo)
            {
                case Singleton.AmmoType.normal:
                    return 1f;
                case Singleton.AmmoType.x0dmg:
                    return 0f;
                case Singleton.AmmoType.x0p5dmg:
                    return 0.5f;
                case Singleton.AmmoType.x1p5dmg:
                    return 1.5f;
                case Singleton.AmmoType.x2dmg:
                    return 2f;
                case Singleton.AmmoType.x3dmg:
                    return 3f;
                default: return 1f;
            }
        }
    }
}
