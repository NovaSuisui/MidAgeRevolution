using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MidAgeRevolution
{
    class Singleton
    {

        public const int WINDOWS_SIZE_X = 1600;
        public const int WINDOWS_SIZE_Y = 800;

        public KeyboardState CurrentKey;
        public KeyboardState PrevoiusKey;
        public MouseState CurrentMouse;
        public MouseState PreviousMouse;

        public Dictionary<int, int> GameConfig;

        public enum GameStare
        {
            None,
            P1Turn,
            P2Turn,
            Shooting,
            Shooting2
        }

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
