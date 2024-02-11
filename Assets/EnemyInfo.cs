using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public GameObject enemyPrefab;
    public float enemySpawnValue;
    public int enemyCoinReward;

    public EnemyInfo DeepCopy(EnemyInfo _enemyInfo)
    {
        var newEnemyInfo = CreateInstance<EnemyInfo>();
        newEnemyInfo.enemyPrefab = _enemyInfo.enemyPrefab;
        newEnemyInfo.enemySpawnValue = _enemyInfo.enemySpawnValue;
        newEnemyInfo.enemyCoinReward = _enemyInfo.enemyCoinReward;
        return newEnemyInfo;   
    }
}