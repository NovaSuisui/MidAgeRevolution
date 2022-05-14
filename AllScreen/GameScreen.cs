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
                        // position = new Vector2(320, 600),
                        position = new Vector2(1296, 328),
                        hitbox_size = new Vector2(50, 81.44f),
                        side = GameSprite.Side_ID.Wisdom_player
                    });
                    _gameObj.Add(new Luck(Singleton.Instance.rl)
                    {
                        // position = new Vector2(1220, 600),
                        position = new Vector2(304, 321),
                        hitbox_size = new Vector2(50, 87.74f),
                        side = GameSprite.Side_ID.Luck_player
                    });
                    _gameObj.Add(new Bullet(test)
                    {
                        //TO DO
                    });
                    // horizontal
                    for (int i = 0; i < 2; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_h)
                        {
                            position = new Vector2(279 + (98 * i), 409),
                            hitbox_size = new Vector2(98, 30),
                            side = GameSprite.Side_ID.Wisdom_obstacle
                        });
                    }
                        
                    //vertical
                    for (int i = 0; i < 3; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v)
                        {
                            position = new Vector2(200 + (120 * i), 720),
                            hitbox_size = new Vector2(30, 98),
                            side = GameSprite.Side_ID.Wisdom_obstacle
                        });
                    }
                    // horizontal
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_h)
                    {
                        position = new Vector2(200, 840),
                        hitbox_size = new Vector2(98, 30),
                        side = GameSprite.Side_ID.Wisdom_obstacle
                    });
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_h)
                    {
                        position = new Vector2(1100, 660),
                        hitbox_size = new Vector2(98, 30),
                        side = GameSprite.Side_ID.Luck_obstacle
                    });
                    for (int i = 0; i < 3; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v)
                        {
                            position = new Vector2(1100 + (120 * i), 720),
                            hitbox_size = new Vector2(30, 98),
                            side = GameSprite.Side_ID.Luck_obstacle
                        });
                    }
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_h)
                    {
                        position = new Vector2(1100, 840),
                        hitbox_size = new Vector2(98, 30),
                        side = GameSprite.Side_ID.Luck_obstacle
                    });


                    Singleton.Instance._gameState = Singleton.GameState.WisdomTurn;
                    break;

                case Singleton.GameState.WisdomTurn:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }

                    break;

                case Singleton.GameState.LuckTurn:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }

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
                    //spriteBatch.Draw(Singleton.Instance.screenBorder, new Vector2(-177, -7), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                    break;

                default:
                    /*foreach (GameSprite obj in _gameObj)
                    {
                        obj.Draw(spriteBatch);
                    }*/
                    spriteBatch.Draw(Singleton.Instance.bg, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    for (int i = _gameObj.Count - 1; i >= 0; i--)
                    {
                        _gameObj[i].Draw(spriteBatch);
                    }

                    spriteBatch.Draw(Singleton.Instance.screenBorder, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    break;
            }

            base.Draw(spriteBatch);
        }
    }
}
