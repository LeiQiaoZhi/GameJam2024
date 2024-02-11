using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Collider playerCollider;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<EnemyInfo> enemyInfos;
    [SerializeField] private float baseCheckInterval;
    [Range(0, 1)] [SerializeField] private float spawnThreshold;

    [Header("Time function, value=a+bt")] [SerializeField]
    private float a;
    [SerializeField] private float b;

    private float currentScore_;
    private float currentTarget_;

    private float startTime_;
    private List<EnemyInfoTracker> spawnedEnemies_ = new List<EnemyInfoTracker>();

    private Transform GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    private float GetCurrentTotalValue()
    {
        float totalValue = 0;
        foreach (var tracker in spawnedEnemies_)
        {
            if (tracker == null) continue;
            totalValue += tracker.enemyInfo.enemySpawnValue;
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
            // check value
            currentScore_ = GetCurrentTotalValue();
            currentTarget_ = ValueTimeFunction(Time.time);
            if (currentScore_ <= currentTarget_ * spawnThreshold)
            {
                while (currentScore_ <= currentTarget_)
                {
                    var enemyInfo = enemyInfos[Random.Range(0, enemyInfos.Count)];
                    currentScore_ += enemyInfo.enemySpawnValue;
                    var spawnPoint = GetSpawnPoint();
                    var enemy = Instantiate(enemyInfo.enemyPrefab, spawnPoint.position, quaternion.identity);
                    enemy.AddComponent<EnemyInfoTracker>().enemyInfo = enemyInfo;
                    enemy.GetComponentInChildren<SimpleTargetSelector>().playerTagert = playerCollider;
                    spawnedEnemies_.Add(enemy.GetComponent<EnemyInfoTracker>());
                    enemy.transform.rotation = Quaternion.identity;
                }
                
            }
            yield return new WaitForSeconds(baseCheckInterval);
        }
    }
}