using Microsoft.Xna.Framework;
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
    class boostBullet : Bullet
    {
        uint count = 1;
        public boostBullet(Texture2D texture, Body body) : base(texture, body)
        {
            damage = 30;
        }

        public override void Update(List<GameSprite> gameObject, GameTime gameTime)
        {
            if(Singleton.Instance.PrevoiusKey.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && Singleton.Instance.CurrentKey.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                body.ResetDynamics();
                int x = Singleton.Instance.CurrentMouse.X;
                int y = Singleton.Instance.CurrentMouse.Y;
                Vector2 direction = Vector2.Normalize(new Vector2(x, y) - this.position);
                body.LinearVelocity = direction * 600f;
            }

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
    }
}
