using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MidAgeRevolution
{
    class Singleton
    {

        public const int WINDOWS_SIZE_X = 1600;
        public const int WINDOWS_SIZE_Y = 900;

        public const float worldScale = 0.04f;
        public const float screenScale = 25.0f;

        public KeyboardState CurrentKey;
        public KeyboardState PrevoiusKey;
        public MouseState CurrentMouse;
        public MouseState PreviousMouse;

        public Dictionary<int, int> GameConfig;

        public GameTime _time;

        public float TEXTURE_SIZE = 60;

        public GraphicsDevice GraphicsDevice;
        public ContentManager Content;

        public Random rnd = new Random();
        public enum MainState
        {
            start,
            mainMenu,
            tutorial,
            gamePlay,
            gameEnd
        }
        public MainState _mainState = MainState.start;

        public enum GameState
        {
            Setup,
            WisdomTurn,
            LuckTurn,
            WisdomShooting,
            LuckShooting,
            WisdomEndTurn,
            LuckEndTurn
        }

        public GameState _gameState /*= GameState.Setup*/;
        public GameState _prvGameState;
        public GameState _nextGameState;

        public enum AmmoType
        {
            normal,
            x0dmg,
            x0p5dmg,
            x1p5dmg,
            x2dmg,
            x3dmg,
            otk
        }
        public AmmoType ammo = AmmoType.normal;
        public SpriteFont testfont;

        //texture2d
        public Texture2D sc;
        public Texture2D rl;
        public Texture2D screenBorder;
        public Texture2D rl_hp_bar;
        public Texture2D sc_hp_bar;
        public Texture2D bg;
        public Texture2D sc_tw_02_v;
        public Texture2D sc_tw_02_h;
        public Texture2D ghb;
        public Texture2D Arrow;

        //menu texture2d
        public Texture2D menu_bg;
        public Texture2D title;
        public Texture2D st_btn;
        public Texture2D tu_btn;
        public Texture2D ex_btn;
        public Texture2D ghibi;
        public Texture2D re_btn;
        public Texture2D tu_title;
        public Texture2D tu_detail;

        public static float Degree2Radian(float degrees) { return (float)(degrees * (Math.PI / 180)); }
        //singleton
        private static Singleton instance;

        private Singleton()
        {
        }
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
}
