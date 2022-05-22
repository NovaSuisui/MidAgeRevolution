using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace MidAgeRevolution.AllSprite
{
    class Obstacle : GameSprite
    {
        public float MaxHP = 300f;
        public float hit_point = 300f;
        public float prv_hitpoint = 300f;
        public float damageTimer = 0.0f;
        public float hp_timer = 0.0f;
        public Obstacle(Texture2D texture,World world) : base(texture)
        {
            colour = Color.White;

            float height = 98f;
            float width = 30f;

            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1,bodyType: BodyType.Dynamic);
            body.FixedRotation = true;
            body.OnCollision += collisionHandler;
            body.Tag = this;
        }

        public Obstacle(Texture2D texture, Body body) : base(texture)
        {
            colour = Color.White;

            this.body = body;
            body.FixedRotation = true;
            body.OnCollision += collisionHandler;
            body.Tag = this;
        }

        public IDictionary<Singleton.StatusEffect, uint> statusEffect = new Dictionary<Singleton.StatusEffect, uint>()
        {
            { Singleton.StatusEffect.fire, 1},
        };
        public void ApplyDamage(float Damage)
        {
            damageTimer = 0.5f;
            hp_timer = 0.7f;
            hit_point = hit_point - Damage;
            hit_point = Math.Clamp(hit_point, 0, MaxHP);
        }

        public void ApplyStatus(Singleton.StatusEffect statusEffect)
        {
            if (statusEffect == Singleton.StatusEffect.fire) this.statusEffect[Singleton.StatusEffect.fire] = 5;
        }

        bool collisionHandler(Fixture sender, Fixture other, Contact contact)
        {
            return true;
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            if (damageTimer > 0)
            {
                float et = (float)gameTime.ElapsedGameTime.TotalSeconds;
                damageTimer -= et;
                damageTimer = Math.Clamp(damageTimer, 0, 0.5f);
                float dv = prv_hitpoint - hit_point;
                if (et < damageTimer)
                    prv_hitpoint -= dv * (et / damageTimer);
                else prv_hitpoint = hit_point;
            }
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    if(Singleton.Instance._prvGameState != Singleton.GameState.WisdomTurn)
                    {
                        body.ResetDynamics();
                        body.BodyType = BodyType.Kinematic;
                        body.FixedRotation = true;
                    }
                    break;
                case Singleton.GameState.LuckTurn:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.LuckTurn)
                    {
                        body.ResetDynamics();
                        body.BodyType = BodyType.Kinematic;
                        body.FixedRotation = true;
                    }
                    break;
                case Singleton.GameState.WisdomShooting:
                    break;
                case Singleton.GameState.LuckShooting:
                    break;
                case Singleton.GameState.WisdomEndTurn:
                    if (statusEffect[Singleton.StatusEffect.fire] > 0 && side == Side.Wisdom)
                    {
                        ApplyDamage(10f);
                        statusEffect[Singleton.StatusEffect.fire]--;
                    }
                    if (hit_point/MaxHP < 0.33)
                    {
                        colour = Color.Red;
                    }
                    else if (hit_point / MaxHP < 0.66)
                    {
                        colour = Color.Orange;
                    }
                    else
                    {
                        colour = Color.White;
                    }
                    break;
                case Singleton.GameState.LuckEndTurn:
                    if (statusEffect[Singleton.StatusEffect.fire] > 0 && side == Side.Luck)
                    {
                        ApplyDamage(10f);
                        statusEffect[Singleton.StatusEffect.fire]--;
                    }
                    if (hit_point / MaxHP < 0.33)
                    {
                        colour = Color.Red;
                    }
                    else if (hit_point / MaxHP < 0.66)
                    {
                        colour = Color.Orange;
                    }
                    else
                    {
                        colour = Color.White;
                    }
                    break;
            }

            if(hit_point<=0)
            {
                isActive = false;
            }

            base.Update(gameObject, gameTime);
        }

        Vector2 fire_texel = Vector2.One / new Vector2(Singleton.Instance.fire.Width, Singleton.Instance.fire.Height);
        Vector2 fire_scale = new Vector2(1, 0.7f);
        Vector2 fire_origin = new Vector2(Singleton.Instance.fire.Width / 2, Singleton.Instance.fire.Height);

        Vector2 poision_texel = Vector2.One / new Vector2(Singleton.Instance.poision.Width, Singleton.Instance.poision.Height);
        Vector2 poision_scale = new Vector2(1, 1f);
        Vector2 poision_origin = new Vector2(Singleton.Instance.poision.Width / 2, Singleton.Instance.poision.Height);
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (statusEffect[Singleton.StatusEffect.fire] > 0)
            {
                if (side == Side.Wisdom)
                spriteBatch.Draw(Singleton.Instance.fire, (position + new Vector2(0,_texture.Height/2))*Singleton.worldScale, null, Color.White, 0f, fire_origin, (fire_texel * fire_scale * new Vector2(_texture.Width,_texture.Height)) * Singleton.worldScale, SpriteEffects.None, 0f);
                else
                spriteBatch.Draw(Singleton.Instance.poision, (position + new Vector2(0, _texture.Height / 2)) * Singleton.worldScale, null, Color.White, 0f, poision_origin, (poision_texel * poision_scale * Math.Min(_texture.Width, _texture.Height)) * Singleton.worldScale, SpriteEffects.None, 0f);
            }
            else colour = Color.White;
            /*if (hp_timer > 0)
            {
                DrawHP(spriteBatch, Singleton.Instance.ghb, position, new Vector2(30 , 4), position, false);
                hp_timer -= (float)Singleton.Instance._time.ElapsedGameTime.TotalSeconds;
            }*/
        }

        public void DrawHP(SpriteBatch spriteBatch, Texture2D border, Vector2 borderPosition, Vector2 hpSize, Vector2 hpPosition, bool decreedToRight = false)
        {
            spriteBatch.Draw(border, borderPosition * Singleton.worldScale, null, Color.White, 0f, Vector2.Zero, 1f * Singleton.worldScale, SpriteEffects.None, 0f);
            if (decreedToRight)
            {
                spriteBatch.Draw(Singleton.Instance.ghb, new Vector2(hpPosition.X + hpSize.X, hpPosition.Y) * Singleton.worldScale, null, Color.Green, 0f, new Vector2(1, 0f), new Vector2(hpSize.X * (prv_hitpoint / MaxHP), hpSize.Y) * Singleton.worldScale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(Singleton.Instance.ghb, hpPosition * Singleton.worldScale, null, Color.Green, 0f, new Vector2(0, 0f), new Vector2(hpSize.X * (prv_hitpoint / MaxHP), hpSize.Y) * Singleton.worldScale, SpriteEffects.None, 0f);
            }
        }
    }
}
