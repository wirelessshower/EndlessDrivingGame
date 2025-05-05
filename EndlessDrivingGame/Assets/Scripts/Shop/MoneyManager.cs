using System;
using TMPro;
using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;
public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text MoneyText;
    public static int currentMoney;
    private int maxMoney = 0;

    void Start()
    {
        UpdateMoney();        
    }

    void Update()
    {
        if(currentMoney > maxMoney){
            maxMoney = currentMoney;
            YG2.SetLeaderboard("CarCoinsLeaderBoard", maxMoney);
        }
    }

    public void UpdateMoney()
    {
        currentMoney = PlayerPrefs.GetInt("Money");
        MoneyText.text = currentMoney.ToString();
    }

    public void AddMoney(int amount) {
        int money = PlayerPrefs.GetInt("Money");
        money += amount;
        PlayerPrefs.SetInt("Money", money);
        UpdateMoney();
    }
    
}
