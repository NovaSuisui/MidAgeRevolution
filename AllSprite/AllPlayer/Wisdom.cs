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

        public override Bullet createBullet()
        {
            World w = body.World;
            Body bulletBody = w.CreateCircle(10f * Singleton.worldScale, 1f, bodyType: BodyType.Dynamic);
            bulletBody.Mass = 1;
            Bullet bullet;
            switch (Singleton.Instance.ammo & Singleton.AmmoType.Behavior)
            {
                case Singleton.AmmoType.explosionBullet:
                    bullet = new explosionBullet(Singleton.Instance.flask, bulletBody);
                    break;
                case Singleton.AmmoType.bounceBullet:
                    bullet = new bounceBullet(Singleton.Instance.flask, bulletBody);
                    break;
                case Singleton.AmmoType.boostBullet:
                    bullet = new boostBullet(Singleton.Instance.flask, bulletBody);
                    break;
                case Singleton.AmmoType.nomalBullet:
                default:
                    bullet = new Bullet(Singleton.Instance.flask, bulletBody);
                    break;
            }
            return bullet;
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
                    if(statusEffect[Singleton.StatusEffect.fire] > 0)
                    {
                        ApplyDamage(5f);
                        statusEffect[Singleton.StatusEffect.fire]--;
                    }
                    break;
                case Singleton.GameState.LuckEndTurn:
                    break;
            }
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (disableControll == false)
                spriteBatch.DrawString(Singleton.Instance.testfont, String.Format("{0} degree", aimAngle), (position + new Vector2(-20, -80)) * Singleton.worldScale, Color.Black, 0, Vector2.Zero, Vector2.One * 0.8f * Singleton.worldScale, SpriteEffects.None, 0f);

            DrawHP(spriteBatch, Singleton.Instance.sc_hp_bar, new Vector2(101, 35), new Vector2(324, 31), new Vector2(247, 57));
            base.Draw(spriteBatch);
        }
    }
}
