using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Transform spawnPointPrefab;
    [SerializeField] private float spawnEdgeOffset;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float minSpawnDelay = 2f;
    [SerializeField] private float maxSpawnDelay = 5f;

    private Transform[] spawnPoints = new Transform[6];
    private int spawnIndex = 0;

    void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = Instantiate(spawnPointPrefab);
            StartCoroutine(SpawnEnemy());
        }
    }

    void Update()
    {
        UpdateSpawnPoints();
    }

    private void RespawnEnemy()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(spawnDelay);

        Transform spawnPoint = spawnPoints[spawnIndex];
        spawnIndex = (spawnIndex + 1) % spawnPoints.Length;

        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.AddDeathListener(RespawnEnemy);
    }

    private void UpdateSpawnPoints()
    {
        Vector2 topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        Vector2 topMiddle = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2f, Screen.height));
        Vector2 bottomMiddle = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2f, 0));
        spawnPoints[0].position = topLeft + new Vector2(spawnEdgeOffset, -spawnEdgeOffset);
        spawnPoints[1].position = topRight + new Vector2(-spawnEdgeOffset, -spawnEdgeOffset);
        spawnPoints[2].position = bottomLeft + new Vector2(spawnEdgeOffset, spawnEdgeOffset);
        spawnPoints[3].position = bottomRight + new Vector2(-spawnEdgeOffset, spawnEdgeOffset);
        spawnPoints[4].position = topMiddle + new Vector2(0, -spawnEdgeOffset);
        spawnPoints[5].position = bottomMiddle + new Vector2(0, spawnEdgeOffset);
    }
}
