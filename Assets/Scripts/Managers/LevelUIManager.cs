using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    [Header("Счеткчик очков и коинов")]
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject coinText;

    [Space(6)]
    [Header("Панель проигрыша")]
    [SerializeField] private int costRestartGame;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button restartButton;
    [SerializeField] private Text restartText;
    [SerializeField] private Button backToMenuButton;

    [Space(6)]
    [Header("Панель паузы")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button backMenuPauseButton;
    [SerializeField] private Button audioButton;
    [SerializeField] private Button soundButton;

    private PlayerController player;
    private Coin coinManager;

    public void Initialize()
    {
        AudioManager.Instance.PaintingButtons(audioButton, soundButton);

        player = FindObjectOfType<PlayerController>();
        coinManager = FindObjectOfType<Coin>();

        ButtonFunc();
    }

    private void ButtonFunc()
    {
        #region PausePanel
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton.onClick.AddListener(() =>
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
            });
        }

        if (continueButton != null)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            });
        }

        if (backMenuPauseButton != null)
        {
            backMenuPauseButton.onClick.RemoveAllListeners();
            backMenuPauseButton.onClick.AddListener(() =>
            {
                //player.Save();
                SceneManager.LoadScene("MainMenu");
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
        #endregion

        #region LosePanel
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() =>
            {
                RestartGame(player.barrier);
            });
        }

        if (backToMenuButton != null)
        {
            backToMenuButton.onClick.RemoveAllListeners();
            backToMenuButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        }
        #endregion
    }

    private void RestartGame(GameObject barrier)
    {
        if (coinManager.crystal >= costRestartGame)
        {
            coinManager.crystal -= costRestartGame;
            costRestartGame += 10;
            coinManager.Initialize();
            Destroy(barrier);
            Time.timeScale = 1;
            scoreText.SetActive(true);
            coinText.SetActive(true);
            pausePanel.SetActive(false);
            losePanel.SetActive(false);
            Debug.Log("Games restarted");
        }
        else
        {
            Debug.Log("Недостаточно средств");
        }
    }

    public void LoseGame()
    {
        restartText.text = costRestartGame + " кристалов";
        Time.timeScale = 0;
        scoreText.SetActive(false);
        coinText.SetActive(false);
        pausePanel.SetActive(false);
        losePanel.SetActive(true);
    }
}
