using UnityEngine;

public class EnemyDamagable : Damagable
{
    public EnemyInfo enemyInfo;
    public GameObject coinPrefab;
    public float coinSpawnRandomness;

    protected override void Death()
    {
        base.Death();
        var amount = enemyInfo.enemyCoinReward;
        for (int i = 0; i < amount; i++)
        {
            GameObject coinGO = Instantiate(coinPrefab);
            Vector2 randomOffset = Random.insideUnitCircle * coinSpawnRandomness;
            coinGO.transform.position = transform.position + new Vector3(randomOffset.x, 0, randomOffset.y);
            var money = coinGO.GetComponent<AddMoneyScript>();
            money.SetMoneyManager(MoneyManager.Instance);
            money.SetMoneyToAdd(10);
        }
    }
}