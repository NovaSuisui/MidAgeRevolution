using Microsoft.Xna.Framework.Graphics;
using MidAgeRevolution.AllSprite.AllPlayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace MidAgeRevolution.AllSprite
{
    class bounceBullet : Bullet
    {
        uint bounceTime;
        public bounceBullet(Texture2D texture,Body body) : base(texture, body)
        {
            damage = 30;
            bounceTime = 2;
            body.SetRestitution(1f);

        }
        public void checkBounce()
        {
            if (bounceTime > 0)
            {
                bounceTime--;
            }
            else
            {
                isActive = false;
            }
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
                onhitPlayer(player, bullet);
                checkBounce();
                return true;
            }
            else if (b as Obstacle != null)
            {
                /*Debug.WriteLine("HitObs");*/
                Obstacle obstacle = (Obstacle)b;
                onhitObstacle(obstacle, bullet);
                checkBounce();
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
                checkBounce();
                return true;
            }
        }
    }
}
