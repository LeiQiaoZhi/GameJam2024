using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int initialMoney;

    private int currentMoney_;

    private void Start()
    {
        currentMoney_ = initialMoney;
    }

    public void ChangeMoney(int _change)
    {
        currentMoney_ += _change;
    }

    public int GetMoney()
    {
        return currentMoney_;
    }
}