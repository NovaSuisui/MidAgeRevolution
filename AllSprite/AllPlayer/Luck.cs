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
                    if (statusEffect[Singleton.StatusEffect.fire] > 0)
                    {
                        ApplyDamage(5f);
                        statusEffect[Singleton.StatusEffect.fire]--;
                    }
                    break;
            }
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            DrawHP(spriteBatch, Singleton.Instance.rl_hp_bar, new Vector2(998, 35), new Vector2(324, 31), new Vector2(1028, 57),true);
            base.Draw(spriteBatch);
        }

        public override Bullet createBullet()
        {
            World w = body.World;
            Body bulletBody = w.CreateCircle(10f * Singleton.worldScale, 1f, bodyType: BodyType.Dynamic);
            bulletBody.Mass = 0;
            Bullet bullet;
            switch (Singleton.Instance.ammo & Singleton.AmmoType.Behavior)
            {
                case Singleton.AmmoType.explosionBullet:
                    bullet = new explosionBullet(Singleton.Instance.Content.Load<Texture2D>("Test/test0"), bulletBody);
                    break;
                case Singleton.AmmoType.bounceBullet:
                    bullet = new bounceBullet(Singleton.Instance.Content.Load<Texture2D>("Test/test0"), bulletBody)
                    {
                        bounceTime = (uint)Singleton.Instance.rnd.Next(0, 4),
                    };
                    break;
                case Singleton.AmmoType.boostBullet:
                    bullet = new boostBullet(Singleton.Instance.Content.Load<Texture2D>("Test/test0"), bulletBody);
                    break;
                case Singleton.AmmoType.nomalBullet:
                default:
                    bullet = new Bullet(Singleton.Instance.Content.Load<Texture2D>("Test/test0"), bulletBody);
                    break;
            }
            return bullet;
        }
    }
}
