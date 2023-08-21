using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Space(7)]
    [Header("Игрок")]
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController _camera;
    [SerializeField] private BonusManager bonusManager;

    [Space(7)]
    [Header("Уровень")]
    [SerializeField] private TileGenerator tileGenerator;
    [SerializeField] private Coin coinManager;
    [SerializeField] private Score scoreManager;
    [SerializeField] private CameraScallerController cameraScallerController;

    [Space(7)]
    [Header("UI")]
    [SerializeField] private LevelUIManager levelUIManager;
    [SerializeField] private QuestManager questManager;


    private void Start()
    {
        levelUIManager.Initialize();
        tileGenerator.Initialize();
        player.Initialize();
        bonusManager.Initialize();
        _camera.Initialize();
        coinManager.Initialize();
        scoreManager.Initialize();
        questManager.Initialize();
        cameraScallerController.Initialize();
    }
}
