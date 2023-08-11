using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] TMP_Text coinsAmount;
 
    private int coins;

    public int Coins { get { return coins; } private set { coins = value; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        coinsAmount.SetText(Coins.ToString());
    }

    public void IncreaseCoins(int amount)
    {
        Coins += amount;
        coinsAmount.SetText(Coins.ToString());
    }
}
