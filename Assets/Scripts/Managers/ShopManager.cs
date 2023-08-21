using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int level;

    public int coins;

    public int crystals;

    public int diamonds;

    public int highScore;

    [Header("Магазин скинов")]
    [Header("Скины")]
    public bool[] skinBuy;
    public int[] skinPrices;
    public GameObject[] skins;
    public int skinSelect;
    private int selectedSkin;
    [Space(2)]
    [Header("UI элементы")]
    public Image selectBuyButton;
    public Text buttonText;

    [Space(7)]
    [Header("Магазин улучшений")]
    [Header("UI элементы")]
    public Text priceX2ScoreText;
    public Text priceX2CoinText;
    public Text priceShieldText;
    public Text priceMegaJumpText;
    [Space(2)]
    public Text levelX2ScoreText;
    public Text levelX2CoinText;
    public Text levelShieldText;
    public Text levelMegaJumpText;
    [Space(2)]
    public GameObject upgradeX2ScoreButton;
    public GameObject upgradeX2CoinButton;
    public GameObject upgradeShieldButton;
    public GameObject upgradeMegaJumpButton;
    [Space(4)]
    [Header("Уровень прокачки бонуса")]
    public int levelX2Score;
    public int levelX2Coin;
    public int levelShield;
    public int levelMegaJump;
    [Space(4)]
    [Header("Стоимость улучшений")]
    public int costX2Score;
    public int costX2Coin;
    public int costShield;
    public int costMegaJump;
    [Space(4)]
    [Header("Длительность бонусов")]
    public float durationX2Score;
    public float durationX2Coin;
    public float durationShield;
    public float durationMegaJump;

    [Space(7)]
    [Header("Панель квестов")]
    [SerializeField]
    private int costSkipQuest;
    [SerializeField]
    private Image[] completedImage;
    [SerializeField]
    private Text[] textQuest;
    [SerializeField]
    private Text[] statusText;
    [SerializeField]
    QuestBlank[] quest;
    [SerializeField]
    private GameObject[] skipQuestButton;

    public int randomQuest;

    public string[] questTag;

    public string[] questText;

    public bool[] questComleted;

    public int[] itemCollected;

    public int[] itemNeeded;

    #region SkinShop
    public void InitializeSkinShop()
    {
        selectedSkin = skinSelect;
        ButtonView();

        for (int i = 0; i < skins.Length; i++)
        {
            if (i == skinSelect)
            {
                skins[i].SetActive(true);
            }
            else
            {
                skins[i].SetActive(false);
            }
        }
    }

    public void RighButton()
    {
        skins[selectedSkin].SetActive(false);
        selectedSkin += 1;
        if (selectedSkin > skins.Length - 1)
        {
            selectedSkin = 0;
            skins[selectedSkin].SetActive(true);
        }
        else
        {
            skins[selectedSkin].SetActive(true);
        }
        ButtonView();
    }

    public void LeftButton()
    {
        skins[selectedSkin].SetActive(false);
        selectedSkin -= 1;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Length - 1;
            skins[selectedSkin].SetActive(true);
        }
        else
        {
            skins[selectedSkin].SetActive(true);
        }
        ButtonView();
    }

    //Функция для кнопки
    public void BuySelectButton()
    {
        if (skinBuy[selectedSkin])
        {
            SelectSkin();
        }
        else
        {
            BuySkin();
        }
        ButtonView();
    }

    //Выбор скина
    private void SelectSkin()
    {
        skinSelect = selectedSkin;
    }

    //Покупка скина
    private void BuySkin()
    {
        //TODO: добавить скины за квестовую валюту
        if (skinPrices[selectedSkin] <= coins)
        {
            coins -= skinPrices[selectedSkin];
            skinBuy[selectedSkin] = true;
            skinSelect = selectedSkin;
        }
        else
        {
            Debug.Log("Недостаточно средств");
        }
    }

    //Вид кнопки
    private void ButtonView()
    {
        if (skinBuy[selectedSkin])
        {
            if (selectedSkin == skinSelect)
            {
                buttonText.text = "Выбрано";
                buttonText.color = Color.green;
                selectBuyButton.color = Color.clear;
            }
            else
            {
                buttonText.text = "Выбрать";
                buttonText.color = Color.black;
                selectBuyButton.color = Color.green;
            }
        }
        else
        {
            buttonText.text = skinPrices[selectedSkin] + "";
            buttonText.color = Color.black;
            selectBuyButton.color = Color.red;
        }
    }

    //Закрытие магазина
    public void CloseShop()
    {
        if (selectedSkin == skinSelect)
        {
            skins[skinSelect].SetActive(true);
        }
        else
        {
            skins[selectedSkin].SetActive(false);
            skins[skinSelect].SetActive(true);
        }
    }

    //Открытие магазина
    public void OpenShop()
    {
        selectedSkin = skinSelect;
        ButtonView();
    }
    #endregion

    #region BonusShop
    public void InitializeBonusShop()
    {
        UpdateMegaJumpText();
        UpdateShieldText();
        UpdateX2CoinText();
        UpdateX2ScoreText();

        UpdateLevelText();
        UpdateButtons();
    }

    #region BuyUpgrade
    public void BuyUpgradeX2Score()
    {
        if (coins >= costX2Score)
        {
            if (levelX2Score < 6)
            {
                coins -= costX2Score;

                levelX2Score += 1;

                UpdateDurationX2Score();
                UpdateButtons();
                UpdateLevelText();
                UpdateX2ScoreText();
            }
        }
        else
        {
            Debug.Log("Недостаточно средств");
        }
    }

    public void BuyUpgradeX2Coin()
    {
        if (coins >= costX2Coin)
        {
            if (levelX2Coin < 6)
            {
                coins -= costX2Coin;

                levelX2Coin += 1;

                UpdateDurationX2Coin();
                UpdateButtons();
                UpdateLevelText();
                UpdateX2CoinText();
            }
        }
        else
        {
            Debug.Log("Недостаточно средств");
        }
    }

    public void BuyUpgradeShield()
    {
        if (coins >= costShield)
        {
            if (levelShield < 6)
            {
                coins -= costShield;

                levelShield += 1;

                UpdateDurationShield();
                UpdateButtons();
                UpdateLevelText();
                UpdateShieldText();
            }
        }
        else
        {
            Debug.Log("Недостаточно средств");
        }
    }

    public void BuyUpgradeMegaJump()
    {
        if (coins >= costMegaJump)
        {
            if (levelMegaJump < 6)
            {
                coins -= costMegaJump;

                levelMegaJump += 1;

                UpdateDurationMegaJump();
                UpdateButtons();
                UpdateLevelText();
                UpdateShieldText();
            }
        }
        else
        {
            Debug.Log("Недостаточно средств");
        }
    }
    #endregion

    #region UpdatePriceText
    private void UpdateX2ScoreText()
    {
        //X2Score
        switch (levelX2Score)
        {
            case 1:
                costX2Score = 1000;
                priceX2ScoreText.text = "" + costX2Score;
                return;

            case 2:
                costX2Score = 2000;
                priceX2ScoreText.text = "" + costX2Score;
                return;

            case 3:
                costX2Score = 3000;
                priceX2ScoreText.text = "" + costX2Score;
                return;

            case 4:
                costX2Score = 4000;
                priceX2ScoreText.text = "" + costX2Score;
                return;

            case 5:
                costX2Score = 5000;
                priceX2ScoreText.text = "" + costX2Score;
                return;
        }
    }

    private void UpdateX2CoinText()
    {
        //X2Coin
        switch (levelX2Coin)
        {
            case 1:
                costX2Coin = 1000;
                priceX2CoinText.text = "" + costX2Coin;
                return;

            case 2:
                costX2Coin = 2000;
                priceX2CoinText.text = "" + costX2Coin;
                return;

            case 3:
                costX2Coin = 3000;
                priceX2CoinText.text = "" + costX2Coin;
                return;

            case 4:
                costX2Coin = 4000;
                priceX2CoinText.text = "" + costX2Coin;
                return;

            case 5:
                costX2Coin = 5000;
                priceX2CoinText.text = "" + costX2Coin;
                return;
        }
    }

    private void UpdateShieldText()
    {
        //Shield
        switch (levelShield)
        {
            case 1:
                costShield = 1000;
                priceShieldText.text = "" + costShield;
                return;

            case 2:
                costShield = 2000;
                priceShieldText.text = "" + costShield;
                return;

            case 3:
                costShield = 3000;
                priceShieldText.text = "" + costShield;
                return;

            case 4:
                costShield = 4000;
                priceShieldText.text = "" + costShield;
                return;

            case 5:
                costShield = 5000;
                priceShieldText.text = "" + costShield;
                return;
        }
    }

    private void UpdateMegaJumpText()
    {
        //MegaJump
        switch (levelMegaJump)
        {
            case 1:
                costMegaJump = 1000;
                priceMegaJumpText.text = "" + costMegaJump;
                return;

            case 2:
                costMegaJump = 2000;
                priceMegaJumpText.text = "" + costMegaJump;
                return;

            case 3:
                costMegaJump = 3000;
                priceMegaJumpText.text = "" + costMegaJump;
                return;

            case 4:
                costMegaJump = 4000;
                priceMegaJumpText.text = "" + costMegaJump;
                return;

            case 5:
                costMegaJump = 5000;
                priceMegaJumpText.text = "" + costMegaJump;
                return;
        }
    }
    #endregion

    #region UpdateLevelUI
    private void UpdateLevelText()
    {
        levelX2ScoreText.text = levelX2Score + "/6";
        levelX2CoinText.text = levelX2Coin + "/6";
        levelShieldText.text = levelShield + "/6";
        levelMegaJumpText.text = levelMegaJump + "/6";
    }

    private void UpdateButtons()
    {
        if (levelX2Score == 6)
        {
            upgradeX2ScoreButton.SetActive(false);
        }
        if (levelX2Coin == 6)
        {
            upgradeX2CoinButton.SetActive(false);
        }
        if (levelShield == 6)
        {
            upgradeShieldButton.SetActive(false);
        }
        if (levelMegaJump == 6)
        {
            upgradeMegaJumpButton.SetActive(false);
        }
    }
    #endregion

    #region UpdateDuration
    private void UpdateDurationX2Score()
    {
        switch (levelX2Score)
        {
            case 1:
                durationX2Score = 10;
                return;

            case 2:
                durationX2Score = 12;
                return;

            case 3:
                durationX2Score = 14;
                return;

            case 4:
                durationX2Score = 16;
                return;

            case 5:
                durationX2Score = 18;
                return;

            case 6:
                durationX2Score = 20;
                return;
        }
    }

    private void UpdateDurationX2Coin()
    {
        switch (levelX2Coin)
        {
            case 1:
                durationX2Coin = 10;
                return;

            case 2:
                durationX2Coin = 12;
                return;

            case 3:
                durationX2Coin = 14;
                return;

            case 4:
                durationX2Coin = 16;
                return;

            case 5:
                durationX2Coin = 18;
                return;

            case 6:
                durationX2Coin = 20;
                return;
        }
    }

    private void UpdateDurationShield()
    {
        switch (levelShield)
        {
            case 1:
                durationShield = 10;
                return;

            case 2:
                durationShield = 12;
                return;

            case 3:
                durationShield = 14;
                return;

            case 4:
                durationShield = 16;
                return;

            case 5:
                durationShield = 18;
                return;

            case 6:
                durationShield = 20;
                return;
        }
    }

    private void UpdateDurationMegaJump()
    {
        switch (levelMegaJump)
        {
            case 1:
                durationMegaJump = 10;
                return;

            case 2:
                durationMegaJump = 12;
                return;

            case 3:
                durationMegaJump = 14;
                return;

            case 4:
                durationMegaJump = 16;
                return;

            case 5:
                durationMegaJump = 18;
                return;

            case 6:
                durationMegaJump = 20;
                return;
        }
    }
    #endregion

    #endregion

    #region QuestMenu
    public void InitializeQuestMenu()
    {
        if (level < 30)
        {
            SetQuestText();
            SetItemNeeded(level);
            SetQuest(level);
            SetColorQuest(level);
            SetButton();
        }
        else
        {
            SetQuestText();
            SetItemNeeded(randomQuest);
            SetQuest(randomQuest);
            SetColorQuest(randomQuest);
            SetButton();
        }
    }

    private void SetQuestText()
    {
        //Текст заданий
        //Уровень 1
        questText[0] = "Соберите 400 монет";
        questTag[0] = BonusTag.Coin;
        questText[1] = "Наберите 5000 очков";
        questTag[1] = BonusTag.Score;
        questText[2] = "Соберите 4 бонуса щита";
        questTag[2] = BonusTag.Shield;

        //Уровень 2
        questText[3] = "Соберите 4 бонуса мега прыжок";
        questTag[3] = BonusTag.MegaJump;
        questText[4] = "Соберите 10 бонусов двойные монеты";
        questTag[4] = BonusTag.DoubleCoin;
        questText[5] = "Соберите 800 монет";
        questTag[5] = BonusTag.Coin;

        //Уровень 3
        questText[6] = "Прыгните 20 раз";
        questTag[6] = BonusTag.Jump;
        questText[7] = "Наберите 10000 очков";
        questTag[7] = BonusTag.Score;
        questText[8] = "Соберите 8 бонусов мега прыжок";
        questTag[8] = BonusTag.MegaJump;

        //Уровень 4
        questText[9] = "Соберите 10 бонусов двойной опыт";
        questTag[9] = BonusTag.DoubleScore;
        questText[10] = "Наберите 20000 очков";
        questTag[10] = BonusTag.Score;
        questText[11] = "Пригнитесь 15 раз";
        questTag[11] = BonusTag.Slide;

        //Уровень 5
        questText[12] = "Прыгните 17 раз";
        questTag[12] = BonusTag.Jump;
        questText[13] = "Пригнитесь 3 раз";
        questTag[13] = BonusTag.Slide;
        questText[14] = "Соберите 3 бонуса щита";
        questTag[14] = BonusTag.Shield;

        //Уровень 6
        questText[15] = "Соберите 1000 монет";
        questTag[15] = BonusTag.Coin;
        questText[16] = "Наберите 25000 очков";
        questTag[16] = BonusTag.Score;
        questText[17] = "Соберите 20 бонусов двойные монеты";
        questTag[17] = BonusTag.DoubleCoin;

        //Уровень 7
        questText[18] = "Соберите 20 бонусов двойной опыт";
        questTag[18] = BonusTag.DoubleScore;
        questText[19] = "Соберите 20 бонусов щита";
        questTag[19] = BonusTag.Shield;
        questText[20] = "Соберите 20 бонусов мега прыжок";
        questTag[20] = BonusTag.MegaJump;

        //Уровень 8
        questText[21] = "Прыгните 30 раз";
        questTag[21] = BonusTag.Jump;
        questText[22] = "Соберите 1500 монет";
        questTag[22] = BonusTag.Coin;
        questText[23] = "Пригнитесь 30 раз";
        questTag[23] = BonusTag.Shield;

        //Уровень 9
        questText[24] = "Наберите 35000 очков";
        questTag[24] = BonusTag.Score;
        questText[25] = "Соберите 25 бонусов двойной опыт";
        questTag[25] = BonusTag.DoubleScore;
        questText[26] = "Прыгните 35 раз";
        questTag[26] = BonusTag.Jump;

        //Уровень 10
        questText[27] = "Соберите 30 бонусов двойные монеты";
        questTag[27] = BonusTag.DoubleCoin;
        questText[28] = "Соберите 2000 монет";
        questTag[28] = BonusTag.Coin;
        questText[29] = "Соберите 30 бонусов щита";
        questTag[29] = BonusTag.Shield;

        //Уровень 11
        questText[30] = "Соберите 30 бонусов двойной опыт";
        questTag[30] = BonusTag.DoubleScore;
        questText[31] = "Наберите 40000 очков";
        questTag[31] = BonusTag.Score;
        questText[32] = "Пригнитесь 40 раз";
        questTag[32] = BonusTag.Slide;

        //Уровень 12
        questText[33] = "Соберите 30 бонусов мега прыжок";
        questTag[33] = BonusTag.MegaJump;
        questText[34] = "Соберите 30 бонусов двойные монеты";
        questTag[34] = BonusTag.DoubleCoin;
        questText[35] = "Соберите 2500 монет";
        questTag[35] = BonusTag.Coin;

        //Уровень 13
        questText[36] = "Соберите 2800 монет";
        questTag[36] = BonusTag.Coin;
        questText[37] = "Наберите 50000 очков";
        questTag[37] = BonusTag.Score;
        questText[38] = "Соберите 40 бонусов щита";
        questTag[38] = BonusTag.Shield;

        //Уровень 14
        questText[39] = "Соберите 40 бонусов двойной опыт";
        questTag[39] = BonusTag.DoubleScore;
        questText[40] = "Соберите 40 бонусов щита";
        questTag[40] = BonusTag.Shield;
        questText[41] = "Соберите 40 бонусов мега прыжок";
        questTag[41] = BonusTag.MegaJump;

        //Уровень 15
        questText[42] = "Прыгните 40 раз";
        questTag[42] = BonusTag.Jump;
        questText[43] = "Пригнитесь 40 раз";
        questTag[43] = BonusTag.Slide;
        questText[44] = "Соберите 40 бонусов щита";
        questTag[44] = BonusTag.Shield;

        //Уровень 16
        questText[45] = "Соберите 3000 монет";
        questTag[45] = BonusTag.Coin;
        questText[46] = "Наберите 55000 очков";
        questTag[46] = BonusTag.Score;
        questText[47] = "Соберите 40 бонусов двойные монеты";
        questTag[47] = BonusTag.DoubleScore;

        //Уровень 17
        questText[48] = "Соберите 45 бонусов двойной опыт";
        questTag[48] = BonusTag.DoubleScore;
        questText[49] = "Соберите 45 бонусов щита";
        questTag[49] = BonusTag.Shield;
        questText[50] = "Соберите 45 бонусов мега прыжок";
        questTag[50] = BonusTag.MegaJump;

        //Уровень 18
        questText[51] = "Прыгните 50 раз";
        questTag[51] = BonusTag.Jump;
        questText[52] = "Соберите 3500 монет";
        questTag[52] = BonusTag.Coin;
        questText[53] = "Пригнитесь 50 раз";
        questTag[53] = BonusTag.Slide;

        //Уровень 19
        questText[54] = "Наберите 60000 очков";
        questTag[54] = BonusTag.Score;
        questText[55] = "Соберите 50 бонусов двойной опыт";
        questTag[55] = BonusTag.DoubleScore;
        questText[56] = "Прыгните 55 раз";
        questTag[56] = BonusTag.Jump;

        //Уровень 20
        questText[57] = "Соберите 60 бонусов двойные монеты";
        questTag[57] = BonusTag.DoubleCoin;
        questText[58] = "Соберите 3700 монет";
        questTag[58] = BonusTag.Coin;
        questText[59] = "Соберите 60 бонусов щита";
        questTag[59] = BonusTag.Shield;

        //Уровень 21
        questText[60] = "Соберите 60 бонусов двойной опыт";
        questTag[60] = BonusTag.DoubleScore;
        questText[61] = "Наберите 70000 очков";
        questTag[61] = BonusTag.Score;
        questText[62] = "Пригнитесь 60 раз";
        questTag[62] = BonusTag.Slide;

        //Уровень 22
        questText[63] = "Соберите 60 бонусов мега прыжок";
        questTag[63] = BonusTag.MegaJump;
        questText[64] = "Соберите 60 бонусов двойные монеты";
        questTag[64] = BonusTag.DoubleCoin;
        questText[65] = "Соберите 4000 монет";
        questTag[65] = BonusTag.Coin;

        //Уровень 23
        questText[66] = "Соберите 60 бонусов двойной опыт";
        questTag[66] = BonusTag.DoubleScore;
        questText[67] = "Наберите 75000 очков";
        questTag[67] = BonusTag.Score;
        questText[68] = "Пригнитесь 60 раз";
        questTag[68] = BonusTag.Slide;

        //Уровень 24
        questText[69] = "Соберите 65 бонусов двойной опыт";
        questTag[69] = BonusTag.DoubleScore;
        questText[70] = "Соберите 65 бонусов щита";
        questTag[70] = BonusTag.Shield;
        questText[71] = "Соберите 65 бонусов мега прыжка";
        questTag[71] = BonusTag.MegaJump;

        //Уровень 25
        questText[72] = "Прыгните 65 раз";
        questTag[72] = BonusTag.Jump;
        questText[73] = "Пригнитесь 65 раз";
        questTag[73] = BonusTag.Slide;
        questText[74] = "Соберите 50 бонусов щита";
        questTag[74] = BonusTag.Shield;

        //Уровень 26
        questText[75] = "Соберите 4000 монет";
        questTag[75] = BonusTag.Coin;
        questText[76] = "Наберите 80000 очков";
        questTag[76] = BonusTag.Score;
        questText[77] = "Соберите 70 бонусов двойные монеты";
        questTag[77] = BonusTag.DoubleCoin;

        //Уровень 27
        questText[78] = "Соберите 80 бонусов двойной опыт";
        questTag[78] = BonusTag.DoubleScore;
        questText[79] = "Соберите 80 бонусов щита";
        questTag[79] = BonusTag.Shield;
        questText[80] = "Соберите 80 бонусов мега прыжок";
        questTag[80] = BonusTag.MegaJump;

        //Уровень 28
        questText[81] = "Прыгните 80 раз";
        questTag[81] = BonusTag.Jump;
        questText[82] = "Соберите 4500 монет";
        questTag[82] = BonusTag.Coin;
        questText[83] = "Пригнитесь 80 раз";
        questTag[83] = BonusTag.Slide;

        //Уровень 29
        questText[84] = "Наберите 300000 очков";
        questTag[84] = BonusTag.Score;
        questText[85] = "Соберите 100 бонусов двойныой опыт";
        questTag[85] = BonusTag.DoubleScore;
        questText[86] = "Соберите 10000 монет";
        questTag[86] = BonusTag.Coin;
    }

    private void SetItemNeeded(int level)
    {
        switch (level)
        {
            case 1:
                SetItemsNeeded(400, 5000, 4);
                return;
            case 2:
                SetItemsNeeded(4, 10, 800);
                return;
            case 3:
                SetItemsNeeded(20, 10000, 8);
                return;
            case 4:
                SetItemsNeeded(10, 20000, 15);
                return;
            case 5:
                SetItemsNeeded(17, 3, 3);
                return;
            case 6:
                SetItemsNeeded(1000, 25000, 20);
                return;
            case 7:
                SetItemsNeeded(20, 20, 20);
                return;
            case 8:
                SetItemsNeeded(30, 1500, 30);
                return;
            case 9:
                SetItemsNeeded(35000, 25, 35);
                return;
            case 10:
                SetItemsNeeded(30, 2000, 30);
                return;
            case 11:
                SetItemsNeeded(30, 40000, 40);
                return;
            case 12:
                SetItemsNeeded(30, 30, 2500);
                return;
            case 13:
                SetItemsNeeded(2800, 50000, 40);
                return;
            case 14:
                SetItemsNeeded(40, 40, 40);
                return;
            case 15:
                SetItemsNeeded(40, 40, 40);
                return;
            case 16:
                SetItemsNeeded(3000, 55000, 40);
                return;
            case 17:
                SetItemsNeeded(45, 45, 45);
                return;
            case 18:
                SetItemsNeeded(50, 3500, 50);
                return;
            case 19:
                SetItemsNeeded(60000, 50, 55);
                return;
            case 20:
                SetItemsNeeded(60, 3700, 60);
                return;
            case 21:
                SetItemsNeeded(60, 70000, 60);
                return;
            case 22:
                SetItemsNeeded(60, 60, 4000);
                return;
            case 23:
                SetItemsNeeded(60, 75000, 60);
                return;
            case 24:
                SetItemsNeeded(65, 65, 65);
                return;
            case 25:
                SetItemsNeeded(65, 65, 50);
                return;
            case 26:
                SetItemsNeeded(4000, 80000, 70);
                return;
            case 27:
                SetItemsNeeded(70, 70, 70);
                return;
            case 28:
                SetItemsNeeded(80, 4500, 80);
                return;
            case 29:
                SetItemsNeeded(300000, 100, 10000);
                return;
            default:
                Debug.Log("Default");
                return;
        }
    }

    private void SetItemsNeeded(int itemNeeded1, int itemNeeded2, int itemNeeded3)
    {
        itemNeeded[0] = itemNeeded1;
        itemNeeded[1] = itemNeeded2;
        itemNeeded[2] = itemNeeded3;
    }

    private void SetQuestElements(int index1, int index2, int index3)
    {
        //UI quest 1
        quest[index1].questCompletedImage = completedImage[0];
        quest[index1].questText = textQuest[0];
        quest[index1].processText = statusText[0];
        //Quest 1
        quest[index1].itemCollected = itemCollected[0];
        quest[index1].itemNeeded = itemNeeded[0];
        quest[index1].questText.text = questText[index1];
        quest[index1].processText.text = quest[index1].itemCollected + "/" + quest[index1].itemNeeded;
        quest[index1].itemTag = questTag[index1];

        //UI quest 2
        quest[index2].questCompletedImage = completedImage[1];
        quest[index2].questText = textQuest[1];
        quest[index2].processText = statusText[1];
        //Quest 2
        quest[index2].itemCollected = itemCollected[1];
        quest[index2].itemNeeded = itemNeeded[1];
        quest[index2].questText.text = questText[index2];
        quest[index2].processText.text = quest[index2].itemCollected + "/" + quest[index2].itemNeeded;
        quest[index2].itemTag = questTag[index2];

        //UI quest 3
        quest[index3].questCompletedImage = completedImage[2];
        quest[index3].questText = textQuest[2];
        quest[index3].processText = statusText[2];
        //Quest 3
        quest[index3].itemCollected = itemCollected[2];
        quest[index3].itemNeeded = itemNeeded[2];
        quest[index3].questText.text = questText[index3];
        quest[index3].processText.text = quest[index3].itemCollected + "/" + quest[index3].itemNeeded;
        quest[index2].itemTag = questTag[index2];
    }

    private void SetQuest(int level)
    {
        switch (level)
        {
            case 1:
                SetQuestElements(0, 1, 2);
                return;

            case 2:
                SetQuestElements(3, 4, 5);
                return;

            case 3:
                SetQuestElements(6, 7, 8);
                return;

            case 4:
                SetQuestElements(9, 10, 11);
                return;

            case 5:
                SetQuestElements(12, 13, 14);
                return;

            case 6:
                SetQuestElements(15, 16, 17);
                return;

            case 7:
                SetQuestElements(18, 19, 20);
                return;

            case 8:
                SetQuestElements(21, 22, 23);
                return;

            case 9:
                SetQuestElements(24, 25, 26);
                return;

            case 10:
                SetQuestElements(27, 28, 29);
                return;

            case 11:
                SetQuestElements(30, 31, 32);
                return;

            case 12:
                SetQuestElements(33, 34, 35);
                return;

            case 13:
                SetQuestElements(36, 37, 38);
                return;

            case 14:
                SetQuestElements(39, 40, 41);
                return;

            case 15:
                SetQuestElements(42, 43, 44);
                return;

            case 16:
                SetQuestElements(45, 46, 47);
                return;

            case 17:
                SetQuestElements(48, 49, 50);
                return;

            case 18:
                SetQuestElements(51, 52, 53);
                return;

            case 19:
                SetQuestElements(54, 55, 56);
                return;

            case 20:
                SetQuestElements(57, 58, 59);
                return;

            case 21:
                SetQuestElements(60, 61, 62);
                return;

            case 22:
                SetQuestElements(63, 64, 65);
                return;

            case 23:
                SetQuestElements(66, 67, 68);
                return;

            case 24:
                SetQuestElements(69, 70, 71);
                return;

            case 25:
                SetQuestElements(72, 73, 74);
                return;

            case 26:
                SetQuestElements(75, 76, 77);
                return;

            case 27:
                SetQuestElements(78, 79, 80);
                return;

            case 28:
                SetQuestElements(81, 82, 83);
                return;

            case 29:
                SetQuestElements(84, 85, 86);
                return;

            default:
                Debug.Log("Default");
                return;
        }
    }

    public void SetColorQuest(int level)
    {
        switch (level)
        {
            case 1:
                SetColors(0, 1, 2);
                return;

            case 2:
                SetColors(3, 4, 5);
                return;

            case 3:
                SetColors(6, 7, 8);
                return;

            case 4:
                SetColors(9, 10, 11);
                return;

            case 5:
                SetColors(12, 13, 14);
                return;

            case 6:
                SetColors(15, 16, 17);
                return;

            case 7:
                SetColors(18, 19, 20);
                return;

            case 8:
                SetColors(21, 22, 23);
                return;

            case 9:
                SetColors(24, 25, 26);
                return;

            case 10:
                SetColors(27, 28, 29);
                return;

            case 11:
                SetColors(30, 31, 32);
                return;

            case 12:
                SetColors(33, 34, 35);
                return;

            case 13:
                SetColors(36, 37, 38);
                return;

            case 14:
                SetColors(39, 40 ,41);
                return;

            case 15:
                SetColors(42, 43, 44);
                return;

            case 16:
                SetColors(45, 46, 47);
                return;

            case 17:
                SetColors(48, 49, 50);
                return;

            case 18:
                SetColors(51, 52, 53);
                return;

            case 19:
                SetColors(54, 55, 56);
                return;

            case 20:
                SetColors(57, 58, 59);
                return;

            case 21:
                SetColors(60, 61, 62);
                return;

            case 22:
                SetColors(63, 64, 65);
                return;

            case 23:
                SetColors(66, 67, 68);
                return;

            case 24:
                SetColors(69, 70, 71);
                return;

            case 25:
                SetColors(72, 73, 74);
                return;

            case 26:
                SetColors(75, 76, 77);
                return;

            case 27:
                SetColors(78, 79, 80);
                return;

            case 28:
                SetColors(81, 82, 83);
                return;

            case 29:
                SetColors(84, 85, 86);
                return;

            default:
                Debug.Log("Default");
                return;
        }
    }

    private void SetColors(int index1, int index2, int index3)
    {
        quest[index1].isQuestCompleted = questComleted[0];
        quest[index2].isQuestCompleted = questComleted[1];
        quest[index3].isQuestCompleted = questComleted[2];

        SetColor(quest[index1].isQuestCompleted, quest[index1].questCompletedImage);
        SetColor(quest[index2].isQuestCompleted, quest[index2].questCompletedImage);
        SetColor(quest[index3].isQuestCompleted, quest[index3].questCompletedImage);
    }

    public void SetButton()
    {
        //TODO: убрать кнопки если уровень выше 30
        for (int i = 0; i < skipQuestButton.Length; i++)
        {
            if (questComleted[i])
            {
                skipQuestButton[i].SetActive(false);
            }
            else
            {
                skipQuestButton[i].SetActive(true);
            }
        }
    }

    private void SetColor(bool isQuestCompleted, Image image)
    {
        if (isQuestCompleted)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
        }
    }


    public void SkipQuest(int indexQuest)
    {
        if (coins >= costSkipQuest)
        {
            if (level < 30)
            {
                coins -= costSkipQuest;
                questComleted[indexQuest] = true;
                itemCollected[indexQuest] = itemNeeded[indexQuest];
                SetQuest(level);
                SetColorQuest(level);
                SetButton();
            }
            else
            {
                coins -= costSkipQuest;
                questComleted[indexQuest] = true;
                itemCollected[indexQuest] = itemNeeded[indexQuest];
                SetQuest(randomQuest);
                SetColorQuest(randomQuest);
                SetButton();
            }
        }
        else
        {
            Debug.Log("Недостаточно средств");
        }
        LevelUp();
    }

    //Добавить проверку на максимальный уровень
    private void LevelUp()
    {
        if (questComleted[0] && questComleted[1] && questComleted[2])
        {
            if (level < 30)
            {
                level += 1;
                if (level == 30)
                {
                    level = 30;
                    randomQuest = Random.Range(0, 29);
                    UpdateDB();
                    SetItemNeeded(randomQuest);
                    SetQuest(randomQuest);
                    SetColorQuest(randomQuest);
                    SetButton();
                }
                else
                {
                    UpdateDB();
                    SetItemNeeded(level);
                    SetQuest(level);
                    SetColorQuest(level);
                    SetButton();
                }
            }

            if (level == 30)
            {
                level = 30;

                //Награды
                coins += 1000;
                diamonds += 10;
                //добавить колесо фортун
                randomQuest = Random.Range(0, 29);
                UpdateDB();
                SetItemNeeded(randomQuest);
                SetQuest(randomQuest);
                SetColorQuest(randomQuest);
                SetButton();
            }
        }
    }

    private void UpdateDB()
    {
        for (int i = 0; i < questComleted.Length; i++)
        {
            questComleted[i] = false;
            itemCollected[i] = 0;
        }
    }
    #endregion

    #region DonateShop
    public void BuyCrystals(int amountCrystal)
    {
        crystals += amountCrystal;
    }

    public void ConvertCrystalsCoins(int amountCrystal, int amountCoin)
    {
        if (amountCrystal <= crystals)
        {
            crystals -= amountCrystal;
            coins += amountCoin;
        }
        else
        {
            Debug.Log("Недостаточно средств");
        }
    }
    #endregion
}
