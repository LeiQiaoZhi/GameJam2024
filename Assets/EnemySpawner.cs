using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<EnemyInfo> enemyInfos;
    [SerializeField] private float baseCheckInterval;
    [Range(0,1)]
    [SerializeField] private float spawnThreshold;
    [Header("Time function, value=a+bt")]
    [SerializeField] private float a;
    [SerializeField] private float b;

    private float startTime_;
    private List<EnemyInfo> spawnedEnemies_;

    private Transform GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    private float GetCurrentTotalValue()
    {
        float totalValue = 0;
        foreach (EnemyInfo enemyInfo in enemyInfos)
        {
            totalValue += enemyInfo.enemySpawnValue;
        }
        return totalValue;
    }

    private float ValueTimeFunction(float _time)
    {
        float x = _time - startTime_;
        return a + b * x;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemyLoop());
        startTime_ = Time.time;
    }

    private IEnumerator SpawnEnemyLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(baseCheckInterval);
            // check value
            var totalValue = GetCurrentTotalValue();
            var targetValue = ValueTimeFunction(Time.time);
            if (totalValue <= targetValue * spawnThreshold)
            {
                
            }
        }
        
    }
}

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "EnemyInfo")]
public class EnemyInfo : ScriptableObject
{
    public GameObject enemyPrefab;
    public float enemySpawnValue;
    public float enemyCoinReward;

    public EnemyInfo DeepCopy(EnemyInfo _enemyInfo)
    {
        var newEnemyInfo = CreateInstance<EnemyInfo>();
        newEnemyInfo.enemyPrefab = _enemyInfo.enemyPrefab;
        newEnemyInfo.enemySpawnValue = _enemyInfo.enemySpawnValue;
        newEnemyInfo.enemyCoinReward = _enemyInfo.enemyCoinReward;
        return newEnemyInfo;   
    }
}
