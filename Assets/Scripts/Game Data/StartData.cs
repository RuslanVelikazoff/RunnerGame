using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartData : MonoBehaviour
{
    private ShopManager shopManager;
    private const string saveKey = "mainSave";

    public void Initialize()
    {
        shopManager = FindObjectOfType<ShopManager>();
        Load();
    }

    private void OnApplicationQuit()
    {
        SaveDB();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveDB();
        }
    }

    private void OnDisable()
    {
        SaveDB();
    }

    private void Load()
    {
        var data = SaveManager.Load<SaveData.GameData>(saveKey);

        shopManager.coins = data.coins;
        shopManager.crystals = data.crystals;
        shopManager.diamonds = data.diamond;

        shopManager.highScore = data.highScore;
        shopManager.level = data.level;

        shopManager.randomQuest = data.randomQuest;
        shopManager.questComleted = data.questsCompleted;
        shopManager.itemCollected = data.itemCollected;
        shopManager.itemNeeded = data.itemNeeded;
        shopManager.questText = data.questText;
        shopManager.questTag = data.questTag;

        shopManager.durationX2Score = data.durationX2Score;
        shopManager.durationX2Coin = data.durationX2Coin;
        shopManager.durationShield = data.durationShield;
        shopManager.durationMegaJump = data.durationMegaJump;

        shopManager.levelX2Score = data.levelX2Score;
        shopManager.levelX2Coin = data.levelX2Coin;
        shopManager.levelShield = data.levelShield;
        shopManager.levelMegaJump = data.levelMegaJump;

        shopManager.skinBuy = data.skinBuy;
        shopManager.skinSelect = data.skinSelect;
    }

    private void SaveDB()
    {
        SaveManager.Save(saveKey, GetSaveSnapshot());
        PlayerPrefs.Save();
    }

    public SaveData.GameData GetSaveSnapshot()
    {
        var data = new SaveData.GameData()
        {
            coins = shopManager.coins,
            crystals = shopManager.crystals,
            diamond = shopManager.diamonds,

            highScore = shopManager.highScore,
            level = shopManager.level,

            randomQuest = shopManager.randomQuest,
            questsCompleted = shopManager.questComleted,
            itemCollected = shopManager.itemCollected,
            itemNeeded = shopManager.itemNeeded,
            questText = shopManager.questText,
            questTag = shopManager.questTag,

            durationX2Score = shopManager.durationX2Score,
            durationX2Coin = shopManager.durationX2Coin,
            durationShield = shopManager.durationShield,
            durationMegaJump = shopManager.durationMegaJump,

            levelX2Score = shopManager.levelX2Score,
            levelX2Coin = shopManager.levelX2Coin,
            levelShield = shopManager.levelShield,
            levelMegaJump = shopManager.levelMegaJump,

            skinBuy = shopManager.skinBuy,
            skinSelect = shopManager.skinSelect
        };

        return data;
    }
}
