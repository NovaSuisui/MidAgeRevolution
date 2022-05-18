using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using MidAgeRevolution.AllButton;
using MidAgeRevolution.AllScreen;
using MidAgeRevolution.AllSprite;
using MidAgeRevolution.AllSprite.AllPlayer;

namespace MidAgeRevolution
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        private List<Screen> _screen;
            
        Texture2D test;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = Singleton.WINDOWS_SIZE_X;
            _graphics.PreferredBackBufferHeight = Singleton.WINDOWS_SIZE_Y;

            _graphics.ApplyChanges();


            _screen = new List<Screen>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            test = this.Content.Load<Texture2D>("Test/test0");
            Singleton.Instance.sc = this.Content.Load<Texture2D>("Asset/science");
            Singleton.Instance.rl = this.Content.Load<Texture2D>("Asset/religious");
            Singleton.Instance.sc_hp_bar = this.Content.Load<Texture2D>("Asset/sc_health_bar");
            Singleton.Instance.rl_hp_bar = this.Content.Load<Texture2D>("Asset/rl_health_bar");
            Singleton.Instance.screenBorder = this.Content.Load<Texture2D>("Asset/play_screen_border");
            Singleton.Instance.bg = this.Content.Load<Texture2D>("Asset/background");
            Singleton.Instance.sc_tw_02_v = this.Content.Load<Texture2D>("Asset/sc_tw_02_vertical");
            Singleton.Instance.sc_tw_02_h = this.Content.Load<Texture2D>("Asset/sc_tw_02_horizontal");
            Singleton.Instance.Arrow = this.Content.Load<Texture2D>("Test/Arrow");
            
            Singleton.Instance.ghb = new Texture2D(GraphicsDevice, 1, 1);
            Singleton.Instance.ghb.SetData(new[] { Color.White });

            _spriteFont = this.Content.Load<SpriteFont>("Test/font0");

            Singleton.Instance.Content = Content;
            Singleton.Instance.GraphicsDevice = _graphics.GraphicsDevice;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            Singleton.Instance.PrevoiusKey = Singleton.Instance.CurrentKey;
            Singleton.Instance.CurrentKey = Keyboard.GetState();

            Singleton.Instance._prvGameState = Singleton.Instance._GameState;
            Singleton.Instance._GameState = Singleton.Instance._nextGameState;

            Singleton.Instance._time = gameTime;

            // TODO: Add your update logic here
            switch (Singleton.Instance._mainState)
            {
                case Singleton.MainState.start:
                    _screen.Add(new MenuScreen(test));
                    _screen.Add(new GameScreen(test));

                    Singleton.Instance._mainState = Singleton.MainState.mainMenu;
                    break;

                case Singleton.MainState.mainMenu:
                    _screen[0].Update(_screen[0]);

                    Singleton.Instance._mainState = Singleton.MainState.gamePlay;
                    break;

                case Singleton.MainState.tutorial:

                    Singleton.Instance._mainState = Singleton.MainState.mainMenu;
                    break;

                case Singleton.MainState.gamePlay:
                    _screen[1].Update(_screen[1], gameTime);

                    //Singleton.Instance._mainState = Singleton.MainState.gameEnd;
                    break;

                case Singleton.MainState.gameEnd:
                    //_screen[1].Update(_screen[1]);

                    //Singleton.Instance._mainState = Singleton.MainState.mainMenu;
                    break;
            }

            Singleton.Instance.PreviousMouse = Singleton.Instance.CurrentMouse;
            Singleton.Instance.PrevoiusKey = Singleton.Instance.CurrentKey;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // TODO: Add your drawing code here
            switch (Singleton.Instance._mainState)
            {
                case Singleton.MainState.start:
                    _screen[0].Draw(_spriteBatch);

                    break;
                case Singleton.MainState.mainMenu:
                    _screen[0].Draw(_spriteBatch);

                    break;
                case Singleton.MainState.tutorial:
                    _screen[0].Draw(_spriteBatch);

                    break;
                case Singleton.MainState.gamePlay:
                    _screen[1].Draw(_spriteBatch);

                    break;
                case Singleton.MainState.gameEnd:
                    _screen[1].Draw(_spriteBatch);
                    //_spriteBatch.DrawString(_spriteFont, _screen[1].label, new Vector2(100, 100), Color.Black);
                    _spriteBatch.DrawString(_spriteFont, _screen[1].label, new Vector2(100, 100), Color.Black, 0, Vector2.Zero, new Vector2(5, 5), SpriteEffects.None, 0f);

                    break;
                default:
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
