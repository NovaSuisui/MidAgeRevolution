using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Diagnostics;
using System.Collections.Generic;

using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Common;

using MidAgeRevolution.AllSprite;
using MidAgeRevolution.AllSprite.AllPlayer;
using MidAgeRevolution.AllButton;


namespace MidAgeRevolution.AllScreen
{
    class GameScreen : Screen
    {

        World world;
        DebugView debugView;
        Camera2D camera;
        BasicEffect batchEffect;
        private List<GameSprite> _gameObj;
        private Button _skill;


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
                        position = new Vector2(304, 321),
                        hitbox_size = new Vector2(50, 81.44f),
                        side = GameSprite.Side_ID.Wisdom_player
                    });
                    _gameObj.Add(new Luck(Singleton.Instance.rl)
                    {
                        position = new Vector2(1296, 320),
                        hitbox_size = new Vector2(50, 87.74f),
                        side = GameSprite.Side_ID.Luck_player
                    });
                    _gameObj.Add(new Bullet(test)
                    {
                        //TO DO
                    });

                    _skill = new Card(test)
                    {
                        position = new Vector2(200, 200),
                        field_size = new Vector2(60, 60)
                    };
                    _skill.Update(_skill);

                    // horizontal wisdom
                    for (int i = 0; i < 2; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_h)
                        {
                            position = new Vector2(255 + (98 * i), 409),
                            hitbox_size = new Vector2(98, 30),
                            side = GameSprite.Side_ID.Wisdom_obstacle
                        });
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_h)
                            {
                                position = new Vector2(157 + (98 * i), 537 + (128 * j)),
                                hitbox_size = new Vector2(98, 30),
                                side = GameSprite.Side_ID.Wisdom_obstacle
                            });
                        }
                    }
                    //vertical wisdom
                    for (int i = 0; i < 3; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v)
                        {
                            position = new Vector2(240 + (98 * i), 439),
                            hitbox_size = new Vector2(30, 98),
                            side = GameSprite.Side_ID.Wisdom_obstacle
                        });
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v)
                            {
                                position = new Vector2(142 + (98 * i), 567 + (128 * j)),
                                hitbox_size = new Vector2(30, 98),
                                side = GameSprite.Side_ID.Wisdom_obstacle
                            });
                        }
                    }

                    // horizontal luck
                    for (int i = 0; i < 2; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_h)
                        {
                            position = new Vector2(1188 + (98 * i), 409),
                            hitbox_size = new Vector2(98, 30),
                            side = GameSprite.Side_ID.Luck_obstacle
                        });
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_h)
                            {
                                position = new Vector2(1090 + (98 * i), 537 + (128 * j)),
                                hitbox_size = new Vector2(98, 30),
                                side = GameSprite.Side_ID.Luck_obstacle
                            });
                        }
                    }
                    //vertical luck
                    for (int i = 0; i < 3; i++)
                    {
                        _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v)
                        {
                            position = new Vector2(1173 + (98 * i), 439),
                            hitbox_size = new Vector2(30, 98),
                            side = GameSprite.Side_ID.Luck_obstacle
                        });
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v)
                            {
                                position = new Vector2(1075 + (98 * i), 567 + (128 * j)),
                                hitbox_size = new Vector2(30, 98),
                                side = GameSprite.Side_ID.Luck_obstacle
                            });
                        }
                    }


                    Singleton.Instance._gameState = Singleton.GameState.WisdomTurn;
                    break;

                case Singleton.GameState.WisdomTurn:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }
                    _skill.Update(_skill);

                    break;

                case Singleton.GameState.LuckTurn:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }
                    _skill.Update(_skill);

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
                    _skill.Update(_skill);
                    Singleton.Instance._gameState = Singleton.GameState.LuckTurn;

                    break;

                case Singleton.GameState.LuckEndTurn:
                    foreach (GameSprite obj in _gameObj)
                    {
                        obj.Update(_gameObj, Singleton.Instance._time);
                    }
                    _skill.Update(_skill);

                    Singleton.Instance._gameState = Singleton.GameState.WisdomTurn;
                    break;
            }

            if (!_gameObj[0].isActive)
            {
                label = "Luck Win";
                Singleton.Instance._mainState = Singleton.MainState.gameEnd;
            }
            else if(!_gameObj[1].isActive)
            {
                label = "Wisdom Win";
                Singleton.Instance._mainState = Singleton.MainState.gameEnd;
            }

            for (int i = 2; i < _gameObj.Count; i++)
            {
                if (!_gameObj[i].isActive)
                {
                    _gameObj.RemoveAt(i);
                }
            }

            base.Update(gameScreen);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    spriteBatch.Draw(Singleton.Instance.bg, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Singleton.Instance.screenBorder, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);

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
                    _skill.Draw(spriteBatch);

                    spriteBatch.Draw(Singleton.Instance.screenBorder, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    break;
            }

            base.Draw(spriteBatch);
        }
    }
}
