using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MidAgeRevolution.AllSprite.AllPlayer
{
    class Item
    {
        public static void otk(List<GameSprite> gameObject)
        {
            Wisdom w = gameObject[0] as Wisdom;
            Luck l = gameObject[1] as Luck;
            int change = Singleton.Instance.rnd.Next(0, 100);
            if (change < 30)
            {
                return;
            }
            else
            {
                change = Singleton.Instance.rnd.Next(0, 100);
                if (change < 5) l.ApplyDamage(l.hit_point);
                else w.ApplyDamage(w.hit_point);
            }
        }
    }
}
