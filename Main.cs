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
            _spriteFont = this.Content.Load<SpriteFont>("Test/font0");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Singleton.Instance.CurrentMouse = Mouse.GetState();
            Singleton.Instance.CurrentKey = Keyboard.GetState();
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
                    _screen[1].Update(_screen[1]);

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
