using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoneyScript : MonoBehaviour
{
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private int moneyToAdd;
    
    public void SetMoneyToAdd(int _money)
    {
        moneyToAdd = _money;
    }
    
    public void SetMoneyManager(MoneyManager _mm)
    {
        moneyManager = _mm;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            Destroy(gameObject);
            moneyManager.ChangeMoney(moneyToAdd);
            Debug.Log("Add Money");
        }
    }
}
