using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemyIA[] enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxEnemiesNumber;
    [SerializeField] private Player playerPos;

    private List<EnemyIA> spawnedEnemies = new List<EnemyIA>();
    private float timeSinceLastSpawn;

    private void Start()
    {
        timeSinceLastSpawn = spawnInterval;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawnInterval)
        {
            timeSinceLastSpawn = 0f;
            if (spawnedEnemies.Count < maxEnemiesNumber)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        EnemyIA enemy = Instantiate(enemyPrefab[UnityEngine.Random.Range(0, enemyPrefab.Length)], transform.position, transform.rotation);
        int spawnPointIndex = spawnedEnemies.Count % spawnPoints.Length;
        enemy.Init(playerPos, spawnPoints[spawnPointIndex]);
        spawnedEnemies.Add(enemy);
    }
}
