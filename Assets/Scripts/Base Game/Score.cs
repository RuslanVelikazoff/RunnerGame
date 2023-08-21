using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [HideInInspector] public int level = 1;
    [HideInInspector] public int score;
    [Range(1,35)] public int scoresAdd;
    [HideInInspector] public int highScore;

    private PlayerController player;
    private QuestManager questManager;

    public void Initialize()
    {
        player = FindObjectOfType<PlayerController>();
        questManager = FindObjectOfType<QuestManager>();
        if (level == 1)
        {
            scoresAdd = 1;
        }
        else
        {
            scoresAdd = level / 2;
        }
    }

    private void FixedUpdate()
    {
        if (!player.x2Score)
        {
            score += scoresAdd;
            if (score > highScore)
            {
                highScore = score;
            }
            scoreText.text = score.ToString();
            if (level < 30)
            {
                questManager.Quest("Score", level);
            }
            else
            {
                questManager.Quest("Score", questManager.randomQuest);
            }
        }
        else
        {
            score += scoresAdd * 2;
            if (score > highScore)
            {
                highScore = score;
            }
            scoreText.text = score.ToString();
            if (level < 30)
            {
                questManager.Quest("Score", level);
            }
            else
            {
                questManager.Quest("Score", questManager.randomQuest);
            }
        }
    }
}
