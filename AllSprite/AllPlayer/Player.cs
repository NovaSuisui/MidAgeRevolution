using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace MidAgeRevolution.AllSprite.AllPlayer
{
    class Player : GameSprite
    {
        float Movespeed = 20f;
        public bool turnLeft { get { return leftDirection; } set { leftDirection = value; spriteEffects = (value) ? SpriteEffects.FlipHorizontally : SpriteEffects.None; } }
        private bool leftDirection = false;

        public float aimAngle= 0;
        public float power = 0;
        public Vector2 collisionBox = new Vector2(50f, 82f);
        public float limitSpeed = 5;
        public Vector3[] HPcolors = { new Vector3(113, 237, 104), new Vector3(236, 213, 35), new Vector3(194,3,3) };
        public float[] HPcolorpoint = { 100, 30, 0 };
        private uint _HPcolor_ptr1;
        private uint _HPcolor_ptr2;
        public static float wind = 0.0f;


        public float MaxHP = 300f;
        public float hit_point = 300f;
        public float prv_hitpoint = 300f;
        public float damageTimer = 0.0f;
        float animated_HP = 300f;
        public bool isAlive = true;

        public IDictionary<Singleton.StatusEffect, uint> statusEffect = new Dictionary<Singleton.StatusEffect, uint>()
        {
            { Singleton.StatusEffect.fire, 0},
        };

        public void ApplyDamage(float Damage)
        {
            prv_hitpoint = animated_HP; 
            damageTimer = 0.5f;
            hit_point = hit_point - Damage;
            hit_point = Math.Clamp(hit_point, 0, MaxHP);
        }

        public void ApplyStatus(Singleton.StatusEffect statusEffect)
        {
            if (statusEffect == Singleton.StatusEffect.fire) this.statusEffect[Singleton.StatusEffect.fire] = 5;
        }

        public Player(Texture2D texture, World world) : base(texture)
        {
            //body = world.CreateRectangle(collisionBox.X * Singleton.worldScale, collisionBox.Y * Singleton.worldScale, 1f, bodyType: BodyType.Dynamic);
            body = world.CreateBody();
            body.CreateRectangle(30 * Singleton.worldScale, 35 * Singleton.worldScale,1,new Vector2(0,5*Singleton.worldScale));
            body.CreateCircle(7 * Singleton.worldScale, 1,new Vector2(0,-22*Singleton.worldScale));
            body.BodyType = BodyType.Dynamic;
            body.FixedRotation = true;
            body.OnCollision += collisionHandler;
            body.OnSeparation += separationHandler;
            body.Tag = this;
            body.SetCollisionCategories(Category.Cat2);
            _HPcolor_ptr1 = 0;
            _HPcolor_ptr2 = 1;
            for(int i = 0; i < HPcolors.Length; i++)
            {
                HPcolors[i] = HPcolors[i]/255;
            }
        }

        public virtual bool collisionHandler(Fixture sender, Fixture other, Contact contact)
        {
            return true;
        }

        public virtual void separationHandler(Fixture sender, Fixture other, Contact contact)
        {
        }

        #region move Method
        public void moveLeft()
        {
            if (body.LinearVelocity.X > 0) body.ResetDynamics();
            if(Math.Abs(body.LinearVelocity.X) < limitSpeed) body.ApplyForce(new Vector2(-Movespeed,-15f));
            spriteEffects = SpriteEffects.FlipHorizontally;
            turnLeft = true;
        }

        public void moveRight()
        {
            if (body.LinearVelocity.X < 0) body.ResetDynamics();
            if (Math.Abs(body.LinearVelocity.X) < limitSpeed) body.ApplyForce(new Vector2(Movespeed, -15f));
            spriteEffects = SpriteEffects.None;
            turnLeft = false;
        }

        private int i = 1;
        public virtual void controlHandler(List<GameSprite> gameObject, GameTime gameTime)
        {
            if (freeFall()) return;
            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Space))
            {
                charger(Keys.Space);
            }
            if (Singleton.Instance.CurrentKey.IsKeyUp(Keys.Space) && Singleton.Instance.PrevoiusKey.IsKeyDown(Keys.Space))
            {
                if (Singleton.Instance._gameState == Singleton.GameState.WisdomTurn) Singleton.Instance._nextGameState = Singleton.GameState.WisdomShooting;
                if (Singleton.Instance._gameState == Singleton.GameState.LuckTurn) Singleton.Instance._nextGameState = Singleton.GameState.LuckShooting;
            }



            if(Singleton.Instance.CurrentKey.IsKeyUp(Keys.Space))
            {
                if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.W))
                {
                    aimAngle += 1;
                    if (aimAngle > 80) aimAngle = 80;
                    Debug.WriteLine(aimAngle);
                }
                if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.S))
                {
                    aimAngle -= 1;
                    if (aimAngle < -30) aimAngle = -30;
                    Debug.WriteLine(aimAngle);
                }
                if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.A))
                {
                    moveLeft();
                }
                else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.D))
                {
                    moveRight();
                }
                else if (Singleton.Instance.CurrentKey.IsKeyUp(Keys.A) && Singleton.Instance.CurrentKey.IsKeyUp(Keys.D))
                {
                    body.LinearVelocity *= new Vector2(0, 1);
                }
            }

        }

        public void charger(Keys keys)
        {
            if (Singleton.Instance.PrevoiusKey.IsKeyUp(keys)) power = 1;
            if (power % 100 == 0) i = -i;
            power += i;
            Debug.WriteLine(power);
        }
        public bool freeFall()
        {
            return body.ContactList == null;
        }
        #endregion
        public void shoot(List<GameSprite> gameObject)
        {
            switch(Singleton.Instance.ammo & Singleton.AmmoType.Controller)
            {
                case Singleton.AmmoType.projectile:
                    shootProjectile(gameObject);
                    break;
                case Singleton.AmmoType.useItem:
                    useItem(gameObject);
                    break;
                    
            }
            
        }

        public void useItem(List<GameSprite> gameObject)
        {
            switch(Singleton.Instance.ammo & Singleton.AmmoType.itemNumber)
            {
                case Singleton.AmmoType.otk:
                    Item.otk(gameObject);
                    break;
            }
        }

        public void shootProjectile(List<GameSprite> gameObject)
        {
            int bulletPerShot = 1;
            switch (Singleton.Instance.ammo & Singleton.AmmoType.Shooting)
            {
                case Singleton.AmmoType.x3ammo:
                    bulletPerShot = 3;
                    break;
                case Singleton.AmmoType.xrndammo:
                    bulletPerShot = Singleton.Instance.rnd.Next(1, 7);
                    Debug.WriteLine(bulletPerShot);
                    break;
                case Singleton.AmmoType.x1ammo:
                default:
                    bulletPerShot = 1;
                    break;
            }

            int mid = bulletPerShot / 2;
            for (int i = 0; i < bulletPerShot; i++)
            {
                float bulletAngle = aimAngle + (i - mid) * 30f / bulletPerShot;
                float x = (float)Math.Cos(Singleton.Degree2Radian(bulletAngle));
                float y = (float)-Math.Sin(Singleton.Degree2Radian(bulletAngle));
                if (turnLeft) x = -x;
                Bullet bullet = createBullet();
                bullet.body.Position = (position + new Vector2(x, y) * 60) * Singleton.worldScale;
                // ใส่แรงยิง
                bullet.body.ApplyForce(new Vector2(x, y) * (400 + power * 10));
                // ใส่แรงลม
                if ((Singleton.Instance.ammo & Singleton.AmmoType.turnOffWind) != 0)
                    bullet.body.ApplyForce(new Vector2(wind * 20, 0));
                // หมุนดิ้ว
                bullet.body.ApplyTorque((turnLeft) ? -5 : 5);
                bullet.side = side;
                gameObject.Add(bullet);
            }

            if ((int)(Singleton.Instance.ammo & Singleton.AmmoType.applyPhysics) != 0)
            {
                foreach (GameSprite sprite in gameObject)
                {
                    sprite.body.BodyType = BodyType.Dynamic;
                }
            }

        }

        public virtual Bullet createBullet()
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
                    bullet = new bounceBullet(Singleton.Instance.Content.Load<Texture2D>("Test/test0"), bulletBody);
                    break;
                case Singleton.AmmoType.boostBullet:
                    bullet = new boostBullet(Singleton.Instance.Content.Load<Texture2D>("Test/test0"), bulletBody);
                    break;
                case Singleton.AmmoType.nomalBullet:
                default :
                    bullet = new Bullet(Singleton.Instance.Content.Load<Texture2D>("Test/test0"), bulletBody);
                    break;
            }
            return bullet;
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            if(damageTimer > 0)
            {
                damageTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                damageTimer = Math.Clamp(damageTimer, 0, 0.5f);
                float dv = prv_hitpoint - hit_point;
                animated_HP = hit_point + (dv * (damageTimer / 0.5f));
            }
            //if (hit_point <= 0 || position.Y > Singleton.WINDOWS_SIZE_Y) isActive = false;
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
            if (position.Y > Singleton.WINDOWS_SIZE_Y + 250 && Singleton.Instance._gameState != Singleton.GameState.End) this.ApplyDamage(hit_point);
            if (hit_point <= 0 && isAlive)
            { 
                isAlive = false;
                Remove(body.World);
            }
            
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float p = power/MaxHP;
            Color color = new Color((p * new Vector3(255, 38, 0) + (1 - p) * new Vector3(0, 0, 0))/255);
            float x = (float)Math.Cos(Singleton.Degree2Radian(aimAngle));
            float y = (float)Math.Sin(Singleton.Degree2Radian(aimAngle));
            Vector2 arrowPositon = (position + new Vector2((turnLeft) ? -x : x, -y) * 50) * Singleton.worldScale;
            float arrowRotation = Singleton.Degree2Radian(aimAngle) * ((turnLeft) ? 1 : -1);
            Vector2 arrowOrigin = new Vector2(Singleton.Instance.Arrow.Width / 2, Singleton.Instance.Arrow.Height / 2);
            spriteBatch.Draw(Singleton.Instance.Arrow, arrowPositon, null, color, arrowRotation, arrowOrigin, Vector2.One * Singleton.worldScale, spriteEffects, 0f);
            base.Draw(spriteBatch);
        }

        public void DrawHP(SpriteBatch spriteBatch,Texture2D border, Vector2 borderPosition, Vector2 hpSize, Vector2 hpPosition, bool decreedToRight = false)
        {
            if (animated_HP > HPcolorpoint[_HPcolor_ptr1] && _HPcolor_ptr2 > 0)
            {
                _HPcolor_ptr2--;
                if (_HPcolor_ptr1 > 0) _HPcolor_ptr1--;
            }
            if (animated_HP < HPcolorpoint[_HPcolor_ptr2] && _HPcolor_ptr1 < HPcolors.Length - 1)
            {
                _HPcolor_ptr1++;
                if (_HPcolor_ptr2 < HPcolors.Length - 1) _HPcolor_ptr2++;
            }
            float MIN = (_HPcolor_ptr1 == HPcolors.Length - 1)? float.MinValue :HPcolorpoint[_HPcolor_ptr2];
            float MAX = (_HPcolor_ptr2 == 0)? float.MaxValue : HPcolorpoint[_HPcolor_ptr1];
            float p = (animated_HP - MIN)/ (MAX - MIN);
            Color hp_color = new Color(p * HPcolors[_HPcolor_ptr1] + (1 - p) * HPcolors[_HPcolor_ptr2]);
            spriteBatch.Draw(border, borderPosition * Singleton.worldScale, null, Color.White, 0f, Vector2.Zero, 1f * Singleton.worldScale, SpriteEffects.None, 0f);
            if(decreedToRight)
            {
                spriteBatch.Draw(Singleton.Instance.ghb, new Vector2(hpPosition.X+hpSize.X, hpPosition.Y) * Singleton.worldScale, null, hp_color, 0f, new Vector2(1, 0f), new Vector2(hpSize.X * (animated_HP / MaxHP), hpSize.Y) * Singleton.worldScale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(Singleton.Instance.ghb, hpPosition * Singleton.worldScale, null, hp_color, 0f, new Vector2(0, 0f), new Vector2(hpSize.X * (animated_HP / MaxHP), hpSize.Y) * Singleton.worldScale, SpriteEffects.None, 0f);
            }
        }
    }
}
