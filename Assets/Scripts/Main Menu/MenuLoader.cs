using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    [SerializeField] private StartData startData;
    [SerializeField] private MenuButtonManager buttonManager;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private CameraScallerController cameraScallerController;

    private void Start()
    {
        startData.Initialize();
        buttonManager.Initialize();
        shopManager.InitializeQuestMenu();
        shopManager.InitializeSkinShop();
        shopManager.InitializeBonusShop();
        cameraScallerController.Initialize();
    }
}
