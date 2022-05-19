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

        public float hit_point = 100f;
        

        public Player(Texture2D texture, World world) : base(texture)
        {
            //body = world.CreateRectangle(collisionBox.X * Singleton.worldScale, collisionBox.Y * Singleton.worldScale, 1f, bodyType: BodyType.Dynamic);
            body = world.CreateBody();
            body.CreateRectangle(32 * Singleton.worldScale, 60 * Singleton.worldScale,1,new Vector2(0,10*Singleton.worldScale));
            body.CreateCircle(10 * Singleton.worldScale, 1,new Vector2(0,-30*Singleton.worldScale));
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
            if(Math.Abs(body.LinearVelocity.X) < limitSpeed) body.ApplyForce(new Vector2(-Movespeed,-10f));
            spriteEffects = SpriteEffects.FlipHorizontally;
            turnLeft = true;
        }

        public void moveRight()
        {
            if (Math.Abs(body.LinearVelocity.X) < limitSpeed) body.ApplyForce(new Vector2(Movespeed, -10f));
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
            else if (Singleton.Instance.CurrentKey.IsKeyUp(Keys.Space) && Singleton.Instance.PrevoiusKey.IsKeyDown(Keys.Space))
            {
                shoot(gameObject);
            }
            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.W))
            {
                aimAngle += 1;
                if (aimAngle > 85) aimAngle = 85;
                Debug.WriteLine(aimAngle);
            }
            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.S))
            {
                aimAngle -= 1;
                if (aimAngle < -30) aimAngle = -30;
                Debug.WriteLine(aimAngle);
            }
            else if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.A))
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
            /*if (body.ContactList != null)
            {
                return false;
            }
            else
            {
                body.LinearVelocity *= new Vector2(0, 1);
                return true;
            }*/
        }
        #endregion

        public void shoot(List<GameSprite> gameObject)
        {
            World w = body.World;
            
            Body bulletBody = w.CreateCircle(10f*Singleton.worldScale, 1f, bodyType: BodyType.Dynamic);
            float x = (float)Math.Cos(Singleton.Degree2Radian(aimAngle));
            float y = (float)Math.Sin(Singleton.Degree2Radian(aimAngle));
            bulletBody.Mass = 0;
            bulletBody.Position = (position + new Vector2((turnLeft)? -x:x, -y)*60)*Singleton.worldScale;
            bulletBody.ApplyForce(new Vector2((turnLeft) ? -x : x, -y) * (400+power*10));
            bulletBody.ApplyForce(new Vector2(wind*20, 0));
            bulletBody.ApplyTorque((turnLeft)?-5 :5f);

            Bullet bullet = new Bullet(Singleton.Instance.Content.Load<Texture2D>("Test/test0"), bulletBody)
            {
                damage = 20,
            };
            bullet.side = side;
            gameObject.Add(bullet);
            if (Singleton.Instance._gameState == Singleton.GameState.WisdomTurn) Singleton.Instance._nextGameState = Singleton.GameState.WisdomShooting;
            if (Singleton.Instance._gameState == Singleton.GameState.LuckTurn) Singleton.Instance._nextGameState = Singleton.GameState.LuckShooting;

        }
        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            if (hit_point <= 0 || position.Y > Singleton.WINDOWS_SIZE_Y) isActive = false;
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
            base.Update(gameObject, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float p = power/100f;
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
            if(hit_point > HPcolorpoint[_HPcolor_ptr1] && _HPcolor_ptr2 > 0)
            {
                _HPcolor_ptr2--;
                if (_HPcolor_ptr1 > 0) _HPcolor_ptr1--;
            }
            if (hit_point < HPcolorpoint[_HPcolor_ptr2] && _HPcolor_ptr1 < HPcolors.Length - 1)
            {
                _HPcolor_ptr1++;
                if (_HPcolor_ptr2 < HPcolors.Length - 1) _HPcolor_ptr2++;
            }
            float MIN = (_HPcolor_ptr1 == HPcolors.Length - 1)? float.MinValue :HPcolorpoint[_HPcolor_ptr2];
            float MAX = (_HPcolor_ptr2 == 0)? float.MaxValue : HPcolorpoint[_HPcolor_ptr1];
            float p = (hit_point - MIN)/ (MAX - MIN);
            Color hp_color = new Color(p * HPcolors[_HPcolor_ptr1] + (1 - p) * HPcolors[_HPcolor_ptr2]);
            spriteBatch.Draw(border, borderPosition * Singleton.worldScale, null, Color.White, 0f, Vector2.Zero, 1f * Singleton.worldScale, SpriteEffects.None, 0f);
            if(decreedToRight)
            {
                spriteBatch.Draw(Singleton.Instance.ghb, new Vector2(hpPosition.X+hpSize.X, hpPosition.Y) * Singleton.worldScale, null, hp_color, 0f, new Vector2(1, 0f), new Vector2(hpSize.X * (this.hit_point / 100f), hpSize.Y) * Singleton.worldScale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(Singleton.Instance.ghb, hpPosition * Singleton.worldScale, null, hp_color, 0f, new Vector2(0, 0f), new Vector2(hpSize.X * (this.hit_point/100f), hpSize.Y) * Singleton.worldScale, SpriteEffects.None, 0f);
            }
        }
    }
}
