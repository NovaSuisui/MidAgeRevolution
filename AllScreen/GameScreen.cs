
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
        public float wind =0.0f;
        private Button _skill;
        private float timer;

        public GameScreen(Main game, Texture2D texture) : base(game, texture)
        {
            _gameObj = new List<GameSprite>();
            world = new World(new Vector2(0.0f,5f));
            world.ContactManager.VelocityConstraintsMultithreadThreshold = 256;
            world.ContactManager.PositionConstraintsMultithreadThreshold = 256;
            world.ContactManager.CollideMultithreadThreshold = 256;
            world.Tag = _gameObj;

            Vertices borders = new Vertices(4);
            borders.Add(new Vector2(0, 36));  // Lower left
            borders.Add(new Vector2(64, 36));   // Lower right
            borders.Add(new Vector2(64, -60));  // Upper right
            borders.Add(new Vector2(0, -60)); // Upper left
            Body border = world.CreateLoopShape(borders);
            border.SetCollidesWith(Category.All & ~Category.Cat2);
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

        public override void Update(Screen gameScreen,GameTime gameTime)
        {
            int numSprite = _gameObj.Count;
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.Setup) loadedContent = false;
                    if(loadedContent == false)
                    {
                        _gameObj.Clear();
                        wisdom = new Wisdom(Singleton.Instance.sc, world)
                        {
                            // position = new Vector2(320, 600),
                            position = new Vector2(450, 200),
                        };
                        _gameObj.Add(wisdom);
                        luck = new Luck(Singleton.Instance.rl, world)
                        {
                            position = new Vector2(1296, 220),
                            turnLeft = true
                        };
                        _gameObj.Add(luck);

                        createTower(new Vector2(200f, 300f), GameSprite.Side.Wisdom);
                        createTower(new Vector2(1000f, 300f), GameSprite.Side.Luck);

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
                        if(isWorldStop()) Singleton.Instance._nextGameState = Singleton.GameState.WisdomTurn;
                    }
                    break;

                case Singleton.GameState.WisdomTurn:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.WisdomTurn)
                    {
                        wind = Singleton.Instance.rnd.Next(-100,100) / 10f;
                        Player.wind = this.wind;
                        Debug.WriteLine($"WisdomTurn wind:{wind}");
                    }
                    _skill.Update(_skill);
                    break;

                case Singleton.GameState.LuckTurn:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.LuckTurn)
                    {
                        wind = Singleton.Instance.rnd.Next(-100, 100) / 10f;
                        Player.wind = this.wind;
                        Debug.WriteLine($"LuckTurn wind:{wind}");
                    }
                    _skill.Update(_skill);
                    break;

                case Singleton.GameState.WisdomShooting:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.WisdomShooting) Debug.WriteLine("WisdomShooting");
                    if (Singleton.Instance._prvGameState==Singleton.GameState.WisdomShooting)
                    {
                        if (isWorldStop()) Singleton.Instance._nextGameState = Singleton.GameState.WisdomEndTurn;
                    }
                    break;

                case Singleton.GameState.LuckShooting:
                    if (Singleton.Instance._prvGameState != Singleton.GameState.LuckShooting) Debug.WriteLine("LuckShooting");
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
                Singleton.Instance.gameResult = Singleton.GameResult.LuckWin;
                Singleton.Instance._mainState = Singleton.MainState.gameEnd;
            }
            else if (!luck.isAlive)
            {
                label = "Wisdom Win";
                Singleton.Instance._nextGameState = Singleton.GameState.End;
                Singleton.Instance.gameResult = Singleton.GameResult.WisdomWin;
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
                    if(_skill!=null) _skill.Draw(spriteBatch);
                    spriteBatch.End();

                    batchEffect.View = Camera2D.GetView();
                    batchEffect.Projection = Camera2D.GetProjection();
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, RasterizerState.CullNone, batchEffect);
                    for (int i = _gameObj.Count - 1; i >= 0; i--)
                    {
                        _gameObj[i].Draw(spriteBatch);
                    }
                    spriteBatch.End();

                    debugView.RenderDebugData(Camera2D.GetProjection(), Camera2D.GetView());

                    spriteBatch.Begin();
                    spriteBatch.Draw(Singleton.Instance.screenBorder, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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

        private void createTower(Vector2 position,GameSprite.Side objectside)
        {
            float gap = 5f;
            float height = 98f;
            float width = 30f;

            // horizontal wisdom
            for (int i = 1; i < 3; i++)
            {
                _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v,world)
                {
                    position = new Vector2(position.X + height / 2 +(height * i), position.Y + ((height + width + gap * 2) * 0)),
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
                        position = new Vector2(position.X + height/2  + (height * i), position.Y + ((height + width + gap * 2) * j)),
                        hitbox_size = new Vector2(width, height),
                        side = objectside,
                        rotation = Singleton.Degree2Radian(90)
                    });
                }
            }
            //vertical wisdom
            for (int i = 1; i < 4; i++)
            {
                _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v,world)
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
                    _gameObj.Add(new Obstacle(Singleton.Instance.sc_tw_02_v,world)
                    {
                        position = new Vector2(position.X + (height * i), position.Y + (width+height+10)/2 + ((height+width + gap*2) * j)),
                        hitbox_size = new Vector2(width, height),
                        side = objectside
                    });
                }
            }

        }
    }
}
