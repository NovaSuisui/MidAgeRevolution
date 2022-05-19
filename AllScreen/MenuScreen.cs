using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllScreen
{
    class MenuScreen : Screen
    {
        private Texture2D test;

        public MenuScreen(Texture2D texture) : base(texture)
        {
            test = texture;
        }

        public override void Update(Screen gameScreen)
        {
            base.Update(gameScreen);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);
        }
    }
}
