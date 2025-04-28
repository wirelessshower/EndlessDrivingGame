using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text MoneyText;
    private int currentMoney;

    void Start()
    {
        UpdateMoney();
        PlayerPrefs.SetInt("Money", 10000);
    }

    public void UpdateMoney()
    {
        currentMoney = PlayerPrefs.GetInt("Money");
        MoneyText.text = currentMoney.ToString();
    }
    
}
