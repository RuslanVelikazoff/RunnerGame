using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [Header("Панель меню")]
    [SerializeField] private GameObject menuPanel;
    [Header("Нижняя панель")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button audioButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button questButton;
    [SerializeField] private Button shopButton;
    [Header("Верхняя панель")]
    [SerializeField] private Button addCoinButton;
    [SerializeField] private Button addCrystalButton;
    [SerializeField] private Text coinText;
    [SerializeField] private Text crystalText;
    [SerializeField] private Text recordText;
    [SerializeField] private Text levelText;

    [Space(6)]
    [Header("Панель с заданиями")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private Button closeQuestButton;
    [SerializeField] private Button skipQuestButton1;
    [SerializeField] private Button skipQuestButton2;
    [SerializeField] private Button skipQuestButton3;

    [Space(6)]
    [Header("Панель магазина")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject bonusPanel;
    [SerializeField] private GameObject skinPanel;
    [SerializeField] private GameObject donatePanel;
    [SerializeField] private GameObject player;
    [SerializeField] private Button bonusButton;
    [SerializeField] private Button skinButton;
    [SerializeField] private Button donateShopButton;
    [SerializeField] private Button closeShopButton;
    [SerializeField] private Text coinInShopPanel;
    [SerializeField] private Text crystalInShopPanel;

    [Space(6)]
    [Header("Улучшения бонусов")]
    [SerializeField] private Button upgradeX2ScoreButton;
    [SerializeField] private Button upgradeX2CoinButton;
    [SerializeField] private Button upgradeShieldButton;
    [SerializeField] private Button upgradeMegaJumpButton;

    [Space(6)]
    [Header("Магазин скинов")]
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button buySelectButton;

    [Space(6)]
    [Header("Магазин доната")]
    [SerializeField] private Text coinInDonateText;
    [SerializeField] private Text crystalInDonateText;
    [SerializeField] private Button closeDonateButton;
    [SerializeField] private Button buy100CrystalButton;
    [SerializeField] private Button buy500CrystalButton;
    [SerializeField] private Button buy1000CrystalButton;
    [SerializeField] private Button buy1500CrystalButton;
    [SerializeField] private Button convert10CrystalButton;
    [SerializeField] private Button convert100CrystalButton;
    [SerializeField] private Button watchAdButton;

    private ShopManager shopManager;

    public void Initialize()
    {
        AudioManager.Instance.PaintingButtons(audioButton, soundButton);
        shopManager = FindObjectOfType<ShopManager>();

        player.SetActive(true);

        InitButton();
        InitText();
    }

    void InitButton()
    {
        #region Нижняя панель
        if (startGameButton != null)
        {
            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Level");
            });
        }

        if (audioButton != null)
        {
            audioButton.onClick.RemoveAllListeners();
            audioButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.OffOnMusic(audioButton);
            });
        }

        if (soundButton != null)
        {
            soundButton.onClick.RemoveAllListeners();
            soundButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.OffOnSound(soundButton);
            });
        }

        if (questButton != null)
        {
            questButton.onClick.RemoveAllListeners();
            questButton.onClick.AddListener(() =>
            {
                questPanel.SetActive(true);
                player.SetActive(false);
            });
        }

        if (shopButton != null)
        {
            shopButton.onClick.RemoveAllListeners();
            shopButton.onClick.AddListener(() =>
            {
                shopManager.OpenShop();
                InitMoneyText(coinInShopPanel, crystalInShopPanel);
                menuPanel.SetActive(false);
                shopPanel.SetActive(true);
                skinPanel.SetActive(true);
                player.SetActive(true);
            });
        }
        #endregion

        #region Верхняя панель
        if (addCoinButton != null)
        {
            addCoinButton.onClick.RemoveAllListeners();
            addCoinButton.onClick.AddListener(() =>
            {
                OpenDonatePanel();
            });
        }

        if (addCrystalButton != null)
        {
            addCrystalButton.onClick.RemoveAllListeners();
            addCrystalButton.onClick.AddListener(() =>
            {
                OpenDonatePanel();
            });
        }
        #endregion

        #region Панель с заданиями
        if (closeQuestButton != null)
        {
            closeQuestButton.onClick.RemoveAllListeners();
            closeQuestButton.onClick.AddListener(() =>
            {
                questPanel.SetActive(false);
                player.SetActive(true);
            });
        }

        if (skipQuestButton1 != null)
        {
            skipQuestButton1.onClick.RemoveAllListeners();
            skipQuestButton1.onClick.AddListener(() =>
            {
                shopManager.SkipQuest(0);
                InitText();
            });
        }

        if (skipQuestButton2 != null)
        {
            skipQuestButton2.onClick.RemoveAllListeners();
            skipQuestButton2.onClick.AddListener(() =>
            {
                shopManager.SkipQuest(1);
                InitText();
            });
        }

        if (skipQuestButton3 != null)
        {
            skipQuestButton3.onClick.RemoveAllListeners();
            skipQuestButton3.onClick.AddListener(() =>
            {
                shopManager.SkipQuest(2);
                InitText();
            });
        }
        #endregion

        #region Панель магазина
        if (bonusButton != null)
        {
            bonusButton.onClick.RemoveAllListeners();
            bonusButton.onClick.AddListener(() =>
            {
                bonusPanel.SetActive(true);
                player.SetActive(false);
                skinPanel.SetActive(false);
                questPanel.SetActive(false);
            });
        }

        if (skinButton != null)
        {
            skinButton.onClick.RemoveAllListeners();
            skinButton.onClick.AddListener(() =>
            {
                bonusPanel.SetActive(false);
                questPanel.SetActive(false);
                skinPanel.SetActive(true);
                player.SetActive(true);
            });
        }

        if (donateShopButton != null)
        {
            donateShopButton.onClick.RemoveAllListeners();
            donateShopButton.onClick.AddListener(() =>
            {
                OpenDonatePanel();
            });
        }

        if (closeShopButton != null)
        {
            closeShopButton.onClick.RemoveAllListeners();
            closeShopButton.onClick.AddListener(() =>
            {
                shopManager.CloseShop();
                questPanel.SetActive(false);
                shopPanel.SetActive(false);
                bonusPanel.SetActive(false);
                player.SetActive(true);
                skinPanel.SetActive(false);
                menuPanel.SetActive(true);
            });
        }
        #endregion

        #region Магазин улучшений
        if (upgradeX2ScoreButton != null)
        {
            upgradeX2ScoreButton.onClick.RemoveAllListeners();
            upgradeX2ScoreButton.onClick.AddListener(() =>
            {
                shopManager.BuyUpgradeX2Score();
                InitText();
                InitMoneyText(coinInShopPanel, crystalInShopPanel);
            });
        }

        if (upgradeX2CoinButton != null)
        {
            upgradeX2CoinButton.onClick.RemoveAllListeners();
            upgradeX2CoinButton.onClick.AddListener(() =>
            {
                shopManager.BuyUpgradeX2Coin();
                InitText();
                InitMoneyText(coinInShopPanel, crystalInShopPanel);
            });
        }

        if (upgradeShieldButton != null)
        {
            upgradeShieldButton.onClick.RemoveAllListeners();
            upgradeShieldButton.onClick.AddListener(() =>
            {
                shopManager.BuyUpgradeShield();
                InitText();
                InitMoneyText(coinInShopPanel, crystalInShopPanel);
            });
        }

        if (upgradeMegaJumpButton != null)
        {
            upgradeMegaJumpButton.onClick.RemoveAllListeners();
            upgradeMegaJumpButton.onClick.AddListener(() =>
            {
                shopManager.BuyUpgradeMegaJump();
                InitText();
                InitMoneyText(coinInShopPanel, crystalInShopPanel);
            });
        }
        #endregion

        #region Магазин скинов
        if (rightButton != null)
        {
            rightButton.onClick.RemoveAllListeners();
            rightButton.onClick.AddListener(() =>
            {
                shopManager.RighButton();
            });
        }

        if (leftButton != null)
        {
            leftButton.onClick.RemoveAllListeners();
            leftButton.onClick.AddListener(() =>
            {
                shopManager.LeftButton();
            });
        }

        if (buySelectButton != null)
        {
            buySelectButton.onClick.RemoveAllListeners();
            buySelectButton.onClick.AddListener(() =>
            {
                shopManager.BuySelectButton();
                InitText();
                InitMoneyText(coinInShopPanel, crystalInShopPanel);
            });
        }
        #endregion

        #region Магазин донат
        if (closeDonateButton != null)
        {
            closeDonateButton.onClick.RemoveAllListeners();
            closeDonateButton.onClick.AddListener(() =>
            {
                donatePanel.SetActive(false);
                InitText();
                InitMoneyText(coinInShopPanel, crystalInShopPanel);
                player.transform.position = new Vector3(0, 1.82f, 0);
            });
        }

        if (buy100CrystalButton != null)
        {
            buy100CrystalButton.onClick.RemoveAllListeners();
            buy100CrystalButton.onClick.AddListener(() =>
            {
                shopManager.BuyCrystals(100);
                InitMoneyText(coinInDonateText, crystalInDonateText);
                Debug.Log("Добавлено 100 кристалов");
            });
        }

        if (buy500CrystalButton != null)
        {
            buy500CrystalButton.onClick.RemoveAllListeners();
            buy500CrystalButton.onClick.AddListener(() =>
            {
                shopManager.BuyCrystals(500);
                InitMoneyText(coinInDonateText, crystalInDonateText);
                Debug.Log("Добавлено 500 кристалов");
            });
        }

        if (buy1000CrystalButton != null)
        {
            buy1000CrystalButton.onClick.RemoveAllListeners();
            buy1000CrystalButton.onClick.AddListener(() =>
            {
                shopManager.BuyCrystals(1000);
                InitMoneyText(coinInDonateText, crystalInDonateText);
                Debug.Log("Добавлено 1000 кристалов");
            });
        }

        if (buy1500CrystalButton != null)
        {
            buy1500CrystalButton.onClick.RemoveAllListeners();
            buy1500CrystalButton.onClick.AddListener(()=>
            {
                shopManager.BuyCrystals(1500);
                InitMoneyText(coinInDonateText, crystalInDonateText);
                Debug.Log("Добавлено 1500 кристалов");
            });
        }

        if (convert10CrystalButton != null)
        {
            convert10CrystalButton.onClick.RemoveAllListeners();
            convert10CrystalButton.onClick.AddListener(() =>
            {
                shopManager.ConvertCrystalsCoins(10, 1000);
                InitMoneyText(coinInDonateText, crystalInDonateText);
            });
        }

        if (convert100CrystalButton != null)
        {
            convert100CrystalButton.onClick.RemoveAllListeners();
            convert100CrystalButton.onClick.AddListener(() =>
            {
                shopManager.ConvertCrystalsCoins(100, 10000);
                InitMoneyText(coinInDonateText, crystalInDonateText);
            });
        }

        if (watchAdButton != null)
        {
            watchAdButton.onClick.RemoveAllListeners();
            watchAdButton.onClick.AddListener(()=>
            {
                Debug.Log("Ad");
            });
        }
        #endregion
    }

    void InitText()
    {
        coinText.text = "Монет: " + shopManager.coins;
        crystalText.text = "Кристалов: " + shopManager.crystals;
        recordText.text = "Рекорд: " + shopManager.highScore;
        levelText.text = "" + shopManager.level;
    }

    private void InitMoneyText(Text coinText, Text crystalText)
    {
        coinText.text = "Монет: " + shopManager.coins;
        crystalText.text = "Кристалов: " + shopManager.crystals;
    }

    private void OpenDonatePanel()
    {
        donatePanel.SetActive(true);
        player.transform.position = new Vector3(0, 1.82f, -20);
        InitMoneyText(coinInDonateText, crystalInDonateText);
    }
}
