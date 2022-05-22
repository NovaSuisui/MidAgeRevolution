using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
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
        public float SKILL_SIZE = 60;

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

        public GameResult _gameResult;

        public enum AmmoType
        {
            /*
                16 bit controll Ammotype
                3 3 2-1 3 2 2 
             */
            // 3 bit (0-2) for damage 7 option
            MultiplyDamage = 0b111,
            x1dmg =   0,  // 000
            x2dmg   =   1,  // 001
            x3dmg   =   2,  // 010
            xrndAdmg   =   3,  //011
            xrndBdmg =   4,  //100
            x1p5dmg = 5,

            // 2 bit (3-4) for debuf 7 option
            DebufAmmo = 0b11000,
            none_debuf = 0,     // 00
            fire_debuf = 8,     // 01
            week_debuf = 16,    // 10
            random_debuf = 24,  // 11

            // 2 bit (5-6) for Bullet Behavior
            Behavior = 0b1100000,
            nomalBullet = 0,        // 00
            bounceBullet = 32,       // 01
            explosionBullet = 64,    // 10
            boostBullet = 96,      // 11

            // 1 bit (7) if bullet apply physics to the word
            applyPhysics = 128,
            
            // 1 bit (8) if no wind
            turnOffWind = 256,


            // 3 bit (9-11) for extra ammo
            Shooting = 0b111000000000,
            x1ammo = 0,         // 000
            x3ammo = 512,        // 001
            xrndammo = 1024,        // 010      

            // 1 bit (12) for special Bullet

            // 2 bit (14-15) for Controller
            Controller  = 0b1100000000000000,
            projectile  = 0b0000000000000000,
            laser       = 0b0100000000000000,
            power       = 0b1000000000000000,
            useItem     = 0b1100000000000000,

            // itemlist
            itemNumber  = 0b0011111111111111,
            otk = 1,
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

        //rework 

        //luck tower
        public Texture2D lt_1;
        public Texture2D lt_2;
        public Texture2D lt_3;
        public Texture2D lt_4_1;
        public Texture2D lt_4_2;
        public Texture2D lt_5;
        public Texture2D lt_6;
        public Texture2D lt_7;
        public Texture2D lt_8;
        public Texture2D lt_9;
        public Texture2D lt_10;
        public Texture2D lt_11;
        public Texture2D lt_12;
        public Texture2D lt_13;
        public Texture2D lt_14;
        public Texture2D lt_15;
        public Texture2D lt_16;
        public Texture2D lt_17;
        public Texture2D lt_18;

        //wisdom tower
        public Texture2D wt_1;
        public Texture2D wt_2;
        public Texture2D wt_3;
        public Texture2D wt_4;
        public Texture2D wt_5;
        public Texture2D wt_6;
        public Texture2D wt_7;
        public Texture2D wt_8;
        public Texture2D wt_9;
        public Texture2D wt_10;
        public Texture2D wt_11;
        public Texture2D wt_12;
        public Texture2D wt_13;
        public Texture2D wt_14;
        public Texture2D wt_15;
        public Texture2D wt_16;

        //charge bar 
        public Texture2D bg_cb;
        public Texture2D rb;
        public Texture2D me_b;

        //wind texture
        public Texture2D wind_border;
        public Texture2D w_l_g;
        public Texture2D w_l_y;
        public Texture2D w_l_o;
        public Texture2D w_l_r;
        public Texture2D w_r_g;
        public Texture2D w_r_y;
        public Texture2D w_r_o;
        public Texture2D w_r_r;
        public Texture2D w_0;

        // card 
        public Texture2D cd_bounce;
        public Texture2D cd_explosion;
        public Texture2D cd_pluck;
        public Texture2D cd_split;
        public Texture2D cd_x2;
        public Texture2D cd_x3;
        public Texture2D cd_wdult;
        public Texture2D cd_card;

        //sound effect
        public SoundEffectInstance mm_song, ps_song, click;

        //debuf
        public Texture2D fire;
        public Texture2D poision;

        //bullet
        public Texture2D flask;
        public Texture2D bible;

        public Texture2D[] WisdomSkill;

        public Vector2 TopLeft(Texture2D texture, Vector2 position)
        {
            return position + new Vector2(texture.Width / 2, texture.Height / 2);
        }
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
