using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllScreen
{
    class GameScreen : Screen
    {
        public GameScreen(Texture2D texture) : base(texture)
        {

        }

        public override void Update(Screen gameScreen)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.None:


                    break;

                case Singleton.GameState.P1Turn:


                    break;

                case Singleton.GameState.P2Turn:


                    break;

                case Singleton.GameState.Shooting:


                    break;

                case Singleton.GameState.Shooting2:


                    break;
            }

            base.Update(gameScreen);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:


                    break;

                case Singleton.GameState.P1Turn:


                    break;

                case Singleton.GameState.P2Turn:


                    break;


                case Singleton.GameState.Shooting:


                    break;

                case Singleton.GameState.Shooting2:


                    break;
            }

            base.Draw(spriteBatch);
        }
    }
}
