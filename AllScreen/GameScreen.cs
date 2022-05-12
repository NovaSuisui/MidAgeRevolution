using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using MidAgeRevolution.AllSprite;
using MidAgeRevolution.AllSprite.AllPlayer;

namespace MidAgeRevolution.AllScreen
{
    class GameScreen : Screen
    {
        private List<GameSprite> _gameObj;

        public GameScreen(Texture2D texture) : base(texture)
        {
            _gameObj = new List<GameSprite>();
        }

        public override void Update(Screen gameScreen)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    _gameObj.Clear();
                    _gameObj.Add(new Wisdom(test)
                    {
                        position = new Vector2(0, 0)
                    });
                    _gameObj.Add(new Luck(test)
                    {
                        position = new Vector2(1540, 840)
                    });
                    //_gameObj.Add(new Bullet(test));
                    //_gameObj.Add(new Obstacle(test));

                    Singleton.Instance._gameState = Singleton.GameState.WisdomTurn;
                    break;

                case Singleton.GameState.WisdomTurn:
                    _gameObj[0].Update(_gameObj, Singleton.Instance._time);

                    if(Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        Singleton.Instance._gameState = Singleton.GameState.WisdomShooting;
                    }
                    break;

                case Singleton.GameState.LuckTurn:
                    _gameObj[1].Update(_gameObj, Singleton.Instance._time);


                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        Singleton.Instance._gameState = Singleton.GameState.LuckShooting;
                    }
                    break;

                case Singleton.GameState.WisdomShooting:
                    foreach(GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }

                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        Singleton.Instance._gameState = Singleton.GameState.LuckTurn;
                    }
                    break;

                case Singleton.GameState.LuckShooting:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }

                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        Singleton.Instance._gameState = Singleton.GameState.WisdomTurn;
                    }
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

                default:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Draw(spriteBatch);
                    }
                    break;
            }

            base.Draw(spriteBatch);
        }
    }
}
