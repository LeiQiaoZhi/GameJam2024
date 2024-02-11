using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int initialMoney;

    private int currentMoney_;
    
    public delegate void OnMoneyChange(int _newMoney);
    public OnMoneyChange onMoneyChange;
    public static MoneyManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentMoney_ = initialMoney;
        onMoneyChange?.Invoke(currentMoney_);
    }

    public void ChangeMoney(int _change)
    {
        currentMoney_ += _change;
        onMoneyChange?.Invoke(currentMoney_);
    }

    public int GetMoney()
    {
        return currentMoney_;
    }
}