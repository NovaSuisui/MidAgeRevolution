using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MidAgeRevolution.AllSprite.AllPlayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using tainicom.Aether.Physics2D.Collision;
using tainicom.Aether.Physics2D.Collision.Shapes;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace MidAgeRevolution.AllSprite
{
    class explosionBullet : Bullet
    {
        float outer_radius;
        Body area;
        public explosionBullet(Texture2D texture, Body bulletBody) : base(texture, bulletBody)
        {
            damage = 40;
            outer_radius = 100f;
        }

        public void createExplosion(Vector2 position, World world)
        {
            area = world.CreateCircle(outer_radius * Singleton.worldScale, 1f, position*Singleton.worldScale);

        }
        public override bool collisionHandler(Fixture sender, Fixture other, Contact contact)
        {
            if (sameLastContact(other.Body)) return false;
            //check obj contact with this

            Bullet bullet = (Bullet)sender.Body.Tag;
            var b = other.Body.Tag;
            if (b as Player != null)
            {
                /*Debug.WriteLine("HitPlayer");*/
                Player player = (Player)b;
                isActive = false;
                createExplosion(bullet.position, bullet.body.World);
                
                return true;
            }
            else if (b as Obstacle != null)
            {
                Debug.WriteLine("HitObs");
                Obstacle obstacle = (Obstacle)b;
                isActive = false;
                createExplosion(bullet.position, bullet.body.World);
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
    }
}
