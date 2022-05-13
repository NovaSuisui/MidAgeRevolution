using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution
{
    class Singleton
    {

        public const int WINDOWS_SIZE_X = 1600;
        public const int WINDOWS_SIZE_Y = 900;

        public KeyboardState CurrentKey;
        public KeyboardState PrevoiusKey;
        public MouseState CurrentMouse;
        public MouseState PreviousMouse;

        public Dictionary<int, int> GameConfig;

        public GameTime _time;

        public float TEXTURE_SIZE = 60;

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
            LuckShooting
        }
        public GameState _gameState = GameState.Setup;

        public enum Status
        {

        }

        public enum AmmoType
        {

        }


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
