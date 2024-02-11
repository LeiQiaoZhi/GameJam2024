using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyTextUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public MoneyManager moneyManager;

    private void OnEnable()
    {
        moneyManager.onMoneyChange += UpdateMoneyText;
    }

    private void OnDisable()
    {
        moneyManager.onMoneyChange -= UpdateMoneyText;
    }

    private void UpdateMoneyText(int _newmoney)
    {
        text.text = _newmoney.ToString();
    }
}
