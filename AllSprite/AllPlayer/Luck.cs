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
                    if (Singleton.Instance._prvGameState != Singleton.GameState.LuckTurn)
                    {
                        foreach (GameSprite spite in gameObject) spite.body.BodyType = BodyType.Kinematic;
                        body.BodyType = BodyType.Dynamic;
                    }
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
            
            DrawHP(spriteBatch, Singleton.Instance.rl_hp_bar, new Vector2(997, 35), new Vector2(324, 31), new Vector2(1028, 57),true);
            base.Draw(spriteBatch);
        }
    }
}
