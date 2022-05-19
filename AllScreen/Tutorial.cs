using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MidAgeRevolution.AllButton;

namespace MidAgeRevolution.AllScreen
{
    class Tutorial : Screen
    {
        private Texture2D test;
        private Texture2D _bg, _title, detail, re_tex2d;
        private Button _return;
        public Tutorial(Main game, Texture2D texture) : base(game, texture)
        {
            test = texture;
            //components
            _bg = (Texture2D)Singleton.Instance.menu_bg;
            _title = (Texture2D)Singleton.Instance.tu_title;
            detail = (Texture2D)Singleton.Instance.tu_detail;
            re_tex2d = (Texture2D)Singleton.Instance.re_btn;

            _return = new ScreenButton(re_tex2d)
            {
                Position = new Vector2(1210, 50)
            };

            _return.onClick += _returnClick;
        }
        private void _returnClick(object sender, EventArgs e)
        {
            Singleton.Instance._mainState = Singleton.MainState.mainMenu;
        }
        public override void Update(Screen gameScreen, GameTime gameTime)
        {

            _return.Update(gameTime);
            base.Update(gameScreen);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_bg, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(detail, new Vector2(72, 215), Color.White);
            spriteBatch.Draw(_title, new Vector2(512, 0), Color.White);
            _return.Draw(spriteBatch);
        }
    }
}
