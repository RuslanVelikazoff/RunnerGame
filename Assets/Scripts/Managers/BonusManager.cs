using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusTag
{
    public static string DoubleCoin = "DoubleCoin";
    public static string DoubleScore = "DoubleScore";
    public static string Shield = "Shield";
    public static string MegaJump = "MegaJump";

    public static string Score = "Score";
    public static string Coin = "Coin";

    public static string Jump = "Jump";
    public static string Slide = "Slide";
}

public class BonusManager : MonoBehaviour
{
    [Header("Изображения бонусов")]
    [SerializeField] private Image X2ScoreImage;
    [SerializeField] private GameObject X2ScoreGameObject;
    [Space(2)]
    [SerializeField] private Image X2CoinImage;
    [SerializeField] private GameObject X2CoinGameObject;
    [Space(2)]
    [SerializeField] private Image ShieldImage;
    [SerializeField] private GameObject ShieldGameObject;
    [Space(2)]
    [SerializeField] private Image MegaJumpImage;
    [SerializeField] private GameObject MegaJumpGameObject;

    [Space(7)]
    [Header("Настройки бонусов")]
    public int levelX2Score;
    public float durationX2Score;
    public float timeLeftX2Score;
    [Space(2)]
    public int levelX2Coin;
    public float durationX2Coin;
    public float timeLeftX2Coin;
    [Space(2)]
    public int levelShield;
    public float durationShield;
    public float timeLeftShield;
    [Space(2)]
    public int levelJump;
    public float durationJump;
    public float timeLeftJump;

    private PlayerController player;

    public void Initialize()
    {
        player = FindObjectOfType<PlayerController>();

        X2ScoreImage.fillAmount = 0;
        X2CoinImage.fillAmount = 0;
        ShieldImage.fillAmount = 0;
        MegaJumpImage.fillAmount = 0;
    }

    private void Update()
    {
        //Двойные очки
        if (player.x2Score)
        {
            X2ScoreGameObject.SetActive(true);
            timeLeftX2Score -= Time.deltaTime;
            AnimationImage(timeLeftX2Score, durationX2Score, X2ScoreImage);

            if (timeLeftX2Score < 0)
            {
                player.x2Score = false;
                timeLeftX2Score = 0;
            }
        }
        else
        {
            X2ScoreGameObject.SetActive(false);
            timeLeftX2Score = durationX2Score;
            player.x2Score = false;
        }

        //Двойные монеты
        if (player.x2Coin)
        {
            X2CoinGameObject.SetActive(true);
            timeLeftX2Coin -= Time.deltaTime;
            AnimationImage(timeLeftX2Coin, durationX2Coin, X2CoinImage);

            if (timeLeftX2Coin < 0)
            {
                player.x2Coin = false;
                timeLeftX2Coin = 0;
            }
        }
        else
        {
            X2CoinGameObject.SetActive(false);
            timeLeftX2Coin = durationX2Coin;
            player.x2Coin = false;
        }

        //Щит
        if (player.shield)
        {
            ShieldGameObject.SetActive(true);
            timeLeftShield -= Time.deltaTime;
            AnimationImage(timeLeftShield, durationShield, ShieldImage);

            if (timeLeftShield < 0)
            {
                player.shield = false;
                timeLeftShield = 0;
            }
        }
        else
        {
            ShieldGameObject.SetActive(false);
            timeLeftShield = durationShield;
            player.shield = false;
        }

        //Большой прыжок
        if (player.megaJump)
        {
            MegaJumpGameObject.SetActive(true);
            timeLeftJump -= Time.deltaTime;
            player.jumpForce = 20;
            AnimationImage(timeLeftJump, durationJump, MegaJumpImage);

            if (timeLeftJump < 0)
            {
                player.megaJump = false;
                player.jumpForce = 15;
                timeLeftJump = 0;
            }
        }
        else
        {
            MegaJumpGameObject.SetActive(false);
            timeLeftJump = durationJump;
            player.jumpForce = 15;
            player.megaJump = false;
        }
    }

    /*Обновление тайиер
     1: Двойные очки
     2: Двоные монеты
     3: Щит
     4: Большой прыжок*/
    public void UpdateTimer(int typeBonus) 
    {
        switch (typeBonus)
        {
            case 1:
                timeLeftX2Score = durationX2Score;
                break;

            case 2:
                timeLeftX2Coin = durationX2Coin;
                break;

            case 3:
                timeLeftShield = durationShield;
                break;

            case 4:
                timeLeftJump = durationJump;
                break;
        }
    }

    public void StopShieldTimer()
    {
        timeLeftShield = 0;
        AnimationImage(timeLeftShield, durationShield, ShieldImage);
    }

    public void AnimationImage(float timeLeft, float time, Image bonusImage)
    {
        var normalizedValue = Mathf.Clamp(timeLeft / time, 0.0f, 1.0f);
        bonusImage.fillAmount = normalizedValue;
    }
}
