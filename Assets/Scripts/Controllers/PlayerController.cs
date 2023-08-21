using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Характеристики персонажа")]
    [SerializeField, Range(0, 35)] private float speed = 8;
    [SerializeField, Range(5, 30)] public float jumpForce;
    private const int speedUp = 1;
    private const int maxScoresAdd = 35;
    private const int speedUpTimer = 10;  //10
    private const int maxSpeed = 35;
    private bool isHit = false;

    [Space(6)]
    [Header("Настройка уровня")]
    [SerializeField, Range(0, -50)] private float Gravity;
    [SerializeField, Range(1, 8)] private float lineDistance;
    private int lineToMove = 1; //0: Left 1: Center 2: Right

    [Space(6)]
    [Header("Бонусы")]
    public bool x2Score = false;
    [Space(2)]
    public bool x2Coin = false;
    [Space(2)]
    public bool shield = false;
    [Space(2)]
    public bool megaJump = false;

    [Space(6)]
    [Header("Уровень")]
    public int level;

    [Space(6)]
    [Header("Скины")]
    public GameObject[] skins;
    private bool[] skinBuy;
    private int skinSelect;

    public GameObject barrier;
    private Vector3 dir;

    [HideInInspector]
    private CharacterController controller;
    private LevelUIManager levelUIManager;
    private Coin coinManager;
    private Score scoreManager;
    private QuestManager questManager;
    private BonusManager bonusManager;

    private const string saveKey = "mainSave";

    public void Initialize()
    {
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
        levelUIManager = FindObjectOfType<LevelUIManager>();
        coinManager = FindObjectOfType<Coin>();
        scoreManager = FindObjectOfType<Score>();
        questManager = FindObjectOfType<QuestManager>();
        bonusManager = FindObjectOfType<BonusManager>();
        Load();
        SetSpeed(scoreManager.level);
        Skin();
        StartCoroutine(SpeedUpCO());
    }

    //Скорость и движение персонажа
    private void FixedUpdate()
    {
        dir.z = speed;
        dir.y += Gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    //Перемещение персонажа
    private void Update()
    {
        if (controller.isGrounded)
        {
            if (SwipeController.swipeUp)
            {
                Jump();
            }
            if (SwipeController.swipeDown)
            {
                StartCoroutine(Slide());
            }
        }
        else
        {
            dir.y += Gravity * Time.deltaTime;
            if (SwipeController.swipeDown)
            {
                StartCoroutine(Slide());
                dir.y = -10;
            }
        }

        if (SwipeController.swipeRight)
        {
            lineToMove++;
            if (lineToMove == 3)
            {
                lineToMove = 2;
            }
        }
        if (SwipeController.swipeLeft)
        {
            lineToMove--;
            if (lineToMove == -1)
            {
                lineToMove = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
        {
            targetPosition += Vector3.left * lineDistance;
        }
        else if (lineToMove == 2)
        {
            targetPosition += Vector3.right * lineDistance;
        }

        if (transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }

    private void Skin()
    {
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

    private void Jump()
    {
        dir.y = jumpForce;
        if (scoreManager.level < 30)
        {
            questManager.Quest("Jump", scoreManager.level);
        }
        else
        {
            questManager.Quest("Jump", questManager.randomQuest);
        }
    }

    private void SetSpeed(int level)
    {
        speed += .5f * level; 
    }

    private IEnumerator Slide()
    {
        if (scoreManager.level < 30)
        {
            questManager.Quest("Slide", scoreManager.level);
        }
        else
        {
            questManager.Quest("Slide", questManager.randomQuest);
        }
        controller.center = new Vector3(0, -.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.5f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
    }

    private IEnumerator SpeedUpCO()
    {
        yield return new WaitForSeconds(speedUpTimer);
        if (speed < maxSpeed)
        {
            if (scoreManager.scoresAdd < maxScoresAdd)
            {
                scoreManager.scoresAdd += speedUp;
            }
            speed += speedUp;
            StartCoroutine(SpeedUpCO());
        }
        if (speed >= maxSpeed)
        {
            scoreManager.scoresAdd = maxScoresAdd;
        }
        Debug.Log("Speed: " + speed + " Scores: " + scoreManager.scoresAdd);
    }

    private IEnumerator IsHitCO()
    {
        Debug.Log("Player is hit");
        isHit = true;

        yield return new WaitForSeconds(5);

        isHit = false;
        Debug.Log("Player is hit end");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        //Высокие преграды
        if (hit.gameObject.tag == "HighObstacle")
        {
            if (this.gameObject.transform.position.y >= 4.52 && this.gameObject.transform.position.y <= 5.5)
            {
                if (this.gameObject.transform.position.y >= 5 && this.gameObject.transform.position.y < 5.3)
                {
                    hit.gameObject.tag = "NonObstacle";
                }
                else
                {
                    if (isHit)
                    {
                        if (shield)
                        {
                            Debug.Log("The shield went off");
                            Destroy(hit.gameObject);
                            bonusManager.StopShieldTimer();
                            shield = false;
                        }
                        else
                        {
                            //DIE
                            barrier = hit.gameObject;
                            SaveDB();
                            levelUIManager.LoseGame();
                        }
                    }
                    else
                    {
                        hit.gameObject.tag = "NonObstacle";
                        StartCoroutine(IsHitCO());
                    }
                }
            }
            else
            {
                if (shield)
                {
                    Debug.Log("The shield went off");
                    Destroy(hit.gameObject);
                    bonusManager.StopShieldTimer();
                    shield = false;
                }
                else
                {
                    //DIE
                    barrier = hit.gameObject;
                    SaveDB();
                    levelUIManager.LoseGame();
                }
            }
        }

        //Низкие преграды
        if (hit.gameObject.tag == "Obstacle")
        {
            if (this.gameObject.transform.position.y >= 2.48 && this.gameObject.transform.position.y <= 4)
            {
                if (this.gameObject.transform.position.y >= 3.07 && this.gameObject.transform.position.y < 3.3)
                {
                    hit.gameObject.tag = "NonObstacle";
                }
                else
                {
                    if (isHit)
                    {
                        if (shield)
                        {
                            Debug.Log("The shield went off");
                            Destroy(hit.gameObject);
                            bonusManager.StopShieldTimer();
                            shield = false;
                        }
                        else
                        {
                            //DIE
                            barrier = hit.gameObject;
                            SaveDB();
                            levelUIManager.LoseGame();
                        }
                    }
                    else
                    {
                        hit.gameObject.tag = "NonObstacle";
                        StartCoroutine(IsHitCO());
                    }
                }
            }
            else
            {
                if (shield)
                {
                    Debug.Log("The shield went off");
                    Destroy(hit.gameObject);
                    bonusManager.StopShieldTimer();
                    shield = false;
                }
                else
                {
                    //DIE
                    barrier = hit.gameObject;
                    SaveDB();
                    levelUIManager.LoseGame();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Сбор монеток
        if (other.gameObject.tag == "Coin")
        {
            if (scoreManager.level < 30)
            {
                questManager.Quest(other.gameObject.tag, scoreManager.level);
            }
            else
            {
                questManager.Quest(other.gameObject.tag, questManager.randomQuest);
            }
            Destroy(other.gameObject);
            if (x2Coin)
            {
                coinManager.AddCoin(true);
            }
            else
            {
                coinManager.AddCoin(false);
            }
        }

        //Двойные очки
        if (other.gameObject.tag == "DoubleScore")
        {
            if (scoreManager.level < 30)
            {
                questManager.Quest(other.gameObject.tag, scoreManager.level);
            }
            else
            {
                questManager.Quest(other.gameObject.tag, questManager.randomQuest);
            }
            Destroy(other.gameObject);
            if (x2Score)
            {
                bonusManager.UpdateTimer(1);
            }
            else
            {
                x2Score = true;
            }
        }

        //Двойные монеты
        if (other.gameObject.tag == "DoubleCoin")
        {
            if (scoreManager.level < 30)
            {
                questManager.Quest(other.gameObject.tag, scoreManager.level);
            }
            else
            {
                questManager.Quest(other.gameObject.tag, questManager.randomQuest);
            }
            Destroy(other.gameObject);
            if (x2Coin)
            {
                bonusManager.UpdateTimer(2);
            }
            else
            {
                x2Coin = true;
            }
        }

        //Щит
        if (other.gameObject.tag == "Shield")
        {
            if (scoreManager.level < 30)
            {
                questManager.Quest(other.gameObject.tag, scoreManager.level);
            }
            else
            {
                questManager.Quest(other.gameObject.tag, questManager.randomQuest);
            }
            Destroy(other.gameObject);
            if (shield)
            {
                bonusManager.UpdateTimer(3);
            }
            else
            {
                shield = true;
            }
        }

        //Мега прыжок
        if (other.gameObject.tag == "MegaJump")
        {
            Destroy(other.gameObject);
            if (megaJump)
            {
                bonusManager.UpdateTimer(4);
                if (scoreManager.level < 30)
                {
                    questManager.Quest(other.gameObject.tag, scoreManager.level);
                }
                else
                {
                    questManager.Quest(other.gameObject.tag, questManager.randomQuest);
                }
            }
            else
            {
                megaJump = true;
                if (scoreManager.level < 30)
                {
                    questManager.Quest(other.gameObject.tag, scoreManager.level);
                }
                else
                {
                    questManager.Quest(other.gameObject.tag, questManager.randomQuest);
                }
            }
        }
    }

    #region DataBase
    //Методы для сохранений
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

        coinManager.coin = data.coins;
        coinManager.crystal = data.crystals;
        coinManager.diamond = data.diamond;

        scoreManager.highScore = data.highScore;
        scoreManager.level = data.level;

        questManager.randomQuest = data.randomQuest;
        questManager.questComleted = data.questsCompleted;
        questManager.itemCollected = data.itemCollected;
        questManager.itemNeeded = data.itemNeeded;
        questManager.questText = data.questText;
        questManager.questTag = data.questTag;

        bonusManager.durationX2Score = data.durationX2Score;
        bonusManager.durationX2Coin = data.durationX2Coin;
        bonusManager.durationShield = data.durationShield;
        bonusManager.durationJump = data.durationMegaJump;

        bonusManager.levelX2Score = data.levelX2Score;
        bonusManager.levelX2Coin = data.levelX2Coin;
        bonusManager.levelShield = data.levelShield;
        bonusManager.levelJump = data.levelMegaJump;

        skinBuy = data.skinBuy;
        skinSelect = data.skinSelect;
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
            coins = coinManager.coin,
            crystals = coinManager.crystal,
            diamond = coinManager.diamond,

            highScore = scoreManager.highScore,
            level = scoreManager.level,

            randomQuest = questManager.randomQuest,
            questsCompleted = questManager.questComleted,
            itemCollected = questManager.itemCollected,
            itemNeeded = questManager.itemNeeded,
            questText = questManager.questText,
            questTag = questManager.questTag,

            durationX2Score = bonusManager.durationX2Score,
            durationX2Coin = bonusManager.durationX2Coin,
            durationShield = bonusManager.durationShield,
            durationMegaJump = bonusManager.durationJump,

            levelX2Score = bonusManager.levelX2Score,
            levelX2Coin = bonusManager.levelX2Coin,
            levelShield = bonusManager.levelShield,
            levelMegaJump = bonusManager.levelJump,

            skinBuy = skinBuy,
            skinSelect = skinSelect
        };

        return data;
    }
    #endregion
}
