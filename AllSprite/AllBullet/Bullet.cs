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
        private Body prvContact;
        private double prvContactTime;

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
            if (sameLastContact(other.Body)) return false;
            //check obj contact with this

            Bullet bullet = (Bullet)sender.Body.Tag;
            var b = other.Body.Tag;
            if (b as Player != null)
            {
                /*Debug.WriteLine("HitPlayer");*/
                Player player = (Player)b;
                onhitPlayer(player, bullet);
                isActive = false;
                return true;
            }
            else if (b as Obstacle != null)
            {
                /*Debug.WriteLine("HitObs");*/
                Obstacle obstacle = (Obstacle)b;
                onhitObstacle(obstacle, bullet);
                isActive = false;
                return true;
            }

            else if (b as Bullet != null)
            {
                Debug.WriteLine("HitBull");
                return false;
            }
            else
            {
                Debug.WriteLine("Unknown");
                isActive = false;
                return true;
            }
        }

        protected bool sameLastContact(Body contact)
        {
            double curtime = Singleton.Instance._time.TotalGameTime.TotalSeconds;
            if (prvContact == null)
            {
                prvContact = contact;
                prvContactTime = Singleton.Instance._time.TotalGameTime.TotalSeconds;
                return false;
            }
            if (contact == prvContact && curtime - prvContactTime < 1) return true;
            else return false;
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
                    body.BodyType = BodyType.Dynamic;
                    break;
                case Singleton.GameState.LuckShooting:
                    body.BodyType = BodyType.Dynamic;
                    break;
                case Singleton.GameState.WisdomEndTurn:
                    break;
                case Singleton.GameState.LuckEndTurn:
                    break;
            }
            
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected void onhitPlayer(Player player,Bullet bullet)
        {
            if (player.side == bullet.side) player.ApplyDamage(bullet.damage / 2f);
            else
            {
                player.ApplyDamage(bullet.damage);
                switch(Singleton.Instance.ammo & Singleton.AmmoType.DebufAmmo)
                {
                    case Singleton.AmmoType.fire_debuf:
                        player.ApplyStatus(Singleton.StatusEffect.fire);
                        break;
                }
            }
            
            string side = (player.side == Side.Wisdom) ? "Wisdom" : "Luck";
            Debug.WriteLine($"{side} HP = {player.hit_point}");
        }

        protected void onhitObstacle(Obstacle obstacle,Bullet bullet)
        {
            if (obstacle.side == bullet.side) obstacle.ApplyDamage(bullet.damage);
            else
            {
                obstacle.ApplyDamage(bullet.damage);
                switch (Singleton.Instance.ammo & Singleton.AmmoType.DebufAmmo)
                {
                    case Singleton.AmmoType.fire_debuf:
                        obstacle.ApplyStatus(Singleton.StatusEffect.fire);
                        break;
                }
            }
            string side = (obstacle.side == Side.Wisdom) ? "Wisdom" : "Luck";
            Debug.WriteLine($"{side}'bock HP = {obstacle.hit_point}");
        }

        protected float damageBuff()
        {
            switch(Singleton.Instance.ammo & Singleton.AmmoType.MultiplyDamage)
            {
                case Singleton.AmmoType.x1dmg:
                    return 1f;
                case Singleton.AmmoType.x2dmg:
                    return 2f;
                case Singleton.AmmoType.x3dmg:
                    return 3f;
                case Singleton.AmmoType.xrndBdmg:
                    return (Singleton.Instance.rnd.Next(0, 16) / 10f);
                default: return 1f;
            }
        }
    }
}
