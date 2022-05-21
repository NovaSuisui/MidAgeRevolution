
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System;

using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Diagnostics;
using tainicom.Aether.Physics2D.Common;

using MidAgeRevolution.ScreenSystem;
using MidAgeRevolution.AllSprite;
using MidAgeRevolution.AllSprite.AllPlayer;
using MidAgeRevolution.AllButton;
using tainicom.Aether.Physics2D.Collision.Shapes;

namespace MidAgeRevolution.AllScreen
{
    class GameScreen : Screen
    {
        World world;
        DebugView debugView;
        BasicEffect batchEffect;
        private List<GameSprite> _gameObj;
        private Luck luck;
        private Wisdom wisdom;
        private bool loadedContent;
        public float wind = 0.0f;
        private Button _skill;
        private float timer;
        private bool enableDebug =true;

        public GameScreen(Main game, Texture2D texture) : base(game, texture)
        {
            _gameObj = new List<GameSprite>();
            world = new World(new Vector2(0.0f, 5f));
            world.ContactManager.VelocityConstraintsMultithreadThreshold = 256;
            world.ContactManager.PositionConstraintsMultithreadThreshold = 256;
            world.ContactManager.CollideMultithreadThreshold = 256;
            world.Tag = _gameObj;

            debugView = new DebugView(world);
            debugView.LoadContent(Singleton.Instance.GraphicsDevice, Singleton.Instance.Content);
            debugView.Flags = DebugViewFlags.None;
            debugView.Flags = (DebugViewFlags)2047;
            /*debugView.AppendFlags(DebugViewFlags.Shape);*/


            batchEffect = new BasicEffect(Singleton.Instance.GraphicsDevice);
            batchEffect.VertexColorEnabled = true;
            batchEffect.TextureEnabled = true;
            loadedContent = false;
        }
        public void setupPlayTurn()
        {
            foreach (GameSprite sprite in _gameObj) sprite.body.BodyType = BodyType.Kinematic;
            wisdom.body.BodyType = BodyType.Dynamic;
            luck.body.BodyType = BodyType.Dynamic;
        }

        public override void Update(Screen gameScreen, GameTime gameTime)
        {
            if(Singleton.Instance.PrevoiusKey.IsKeyDown(Keys.F2) && Singleton.Instance.CurrentKey.IsKeyUp(Keys.F2))
            {
                enableDebug = !enableDebug;
            }
            if (Singleton.Instance.PrevoiusKey.IsKeyDown(Keys.F1) && Singleton.Instance.CurrentKey.IsKeyUp(Keys.F1))
            {
                Singleton.Instance._nextGameState = Singleton.GameState.Setup;
                Singleton.Instance._gameResult = Singleton.GameResult.None;
                Singleton.Instance._mainState = Singleton.MainState.gamePlay;
            }

            int numSprite = _gameObj.Count;
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.Setup) loadedContent = false;
                    if (loadedContent == false)
                    {
                        world.Clear();
                        _gameObj.Clear();

                        Vertices borders = new Vertices(4);
                        borders.Add(new Vector2(0, 800*Singleton.worldScale));  // Lower left
                        borders.Add(new Vector2(1900 * Singleton.worldScale, 800 * Singleton.worldScale));   // Lower right
                        borders.Add(new Vector2(1900 * Singleton.worldScale, -1500 * Singleton.worldScale));  // Upper right
                        borders.Add(new Vector2(0, -1500 * Singleton.worldScale)); // Upper left
                        Body border = world.CreateLoopShape(borders);
                        border.SetCollidesWith(Category.All & ~Category.Cat2);
                        wisdom = new Wisdom(Singleton.Instance.sc, world)
                        {
                            // position = new Vector2(320, 600),
                            position = new Vector2(219, 359),
                        };
                        _gameObj.Add(wisdom);
                        luck = new Luck(Singleton.Instance.rl, world)
                        {
                            position = new Vector2(1236, 416),
                            turnLeft = true
                        };
                        _gameObj.Add(luck);

                        //create Tower
                        createLuckTower(GameSprite.Side.Luck);
                        createWisdomTower(GameSprite.Side.Wisdom);
                        //createTower(new Vector2(200f, 300f), GameSprite.Side.Wisdom);
                        //createTower(new Vector2(1000f, 300f), GameSprite.Side.Luck);

                        _skill = new Card(test)
                        {
                            position = new Vector2(200, 200),
                            field_size = new Vector2(60, 60)
                        };
                        _skill.Update(_skill);

                        loadedContent = true;
                    }
                    else
                    {
                        if (isWorldStop()) Singleton.Instance._nextGameState = Singleton.GameState.WisdomTurn;
                    }
                    break;

                case Singleton.GameState.WisdomTurn:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.WisdomTurn)
                    {
                        wind = Singleton.Instance.rnd.Next(-100, 100) / 10f;
                        Player.wind = this.wind;
                        Debug.WriteLine($"WisdomTurn wind:{wind}");

                        setupPlayTurn();
                    }
                    _skill.Update(_skill);
                    break;

                case Singleton.GameState.LuckTurn:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.LuckTurn)
                    {
                        wind = Singleton.Instance.rnd.Next(-100, 100) / 10f;
                        Player.wind = this.wind;
                        Debug.WriteLine($"LuckTurn wind:{wind}");

                        setupPlayTurn();
                    }
                    _skill.Update(_skill);
                    break;

                case Singleton.GameState.WisdomShooting:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.WisdomShooting)
                    {
                        Debug.WriteLine("WisdomShooting");
                        foreach (GameSprite sprite in _gameObj)
                        {
                            sprite.body.ResetDynamics();
                            sprite.body.BodyType = BodyType.Kinematic; 
                        }
                        wisdom.shoot(_gameObj);
                    } 
                    if (Singleton.Instance._prvGameState==Singleton.GameState.WisdomShooting)
                    {
                        if (isWorldStop()) Singleton.Instance._nextGameState = Singleton.GameState.WisdomEndTurn;
                    }
                    break;

                case Singleton.GameState.LuckShooting:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.LuckShooting)
                    {
                        Debug.WriteLine("LuckShooting");
                        foreach (GameSprite sprite in _gameObj)
                        {
                            sprite.body.ResetDynamics();
                            sprite.body.BodyType = BodyType.Kinematic;
                        }
                        luck.shoot(_gameObj);
                    }
                    if (Singleton.Instance._prvGameState == Singleton.GameState.LuckShooting)
                    {
                        if (isWorldStop()) Singleton.Instance._nextGameState = Singleton.GameState.LuckEndTurn;
                    }
                    break;

                case Singleton.GameState.WisdomEndTurn:
                        Singleton.Instance._nextGameState = Singleton.GameState.LuckTurn;
                    break;

                case Singleton.GameState.LuckEndTurn:
                    Singleton.Instance._nextGameState = Singleton.GameState.WisdomTurn;
                    break;
            }

            for (int i = 0; i < numSprite; i++)
            {
                if (_gameObj[i].isActive)
                {
                    _gameObj[i].Update(_gameObj, Singleton.Instance._time);
                }
            }

            for (int i = 0; i < numSprite; i++)
            {
                if (!_gameObj[i].isActive)
                {
                    _gameObj[i].Remove(world);
                    _gameObj.RemoveAt(i);
                    i--;
                    numSprite--;
                }
            }

            if (!wisdom.isAlive)
            {
                label = "Luck Win";
                Singleton.Instance._nextGameState = Singleton.GameState.End;
                Singleton.Instance._gameResult = Singleton.GameResult.LuckWin;
                Singleton.Instance._mainState = Singleton.MainState.gameEnd;
            }
            else if (!luck.isAlive)
            {
                label = "Wisdom Win";
                Singleton.Instance._nextGameState = Singleton.GameState.End;
                Singleton.Instance._gameResult = Singleton.GameResult.WisdomWin;
                Singleton.Instance._mainState = Singleton.MainState.gameEnd;
            }

            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            base.Update(gameScreen);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance._gameState)
            {
                /*case Singleton.GameState.Setup:
                    break;*/
                default:
                    /*foreach (GameSprite obj in _gameObj)
                    {
                        obj.Draw(spriteBatch);
                    }*/

                    spriteBatch.Draw(Singleton.Instance.bg, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    if (_skill != null) _skill.Draw(spriteBatch);
                    spriteBatch.End();

                    batchEffect.View = Camera2D.GetView();
                    batchEffect.Projection = Camera2D.GetProjection();
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, RasterizerState.CullNone, batchEffect);
                    for (int i = _gameObj.Count - 1; i >= 0; i--)
                    {
                        _gameObj[i].Draw(spriteBatch);
                    }
                    spriteBatch.End();
                    if(enableDebug)
                        debugView.RenderDebugData(Camera2D.GetProjection(), Camera2D.GetView());

                    spriteBatch.Begin();
                    spriteBatch.Draw(Singleton.Instance.screenBorder, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(Singleton.Instance.ghb, new Vector2(0,800), null, Color.Green, 0, Vector2.Zero, new Vector2(Singleton.WINDOWS_SIZE_X,100), SpriteEffects.None, 0f);
                    break;
            }

            base.Draw(spriteBatch);
        }

        public bool isWorldStop()
        {
            bool allBodyStop = true;
            var bodylist = world.BodyList;
            foreach (var body in bodylist)
            {
                if (body.LinearVelocity != Vector2.Zero)
                {
                    allBodyStop = false;
                    break;
                }
            }
            return allBodyStop;
        }

        private void createTower(Vector2 position, GameSprite.Side objectside)
        {
            float gap = 5f;
            float height = 98f;
            float width = 30f;

            // horizontal wisdom
            for (int i = 1; i < 3; i++)
            {
                _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v, world)
                {
                    position = new Vector2(position.X + height / 2 + (height * i), position.Y + ((height + width + gap * 2) * 0)),
                    hitbox_size = new Vector2(width, height),
                    side = objectside,
                    rotation = Singleton.Degree2Radian(90)
                });
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v, world)
                    {
                        position = new Vector2(position.X + height / 2 + (height * i), position.Y + ((height + width + gap * 2) * j)),
                        hitbox_size = new Vector2(width, height),
                        side = objectside,
                        rotation = Singleton.Degree2Radian(90)
                    });
                }
            }
            //vertical wisdom
            for (int i = 1; i < 4; i++)
            {
                _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v, world)
                {
                    position = new Vector2(position.X + (height * i), position.Y + (width + height + 10) / 2 + ((height + width + gap * 2) * 0)),
                    hitbox_size = new Vector2(width, height),
                    side = objectside
                });
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v, world)
                    {
                        position = new Vector2(position.X + (height * i), position.Y + (width + height + 10) / 2 + ((height + width + gap * 2) * j)),
                        hitbox_size = new Vector2(width, height),
                        side = objectside
                    });
                }
            }

        }
        private void createLuckTower(GameSprite.Side objectside)
        {
            Vertices triangle = new Vertices(3);
            Body body;
            float width, height, x, y;

            //first row

            width = 63.1f;
            height = 96.22f;
            x = 1074.86f;
            y = 398.0f;
            /*float width2 = width / 2 * Singleton.worldScale;
            float height2 = (height) / 2 * Singleton.worldScale;
            triangle.Add(new Vector2(-width2,height2));
            triangle.Add(new Vector2(width2, height2));
            triangle.Add(new Vector2(0, -height2+20*Singleton.worldScale));
            body = world.CreateBody();
            body.CreatePolygon(triangle, 1f);
            triangle.Clear();*/
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_1, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_1, new Vector2(x, y)),
                side = objectside
            });

            width = 68.45f;
            height = 96.22f;
            x = 1305.72f;
            y = 401.87f;
            /*width2 = width / 2 * Singleton.worldScale;
            height2 = (height) / 2 * Singleton.worldScale;
            triangle.Add(new Vector2(-width2, height2));
            triangle.Add(new Vector2(width2, height2));
            triangle.Add(new Vector2(0, -height2 + 20 * Singleton.worldScale));
            body = world.CreateBody();
            body.CreatePolygon(triangle, 1f);
            triangle.Clear();*/
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_1, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_2, new Vector2(x, y)),
                side = objectside
            });
            //secibd row
            x = 1071.96f;
            y = 494.22f;
            width = 74.58f;
            height = 68.13f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_3, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_3, new Vector2(x, y)),
                side = objectside
            });

            width = 15.82f;
            height = 22.92f;
            x = 1216.93f;
            y = 464.84f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_4_1, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_4_1, new Vector2(x, y)),
                side = objectside
            });

            width = 164.35f;
            height = 87.82f;
            x = 1142.67f;
            y = 487.76f;
            /*width2 = width / 2 * Singleton.worldScale;
            height2 = (height) / 2 * Singleton.worldScale;
            triangle.Add(new Vector2(-width2, height2));
            triangle.Add(new Vector2(width2, height2));
            triangle.Add(new Vector2(0, -height2));
            body = world.CreateBody();
            body.CreatePolygon(triangle, 1f);
            triangle.Clear();*/
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_4_2, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_4_2, new Vector2(x, y)),
                side = objectside
            });
            width = 71.68f;
            height = 69.42f;
            x = 1305.0f;
            y = 493.0f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_5, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_5, new Vector2(x, y)),
                side = objectside
            });

            //third row

            width = 69.1f;
            height = 111.39f;
            x = 1074.86f;
            y = 562.34f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_6, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_6, new Vector2(x, y)),
                side = objectside
            });

            width = 164.02f;
            height = 41.33f;
            x = 1142.67f;
            y = 573.0f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_7, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_7, new Vector2(x, y)),
                side = objectside
            });

            width = 73.94f;
            height = 107.84f;
            x = 1305.07f;
            y = 562.34f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_8, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_8, new Vector2(x, y)),
                side = objectside
            });

            width = 24.86f;
            height = 191.47f;
            x = 1050.0f;
            y = 596.89f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_9, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_9, new Vector2(x, y)),
                side = objectside
            });

            width = 49.69f;
            height = 60.06f;
            x = 1143.31f;
            y = 614.33f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_10, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_10, new Vector2(x, y)),
                side = objectside
            });

            width = 65.0f;
            height = 60.06f;
            x = 1193.0f;
            y = 614.33f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_11, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_11, new Vector2(x, y)),
                side = objectside
            });

            width = 47.72f;
            height = 60.06f;
            x = 1258.0f;
            y = 614.33f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_12, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_12, new Vector2(x, y)),
                side = objectside
            });

            //fourth row

            width = 68.45f;
            height = 115.59f;
            x = 1074.86f;
            y = 673.74f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_13, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_13, new Vector2(x, y)),
                side = objectside
            });

            width = 49.69f;
            height = 114.94f;
            x = 1143.31f;
            y = 674.38f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_14, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_14, new Vector2(x, y)),
                side = objectside
            });

            width = 65.0f;
            height = 114.94f;
            x = 1193.0f;
            y = 674.38f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_15, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_15, new Vector2(x, y)),
                side = objectside
            });

            width = 46.34f;
            height = 114.94f;
            x = 1258.0f;
            y = 674.38f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_16, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_16, new Vector2(x, y)),
                side = objectside
            });

            width = 74.58f;
            height = 119.14f;
            x = 1304.43f;
            y = 669.54f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_17, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_17, new Vector2(x, y)),
                side = objectside
            });

            width = 23.25f;
            height = 191.47f;
            x = 1367.75f;
            y = 596.25f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.lt_18, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.lt_18, new Vector2(x, y)),
                side = objectside
            });
        }

        private void createWisdomTower(GameSprite.Side objectside)
        {
            Body body;
            float width, height, x, y;

            //first row
            width = 70.4f;
            height = 86.11f;
            x = 176.52f;
            y = 446.62f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_1, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_1, new Vector2(x, y)),
                side = objectside
            });

            width = 75.16f;
            height = 86.11f;
            x = 246.92f;
            y = 446.62f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_2, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_2, new Vector2(x, y)),
                side = objectside
            });

            //second row
            width = 51.84f;
            height = 48.89f;
            x = 166.92f;
            y = 532.38f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_3, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_3, new Vector2(x, y)),
                side = objectside
            });

            width = 51.2f;
            height = 48.89f;
            x = 218.76f;
            y = 532.38f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_4, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_4, new Vector2(x, y)),
                side = objectside
            });

            width = 51.96f;
            height = 48.89f;
            x = 269.96f;
            y = 532.38f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_5, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_5, new Vector2(x, y)),
                side = objectside
            });

            //third row
            width = 78.08f;
            height = 48.33f;
            x = 167.56f;
            y = 581.02f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_6, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_6, new Vector2(x, y)),
                side = objectside
            });

            width = 76.92f;
            height = 48.33f;
            x = 245.64f;
            y = 581.02f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_7, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_7, new Vector2(x, y)),
                side = objectside
            });

            //fourth row
            width = 52.48f;
            height = 49.44f;
            x = 166.92f;
            y = 629.02f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_8, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_8, new Vector2(x, y)),
                side = objectside
            });

            width = 51.2f;
            height = 49.44f;
            x = 219.4f;
            y = 629.02f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_9, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_9, new Vector2(x, y)),
                side = objectside
            });

            width = 52.43f;
            height = 49.44f;
            x = 270.6f;
            y = 629.02f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_10, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_10, new Vector2(x, y)),
                side = objectside
            });

            //fifth row
            width = 81.92f;
            height = 48.89f;
            x = 165f;
            y = 678.3f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_11, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_11, new Vector2(x, y)),
                side = objectside
            });

            width = 77.52f;
            height = 48.89f;
            x = 249.62f;
            y = 678.3f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_12, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_12, new Vector2(x, y)),
                side = objectside
            });

            //sixth row
            width = 40.32f;
            height = 81.67f;
            x = 165.0f;
            y = 726.94f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_13, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_13, new Vector2(x, y)),
                side = objectside
            });

            width = 80.0f;
            height = 81.67f;
            x = 205.32f;
            y = 726.94f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_14, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_14, new Vector2(x, y)),
                side = objectside
            });

            width = 39.68f;
            height = 81.67f;
            x = 285.32f;
            y = 726.94f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_15, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_15, new Vector2(x, y)),
                side = objectside
            });

            //flag
            width = 30.56f;
            height = 39.44f;
            x = 218.76f;
            y = 407.58f;
            body = world.CreateRectangle(width * Singleton.worldScale, height * Singleton.worldScale, 1.0f, bodyType: BodyType.Dynamic);
            _gameObj.Add(new Obstacle(Singleton.Instance.wt_16, body)
            {
                position = Singleton.Instance.TopLeft(Singleton.Instance.wt_16, new Vector2(x, y)),
                side = objectside
            });
        }
    }
}
