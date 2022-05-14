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
                    _gameObj.Add(new Wisdom(Singleton.Instance.sc)
                    {
                        position = new Vector2(320, 600),
                        scale = new Vector2(50, 87.74),
                        side = GameSprite.Side_ID.Wisdom_player
                    });
                    _gameObj.Add(new Luck(Singleton.Instance.rl)
                    {
                        position = new Vector2(1220, 600),
                        side = GameSprite.Side_ID.Luck_player
                    });
                    _gameObj.Add(new Bullet(test)
                    {
                        //TO DO
                    });
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02)
                    {
                        position = new Vector2(200, 660),
                        scale = new Vector2(5, 1),
                        side = GameSprite.Side_ID.Wisdom_obstacle
                    });
                    for (int i = 0; i < 3; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02)
                        {
                            position = new Vector2(200 + (120 * i), 720),
                            scale = new Vector2(1, 2),
                            side = GameSprite.Side_ID.Wisdom_obstacle
                        });
                    }
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02)
                    {
                        position = new Vector2(200, 840),
                        scale = new Vector2(5, 1),
                        side = GameSprite.Side_ID.Wisdom_obstacle
                    });
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02)
                    {
                        position = new Vector2(1100, 660),
                        scale = new Vector2(5, 1),
                        side = GameSprite.Side_ID.Luck_obstacle
                    });
                    for (int i = 0; i < 3; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02)
                        {
                            position = new Vector2(1100 + (120 * i), 720),
                            scale = new Vector2(1, 2),
                            side = GameSprite.Side_ID.Luck_obstacle
                        });
                    }
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02)
                    {
                        position = new Vector2(1100, 840),
                        scale = new Vector2(5, 1),
                        side = GameSprite.Side_ID.Luck_obstacle
                    });


                    Singleton.Instance._gameState = Singleton.GameState.WisdomTurn;
                    break;

                case Singleton.GameState.WisdomTurn:
                    _gameObj[0].Update(_gameObj, Singleton.Instance._time);
                    _gameObj[2].Update(_gameObj, Singleton.Instance._time);

                    break;

                case Singleton.GameState.LuckTurn:
                    _gameObj[1].Update(_gameObj, Singleton.Instance._time);
                    _gameObj[2].Update(_gameObj, Singleton.Instance._time);

                    break;

                case Singleton.GameState.WisdomShooting:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }

                    break;

                case Singleton.GameState.LuckShooting:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }

                    break;

                case Singleton.GameState.WisdomEndTurn:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }

                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        if (_gameObj[1].isActive)
                        {
                            Singleton.Instance._gameState = Singleton.GameState.LuckTurn;
                        }
                        else
                        {
                            label = "Wisdom Win";
                            Singleton.Instance._mainState = Singleton.MainState.gameEnd;
                        }
                    }

                    for (int i = 2; i < _gameObj.Count; i++)
                    {
                        if (!_gameObj[i].isActive)
                        {
                            _gameObj.RemoveAt(i);
                        }
                    }
                    break;

                case Singleton.GameState.LuckEndTurn:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }

                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released)
                    {
                        if (_gameObj[0].isActive)
                        {
                            Singleton.Instance._gameState = Singleton.GameState.WisdomTurn;
                        }
                        else
                        {
                            label = "Luck Win";
                            Singleton.Instance._mainState = Singleton.MainState.gameEnd;
                        }
                    }

                    for (int i = 2; i < _gameObj.Count; i++)
                    {
                        if (!_gameObj[i].isActive)
                        {
                            _gameObj.RemoveAt(i);
                        }
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
