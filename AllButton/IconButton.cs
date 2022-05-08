using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllButton
{
    class IconButton : Button
    {
        public IconButton(Texture2D texture) : base(texture)
        {

        }

        public override void Update(List<Button> gameButton)
        {
            base.Update(gameButton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
