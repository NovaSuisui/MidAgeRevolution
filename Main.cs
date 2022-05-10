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
            _screen.Add(new MenuScreen(test));
            _screen.Add(new GameScreen(test));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (Singleton.Instance._mainState)
            {
                case Singleton.MainState.start:

                    Singleton.Instance._mainState = Singleton.MainState.mainMenu;
                    break;
                case Singleton.MainState.mainMenu:
                    break;
                case Singleton.MainState.tutorial:
                    break;
                case Singleton.MainState.gamePlay:
                    break;
                case Singleton.MainState.gameEnd:
                    break;
            }






            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
