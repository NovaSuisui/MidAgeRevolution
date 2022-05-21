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
            LuckEndTurn,
            End,
        }

        public GameState _gameState /*= GameState.Setup*/;
        public GameState _prvGameState;
        public GameState _nextGameState;

        public enum GameResult
        {
            None,
            LuckWin,
            WisdomWin
        }

        public GameResult gameResult;

        public enum AmmoType
        {
            /*
                16 bit controll Ammotype
                3 3 2-1 3 2 2 
             */
            // 3 bit (0-2) for damage 7 option
            MultiplyDamage = 0b111,
            x1dmg =   0,  // 000
            x0dmg   =   1,  // 001
            x0p3dmg =   2,  // 010
            x0p5dmg =   3,  // 011
            x0p6dmg =   4,  // 100
            x1p5dmg =   5,  // 101
            x2dmg   =   6,  // 110
            x3dmg   =   7,  // 111

            // 3 bit (3-5) for debuf 7 option
            DebufAmmo = 0b111000,
            none_debuf = 0,     // 000
            fire_debuf = 8,     // 001
            week_debuf = 16,    // 010
            random_debuf = 24,  // 011

            // 2 bit (6-7) for Bullet Behavior
            Behavior = 0b11000000,
            nomalBullet = 0,        // 00
            bounceBullet = 64,       // 01
            explosionBullet = 128,    // 10

            // 1 bit (8) if bullet apply physics to the word
            applyPhysics = 256, 


            // 3 bit (9-11) for extra ammo
            Shooting = 0b111000000000,
            x1ammo = 0,         // 000
            x3ammo = 512,        // 001
            x2time = 1048,        // 010      

            otk = 0b1100000000000001
        }
        public AmmoType ammo = AmmoType.x1dmg;
        public SpriteFont testfont;

        public enum StatusEffect
        {
            fire,
            weak
        }

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

        //win texture2d pop up
        public Texture2D rl_win;
        public Texture2D sc_win;
        public Texture2D mm_b; //back to mainmenu button
        public Texture2D re_b; //restart game button
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
