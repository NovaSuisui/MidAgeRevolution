using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MidAgeRevolution.AllButton
{
    class Card : Button
    {
        Texture2D test;
        private int[] wisdomSkillPool =
        {
            2,
            1
        };
        private int[] wisdomSkillCount;
        private Color[] wisdomColor =
        {
            Color.White,
            Color.White
        };
        private int CurrentSkill;
        private int PreviousSkill;

        public Card(Texture2D texture) : base(texture)
        {
            test = texture;
            wisdomSkillCount = new int[2];
            CurrentSkill = -1;
            PreviousSkill = -1;
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

                    break;
                case Singleton.GameState.WisdomTurn:
                    if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed &&
                        Singleton.Instance.PreviousMouse.LeftButton == ButtonState.Released &&
                        Singleton.Instance.CurrentMouse.Y < position.Y + field_size.Y &&
                        Singleton.Instance.CurrentMouse.Y > position.Y)
                    {
                        if (Singleton.Instance.CurrentMouse.X < position.X + 60 &&
                            Singleton.Instance.CurrentMouse.X > position.X)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.x2dmg;
                            CurrentSkill = 0;
                            setAmmo();
                        }
                        else if (Singleton.Instance.CurrentMouse.X < position.X + 120 && 
                            Singleton.Instance.CurrentMouse.X > position.X + 60)
                        {
                            Singleton.Instance.ammo = Singleton.AmmoType.x3dmg;
                            CurrentSkill = 1;
                            setAmmo();
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
                        wisdomColor[i] = Color.White;
                    }
                    CurrentSkill = -1;
                    PreviousSkill = -1;
                    Singleton.Instance.ammo = Singleton.AmmoType.normal;

                    break;
                case Singleton.GameState.LuckEndTurn:
                    //random for nextround

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
                    for (int i = 0; i < wisdomSkillCount.Length; i++) // for test
                    {
                        //spriteBatch.Draw(test, new Vector2(position.X + 60 * i, position.Y), null, wisdomColor[i], 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        //spriteBatch.Draw(test, rect, Color.White);
                        spriteBatch.Draw(test, new Rectangle(rect.X + (i * rect.Width), rect.Y, rect.Width, rect.Height), wisdomColor[i]);
                        spriteBatch.DrawString(Singleton.Instance.testfont, ""+wisdomSkillCount[i], new Vector2(position.X + 60 * i, 265), Color.Black, 0, Vector2.Zero, new Vector2(5, 5), SpriteEffects.None, 0f);
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

        private void setAmmo()
        {
            if (PreviousSkill == -1 &&
                            wisdomSkillCount[CurrentSkill] > 0)
            {
                wisdomSkillCount[CurrentSkill]--;
                wisdomColor[CurrentSkill] = Color.LightBlue;
                PreviousSkill = CurrentSkill;
            }
            else if (CurrentSkill != PreviousSkill &&
                wisdomSkillCount[CurrentSkill] > 0)
            {
                wisdomSkillCount[CurrentSkill]--;
                wisdomSkillCount[PreviousSkill]++;
                wisdomColor[CurrentSkill] = Color.LightBlue;
                wisdomColor[PreviousSkill] = Color.White;
                PreviousSkill = CurrentSkill;
            }
            else if (CurrentSkill == PreviousSkill)
            {
                wisdomSkillCount[CurrentSkill]++;
                wisdomColor[CurrentSkill] = Color.White;
                PreviousSkill = -1;
                Singleton.Instance.ammo = Singleton.AmmoType.normal;
            }
            else
            {
                Singleton.Instance.ammo = Singleton.AmmoType.normal;
            }
        }
    }
}
