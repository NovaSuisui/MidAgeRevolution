using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllButton
{
    class IconButton : Button
    {
        private Texture2D _texture;
        private int[] wisdomSkillPool =
        {
            2,
            2,
            2,
            2,
            2,
            2,
            1
        };
        private int[] wisdomSkillCount;
        private Color[] skillColor =
        {
            Color.White,
            Color.White,
            Color.White,
            Color.White,
            Color.White,
            Color.White,
            Color.White
        };
        private int CurrentSkill;
        private int PreviousSkill;

        public IconButton(Texture2D texture) : base(texture)
        {
            _texture = texture;
            wisdomSkillCount = new int[7];
            CurrentSkill = -1;
            PreviousSkill = -1;
            /*for (int i = 0; i < wisdomSkillCount.Length; i++)
            {
                wisdomSkillCount[i] = wisdomSkillPool[i];
            }*/
        }

        public override void Update(Button gameButton)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    rect = new Rectangle((int)position.X, (int)position.Y, (int)field_size.X, (int)field_size.Y);
                    for (int i = 0; i < wisdomSkillCount.Length; i++)
                    {
                        wisdomSkillCount[i] = wisdomSkillPool[i];
                    }
                    Singleton.Instance.ammo = Singleton.AmmoType.x1dmg;

                    break;
                case Singleton.GameState.WisdomTurn:
                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                    Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released &&
                    Singleton.Instance.CurrentMouse.Y < position.Y + field_size.Y &&
                    Singleton.Instance.CurrentMouse.Y > position.Y)
                    {
                        if (Singleton.Instance.CurrentMouse.X < position.X + Singleton.Instance.SKILL_SIZE &&
                            Singleton.Instance.CurrentMouse.X > position.X)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.bounceBullet;
                            CurrentSkill = 0;
                            setAmmoWisdom();
                        }
                        else if (Singleton.Instance.CurrentMouse.X < position.X + Singleton.Instance.SKILL_SIZE * 2 &&
                            Singleton.Instance.CurrentMouse.X > position.X + Singleton.Instance.SKILL_SIZE)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.applyPhysics;
                            CurrentSkill = 1;
                            setAmmoWisdom();
                        }
                        else if (Singleton.Instance.CurrentMouse.X < position.X + Singleton.Instance.SKILL_SIZE * 3 &&
                            Singleton.Instance.CurrentMouse.X > position.X + Singleton.Instance.SKILL_SIZE * 2)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.x2dmg;
                            CurrentSkill = 2;
                            setAmmoWisdom();
                        }
                        else if (Singleton.Instance.CurrentMouse.X < position.X + Singleton.Instance.SKILL_SIZE * 4 &&
                            Singleton.Instance.CurrentMouse.X > position.X + Singleton.Instance.SKILL_SIZE * 3)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.x3dmg;
                            CurrentSkill = 3;
                            setAmmoWisdom();
                        }
                        else if (Singleton.Instance.CurrentMouse.X < position.X + Singleton.Instance.SKILL_SIZE * 5 &&
                            Singleton.Instance.CurrentMouse.X > position.X + Singleton.Instance.SKILL_SIZE * 4)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.x3ammo;
                            CurrentSkill = 4;
                            setAmmoWisdom();
                        }
                        else if (Singleton.Instance.CurrentMouse.X < position.X + Singleton.Instance.SKILL_SIZE * 6 &&
                            Singleton.Instance.CurrentMouse.X > position.X + Singleton.Instance.SKILL_SIZE * 5)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.fire_debuf;
                            CurrentSkill = 5;
                            setAmmoWisdom();
                        }
                        else if (Singleton.Instance.CurrentMouse.X < position.X + Singleton.Instance.SKILL_SIZE * 7 &&
                            Singleton.Instance.CurrentMouse.X > position.X + Singleton.Instance.SKILL_SIZE * 6)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.boostBullet | Singleton.AmmoType.x3ammo | Singleton.AmmoType.fire_debuf ;
                            CurrentSkill = 6;
                            setAmmoWisdom();
                        }
                    }

                    break;
                case Singleton.GameState.LuckTurn:
                    break;
                case Singleton.GameState.WisdomShooting:
                    break;
                case Singleton.GameState.LuckShooting:
                    break;
                case Singleton.GameState.WisdomEndTurn:
                    if (isOutOfSkill())
                    {
                        for (int i = 0; i < wisdomSkillCount.Length; i++)
                        {
                            wisdomSkillCount[i] = wisdomSkillPool[i];
                        }
                    }
                    for (int i = 0; i < wisdomSkillCount.Length; i++)
                    {
                        skillColor[i] = Color.White;
                    }
                    CurrentSkill = -1;
                    PreviousSkill = -1;
                    Singleton.Instance.ammo = Singleton.AmmoType.x1dmg;

                    break;
                case Singleton.GameState.LuckEndTurn:
                    break;
                case Singleton.GameState.End:
                    break;
            }

            base.Update(gameButton);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Singleton.Instance._gameState)
            {
                case Singleton.GameState.Setup:
                    break;
                case Singleton.GameState.WisdomTurn:
                    for (int i = 0; i < wisdomSkillCount.Length; i++)
                    {
                        //spriteBatch.Draw(_texture, new Vector2(position.X + 60 * i, position.Y), null, skillColor[i], 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        spriteBatch.Draw(Singleton.Instance.WisdomSkill[i], new Rectangle(rect.X + (i * rect.Width), rect.Y, rect.Width, rect.Height), skillColor[i]);
                        spriteBatch.DrawString(Singleton.Instance.testfont, "" + wisdomSkillCount[i], new Vector2(position.X + Singleton.Instance.SKILL_SIZE * i, position.Y), Color.Black, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
                    }

                    break;
                case Singleton.GameState.LuckTurn:
                    break;
                case Singleton.GameState.WisdomShooting:
                    break;
                case Singleton.GameState.LuckShooting:
                    break;
                case Singleton.GameState.WisdomEndTurn:
                    break;
                case Singleton.GameState.LuckEndTurn:
                    break;
                case Singleton.GameState.End:
                    break;
            }

            base.Draw(spriteBatch);
        }

        private bool isOutOfSkill()
        {
            int count = 0;
            for (int i = 0; i < wisdomSkillCount.Length; i++)
            {
                if (wisdomSkillCount[i] == 0)
                {
                    count++;
                }
            }

            if (count == wisdomSkillCount.Length)
            {
                return true;
            }
            return false;
        }

        private void setAmmoWisdom()
        {
            if (PreviousSkill == -1 &&
                wisdomSkillCount[CurrentSkill] > 0)
            {
                wisdomSkillCount[CurrentSkill]--;
                skillColor[CurrentSkill] = Color.LightBlue;
                PreviousSkill = CurrentSkill;
            }
            else if (CurrentSkill != PreviousSkill &&
                wisdomSkillCount[CurrentSkill] > 0)
            {
                wisdomSkillCount[CurrentSkill]--;
                wisdomSkillCount[PreviousSkill]++;
                skillColor[CurrentSkill] = Color.LightBlue;
                skillColor[PreviousSkill] = Color.White;
                PreviousSkill = CurrentSkill;
            }
            else if (CurrentSkill == PreviousSkill)
            {
                wisdomSkillCount[CurrentSkill]++;
                skillColor[CurrentSkill] = Color.White;
                PreviousSkill = -1;
                Singleton.Instance.ammo = Singleton.AmmoType.x1dmg;
            }
            else
            {
                Singleton.Instance.ammo = Singleton.AmmoType.x1dmg;
            }
        }
    }
}
