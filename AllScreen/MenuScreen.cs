using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MidAgeRevolution.AllButton;
using MidAgeRevolution;

namespace MidAgeRevolution.AllScreen
{
    class MenuScreen : Screen
    {
        private Texture2D test;
        private Texture2D _bg, _ghibi, _title, st_tex2d, tu_tex2d, ex_tex2d;
        private Button _start, _tutorial, _exit;
        public MenuScreen(Main game, Texture2D texture) : base(game, texture)
        {
            test = texture;
            //components
            _bg = (Texture2D)Singleton.Instance.menu_bg;
            _ghibi = (Texture2D)Singleton.Instance.ghibi;
            _title = (Texture2D)Singleton.Instance.title;

            st_tex2d = (Texture2D)Singleton.Instance.st_btn;
            tu_tex2d = (Texture2D)Singleton.Instance.tu_btn;
            ex_tex2d = (Texture2D)Singleton.Instance.ex_btn;

            _start = new MenuButton(st_tex2d)
            {
                Position = new Vector2(623, 472)
            };

            _start.onClick += _startClick;

            _tutorial = new MenuButton(tu_tex2d)
            {
                Position = new Vector2(623, 611)
            };

            _tutorial.onClick += _tutorialClick;

            _exit = new MenuButton(ex_tex2d)
            {
                Position = new Vector2(623, 750)
            };

            _exit.onClick += _exitClick;
        }
        private void _startClick(object sender, EventArgs e)
        {
            // Load into the game screen
            Singleton.Instance._mainState = Singleton.MainState.gamePlay;
        }
        private void _tutorialClick(object sender, EventArgs e)
        {
            // Load into the game screen
            Singleton.Instance._mainState = Singleton.MainState.tutorial;
        }
        private void _exitClick(object sender, EventArgs e)
        {
            // Load into the game screen
            _game.Exit();
        }
        public override void Update(Screen gameScreen, GameTime gameTime)
        {
            
            _start.Update(gameTime);
            _tutorial.Update(gameTime);
            _exit.Update(gameTime);
            base.Update(gameScreen);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_bg, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(_ghibi, new Vector2(27, 696), Color.White);
            spriteBatch.Draw(_title, new Vector2(422, 146), Color.White);
            _start.Draw(spriteBatch);
            _tutorial.Draw(spriteBatch);
            _exit.Draw(spriteBatch);
        }
    }
}
