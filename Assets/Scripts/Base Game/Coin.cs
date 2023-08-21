using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text crystalText;

    public int coin;
    public int crystal;
    public int diamond;

    public void Initialize()
    {
        coinText.text = "Монеток: " + coin;
        crystalText.text = "Кристалов: " + crystal;
    }

    public void AddCoin(bool doubleCoin)
    {
        if (doubleCoin)
        {
            coin += 2;
            coinText.text = "Монеток: " + coin;
        }
        else
        {
            coin++;
            coinText.text = "Монеток: " + coin;
        }
    }
}
