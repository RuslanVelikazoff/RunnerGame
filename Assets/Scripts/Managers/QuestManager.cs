using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class QuestBlank
{
    [Header("Задание")]
    public int itemNeeded;
    public int itemCollected;
    public string itemTag;

    [Space(7)]
    [Header("Выполнено ли?")]
    public bool isQuestCompleted;
    public Image questCompletedImage;

    [Space(7)]
    [Header("Текст задания")]
    public Text questText;
    public Text processText;
}

public class QuestManager : MonoBehaviour
{
    public QuestBlank[] quest;

    [SerializeField]
    private Image[] statusImage;
    [SerializeField]
    private Text[] questsText;
    [SerializeField]
    private Text[] statusText;

    public int randomQuest;

    public string[] questText;

    public bool[] questComleted;

    public int[] itemCollected;

    public int[] itemNeeded;

    public string[] questTag;

    private Score scoreManager;
    private PlayerController player;

    public void Initialize()
    {
        scoreManager = FindObjectOfType<Score>(); //Узнаем текущий уровень персонажа
        player = FindObjectOfType<PlayerController>();

        if (scoreManager.level < 30)
        {
            SetText(scoreManager.level);
            ChangeStatusDB(scoreManager.level);
            ChangeItemDB(scoreManager.level);
            SetCheckerColor(scoreManager.level);
        }
        else
        {
            SetText(randomQuest);
            ChangeStatusDB(randomQuest);
            ChangeItemDB(randomQuest);
            SetCheckerColor(randomQuest);
        }
    }

    //Метод продвижения по квесту
    public void Quest(string Tag, int level) 
    {
        switch (level)
        {
            case 1:
                Cycle(0, 2, Tag, level);
                return;
            case 2:
                Cycle(3, 5, Tag, level);
                return;
            case 3:
                Cycle(6, 8, Tag, level);
                return;
            case 4:
                Cycle(9, 11, Tag, level);
                return;
            case 5:
                Cycle(12, 14, Tag, level);
                return;
            case 6:
                Cycle(15, 17, Tag, level);
                return;
            case 7:
                Cycle(18, 20, Tag, level);
                return;
            case 8:
                Cycle(21, 23, Tag, level);
                return;
            case 9:
                Cycle(24, 26, Tag, level);
                return;
            case 10:
                Cycle(27, 29, Tag, level);
                return;
            case 11:
                Cycle(30, 32, Tag, level);
                return;
            case 12:
                Cycle(33, 35, Tag, level);
                return;
            case 13:
                Cycle(36, 38, Tag, level);
                return;
            case 14:
                Cycle(39, 41, Tag, level);
                return;
            case 15:
                Cycle(42, 44, Tag, level);
                return;
            case 16:
                Cycle(45, 47, Tag, level);
                return;
            case 17:
                Cycle(48, 50, Tag, level);
                return;
            case 18:
                Cycle(51, 53, Tag, level);
                return;
            case 19:
                Cycle(54, 56, Tag, level);
                return;
            case 20:
                Cycle(57, 59, Tag, level);
                return;
            case 21:
                Cycle(60, 62, Tag, level);
                return;
            case 22:
                Cycle(63, 65, Tag, level);
                return;
            case 23:
                Cycle(66, 68, Tag, level);
                return;
            case 24:
                Cycle(69, 71, Tag, level);
                return;
            case 25:
                Cycle(72, 74, Tag, level);
                return;
            case 26:
                Cycle(75, 77, Tag, level);
                return;
            case 27:
                Cycle(78, 80, Tag, level);
                return;
            case 28:
                Cycle(81, 83, Tag, level);
                return;
            case 29:
                Cycle(84, 86, Tag, level);
                return;
            default:
                Debug.Log("Default");
                return;
        }
    }

    private void Cycle(int minIndex, int maxIndex, string tag, int level)
    {
        for (int i = minIndex; i < maxIndex + 1; i++)
        {
            if (quest[i].itemTag == tag)
            {
                if (quest[i].itemTag == "Coin")
                {
                    //Coin
                    if (player.x2Coin)
                    {
                        quest[i].itemCollected += 2;
                        UpdateSoloStatusText(quest[i], level);
                    }
                    else
                    {
                        quest[i].itemCollected++;
                        UpdateSoloStatusText(quest[i], level);
                    }
                }
                else if (quest[i].itemTag == "Score")
                {
                    quest[i].itemCollected += scoreManager.scoresAdd;
                    UpdateSoloStatusText(quest[i], level);
                }
                else
                {
                    quest[i].itemCollected++;
                    UpdateSoloStatusText(quest[i], level);
                }
            }
        }
    }

    //Обновление текста задания
    public void UpdateSoloStatusText(QuestBlank quest, int level)
    {
        if (quest.itemCollected >= quest.itemNeeded)
        {
            quest.itemCollected = quest.itemNeeded;
            quest.isQuestCompleted = true;
            ChangeStatusDB(level);
            SetColor(quest.isQuestCompleted, quest.questCompletedImage);
            quest.processText.text = quest.itemCollected + "/" + quest.itemNeeded;
        }
        else
        {
            quest.processText.text = quest.itemCollected + "/" + quest.itemNeeded;
        }
        ChangeItemDB(level);
        LevelUP();
    }

    //Обновление базы данных
    private void ChangeStatusDB(int level) 
    {
        switch (level)
        {
            case 1:
                ChangeStatus(0, 1, 2);
                return;
            case 2:
                ChangeStatus(3, 4, 5);
                return;
            case 3:
                ChangeStatus(6, 7, 8);
                return;
            case 4:
                ChangeStatus(9, 10, 11);
                return;
            case 5:
                ChangeStatus(12, 13, 14);
                return;
            case 6:
                ChangeStatus(15, 16, 17);
                return;
            case 7:
                ChangeStatus(18, 19, 20);
                return;
            case 8:
                ChangeStatus(21, 22, 23);
                return;
            case 9:
                ChangeStatus(24, 25, 26);
                return;
            case 10:
                ChangeStatus(27, 28, 29);
                return;
            case 11:
                ChangeStatus(30, 31, 32);
                return;
            case 12:
                ChangeStatus(33, 34, 35);
                return;
            case 13:
                ChangeStatus(36, 37, 38);
                return;
            case 14:
                ChangeStatus(39, 40, 41);
                return;
            case 15:
                ChangeStatus(42, 43, 44);
                return;
            case 16:
                ChangeStatus(45, 46, 47);
                return;
            case 17:
                ChangeStatus(48, 49, 50);
                return;
            case 18:
                ChangeStatus(51, 52, 53);
                return;
            case 19:
                ChangeStatus(54, 55, 56);
                return;
            case 20:
                ChangeStatus(57, 58, 59);
                return;
            case 21:
                ChangeStatus(60, 61, 62);
                return;
            case 22:
                ChangeStatus(63, 64, 65);
                return;
            case 23:
                ChangeStatus(66, 67, 68);
                return;
            case 24:
                ChangeStatus(69, 70, 71);
                return;
            case 25:
                ChangeStatus(72, 73, 74);
                return;
            case 26:
                ChangeStatus(75, 76, 77);
                return;
            case 27:
                ChangeStatus(78, 79, 80);
                return;
            case 28:
                ChangeStatus(81, 82, 83);
                return;
            case 29:
                ChangeStatus(84, 85, 86);
                return;
            default:
                Debug.Log("Default");
                return;
        }
    }

    private void ChangeStatus(int index1, int index2, int index3)
    {
        if (quest[index1].isQuestCompleted)
        {
            questComleted[0] = quest[index1].isQuestCompleted;
        }
        if (quest[index2].isQuestCompleted)
        {
            questComleted[1] = quest[index2].isQuestCompleted;
        }
        if (quest[index3].isQuestCompleted)
        {
            questComleted[2] = quest[index3].isQuestCompleted;
        }
    }

    //Обновление базы данных
    private void ChangeItemDB(int level)
    {
        switch (level)
        {
            case 1:
                ChangeItem(0, 1, 2);
                return;
            case 2:
                ChangeItem(3, 4, 5);
                return;
            case 3:
                ChangeItem(6, 7, 8);
                return;
            case 4:
                ChangeItem(9, 10, 11);
                return;
            case 5:
                ChangeItem(12, 13, 14);
                return;
            case 6:
                ChangeItem(15, 16, 17);
                return;
            case 7:
                ChangeItem(18, 19, 20);
                return;
            case 8:
                ChangeItem(21, 22, 23);
                return;
            case 9:
                ChangeItem(24, 25, 26);
                return;
            case 10:
                ChangeItem(27, 28, 29);
                return;
            case 11:
                ChangeItem(30, 31, 32);
                return;
            case 12:
                ChangeItem(33, 34, 35);
                return;
            case 13:
                ChangeItem(36, 37, 38);
                return;
            case 14:
                ChangeItem(39, 40, 41);
                return;
            case 15:
                ChangeItem(42, 43, 44);
                return;
            case 16:
                ChangeItem(45, 46, 47);
                return;
            case 17:
                ChangeItem(48, 49, 50);
                return;
            case 18:
                ChangeItem(51, 52, 53);
                return;
            case 19:
                ChangeItem(54, 55, 56);
                return;
            case 20:
                ChangeItem(57, 58, 59);
                return;
            case 21:
                ChangeItem(60, 61, 62);
                return;
            case 22:
                ChangeItem(63, 64, 65);
                return;
            case 23:
                ChangeItem(66, 67, 68);
                return;
            case 24:
                ChangeItem(69, 70, 71);
                return;
            case 25:
                ChangeItem(72, 73, 74);
                return;
            case 26:
                ChangeItem(75, 76, 77);
                return;
            case 27:
                ChangeItem(78, 79, 80);
                return;
            case 28:
                ChangeItem(81, 82, 83);
                return;
            case 29:
                ChangeItem(84, 85, 86);
                return;
            default:
                Debug.Log("Default");
                return;
        }
    }

    private void ChangeItem(int index1, int index2, int index3)
    {
        if (quest[index1].itemCollected > itemCollected[0])
        {
            itemCollected[0] = quest[index1].itemCollected;
        }
        if (quest[index2].itemCollected > itemCollected[1])
        {
            itemCollected[1] = quest[index2].itemCollected;
        }
        if (quest[index3].itemCollected > itemCollected[2])
        {
            itemCollected[2] = quest[index3].itemCollected;
        }
    }

    //Повышение уровня
    public void LevelUP()
    {
        if (questComleted[0] && questComleted[1] && questComleted[2])
        {
            if (scoreManager.level < 30)
            {
                scoreManager.level += 1;
            }

            if (scoreManager.level == 30)
            {
                scoreManager.level = 30;
                randomQuest = Random.Range(0, 29);
            }
            else
            {
                questComleted[0] = false;
                questComleted[1] = false;
                questComleted[2] = false;

                itemCollected[0] = 0;
                itemCollected[1] = 0;
                itemCollected[2] = 0;
            }
        }
    }

    //Инициализация текста
    public void SetText(int level)
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

    private void SetQuestElements(int index1, int index2, int index3)
    {
        //Quest UI 1
        quest[index1].questCompletedImage = statusImage[0];
        quest[index1].questText = questsText[0];
        quest[index1].processText = statusText[0];
        //Quest 1
        quest[index1].itemCollected = itemCollected[0];
        quest[index1].itemNeeded = itemNeeded[0];
        quest[index1].questText.text = questText[index1];
        quest[index1].processText.text = quest[index1].itemCollected + "/" + quest[index1].itemNeeded;
        quest[index1].itemTag = questTag[index1];

        //Quest UI 2
        quest[index2].questCompletedImage = statusImage[1];
        quest[index2].questText = questsText[1];
        quest[index2].processText = statusText[1];
        //Quest 2
        quest[index2].itemCollected = itemCollected[1];
        quest[index2].itemNeeded = itemNeeded[1];
        quest[index2].questText.text = questText[index2];
        quest[index2].processText.text = quest[index2].itemCollected + "/" + quest[index2].itemNeeded;
        quest[index2].itemTag = questTag[index2];

        //Quest UI 3
        quest[index3].questCompletedImage = statusImage[2];
        quest[index3].questText = questsText[2];
        quest[index3].processText = statusText[2];
        //Quest 3
        quest[index3].itemCollected = itemCollected[2];
        quest[index3].itemNeeded = itemNeeded[2];
        quest[index3].questText.text = questText[index3];
        quest[index3].processText.text = quest[index3].itemCollected + "/" + quest[index3].itemNeeded;
        quest[index3].itemTag = questTag[index3];
    }

    //Инициализация цвета индикатора
    public void SetCheckerColor(int level) 
    {
        switch (level)
        {
            case 1:
                SetChecker(0, 1, 2);
                return;
            case 2:
                SetChecker(3, 4, 5);
                return;
            case 3:
                SetChecker(6, 7, 8);
                return;
            case 4:
                SetChecker(9, 10, 11);
                return;
            case 5:
                SetChecker(12, 13, 14);
                return;
            case 6:
                SetChecker(15, 16, 17);
                return;
            case 7:
                SetChecker(18, 19, 20);
                return;
            case 8:
                SetChecker(21, 22, 23);
                return;
            case 9:
                SetChecker(24, 25, 26);
                return;
            case 10:
                SetChecker(27, 28, 29);
                return;
            case 11:
                SetChecker(30, 31, 32);
                return;
            case 12:
                SetChecker(33, 34, 35);
                return;
            case 13:
                SetChecker(36, 37, 38);
                return;
            case 14:
                SetChecker(39, 40, 41);
                return;
            case 15:
                SetChecker(42, 43, 44);
                return;
            case 16:
                SetChecker(45, 46, 47);
                return;
            case 17:
                SetChecker(48, 49, 50);
                return;
            case 18:
                SetChecker(51, 52, 53);
                return;
            case 19:
                SetChecker(54, 55, 56);
                return;
            case 20:
                SetChecker(57, 58, 59);
                return;
            case 21:
                SetChecker(60, 61, 62);
                return;
            case 22:
                SetChecker(63, 64, 65);
                return;
            case 23:
                SetChecker(66, 67, 68);
                return;
            case 24:
                SetChecker(69, 70, 71);
                return;
            case 25:
                SetChecker(72, 73, 74);
                return;
            case 26:
                SetChecker(75, 76, 77);
                return;
            case 27:
                SetChecker(78, 79, 80);
                return;
            case 28:
                SetChecker(81, 82, 83);
                return;
            case 29:
                SetChecker(84, 85, 86);
                return;
            default:
                Debug.Log("Default");
                return;
        }
    }

    private void SetChecker(int inde1, int index2, int index3)
    {
        quest[inde1].isQuestCompleted = questComleted[0];
        quest[index2].isQuestCompleted = questComleted[1];
        quest[index3].isQuestCompleted = questComleted[2];

        SetColor(quest[inde1].isQuestCompleted, quest[inde1].questCompletedImage);
        SetColor(quest[index2].isQuestCompleted, quest[index2].questCompletedImage);
        SetColor(quest[index3].isQuestCompleted, quest[index3].questCompletedImage);
    }

    //Установка цвета индикатора
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
}
