using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    [System.Serializable]
    public class GameData
    {
        public int coins;
        public int crystals;
        public int diamond;

        public int highScore;

        public int level;

        public int randomQuest;
        public bool[] questsCompleted = new bool[3];
        public int[] itemCollected = new int[3];
        public int[] itemNeeded = new int[3];
        public string[] questText = new string[87];
        public string[] questTag = new string[87];

        public float durationX2Score;
        public float durationX2Coin;
        public float durationShield;
        public float durationMegaJump;

        public int levelX2Score;
        public int levelX2Coin;
        public int levelShield;
        public int levelMegaJump;

        public bool[] skinBuy = new bool[5];
        public int skinSelect;

        public GameData()
        {
            level = 1;

            InitQuest();
            InitBonus();
            InitSkins();
        }

        private void InitQuest()
        {
            randomQuest = 0;

            questsCompleted[0] = false;
            itemCollected[0] = 0;
            questsCompleted[1] = false;
            itemCollected[1] = 0;
            questsCompleted[2] = false;
            itemCollected[2] = 0;
        }

        private void InitBonus()
        {
            levelX2Score = 1;
            durationX2Score = 10;

            levelX2Coin = 1;
            durationX2Coin = 10;

            levelShield = 1;
            durationShield = 10;

            levelMegaJump = 1;
            durationMegaJump = 10;
        }

        private void InitSkins()
        {
            //Скины
            skinSelect = 0;

            for (int i = 0; i < skinBuy.Length; i++)
            {
                if (i == 0)
                {
                    skinBuy[i] = true;
                }
                else
                {
                    skinBuy[i] = false;
                }
            }
        }
    }
}
