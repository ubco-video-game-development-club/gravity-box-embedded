using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private Transform spawnPointPrefab;
    [SerializeField] private float spawnEdgeOffset;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float baseSpawnInterval = 2f;
    [SerializeField] private float spawnIntervalDecrement = 0.05f;
    [SerializeField] private float baseSpawnCount = 1.5f;
    [SerializeField] private float spawnCountIncrement = 0.5f;
    [SerializeField] private int startDelay = 3;
    [SerializeField] private int waveCutoffDuration = 3;

    public int WaveTimer
    {
        get { return _waveTimer; }
        set
        {
            _waveTimer = value;
            if (_waveTimer == 0)
            {
                StartCoroutine(SpawnWave());
            }

            if (tickCoroutine != null)
            {
                StopCoroutine(tickCoroutine);
            }
            if (_waveTimer > 0)
            {
                tickCoroutine = StartCoroutine(TickWaveTimer());
            }
        }
    }
    private int _waveTimer;

    private Transform[] spawnPoints = new Transform[4];

    private float spawnInterval = 0;
    private int spawnCount = 0;
    private float rawSpawnCount = 0;
    private int enemiesRemaining = 0;

    private YieldInstruction spawnInstruction;
    private YieldInstruction tickInstruction;
    private Coroutine tickCoroutine;

    void Awake()
    {
        spawnInterval = baseSpawnInterval;
        rawSpawnCount = baseSpawnCount;
        spawnInstruction = new WaitForSeconds(spawnInterval);
        spawnCount = Mathf.FloorToInt(rawSpawnCount);
        tickInstruction = new WaitForSeconds(1);

        for (int i = 0; i < 4; i++)
        {
            spawnPoints[i] = Instantiate(spawnPointPrefab);
        }
    }

    void Start()
    {
        WaveTimer = startDelay;
    }

    void Update()
    {
        UpdateSpawnPoints();
    }

    public void RemoveEnemy()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            WaveTimer = waveCutoffDuration;
        }
    }

    private IEnumerator TickWaveTimer()
    {
        yield return new WaitForSeconds(1);
        WaveTimer--;
    }

    private IEnumerator SpawnWave()
    {
        enemiesRemaining += 4 * spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                enemy.AddDeathListener(RemoveEnemy);
            }

            yield return spawnInstruction;
        }

        UpdateWave();
    }

    private void UpdateWave()
    {
        // Increase difficulty
        spawnInterval = Mathf.Clamp(spawnInterval - spawnIntervalDecrement, 0.1f, baseSpawnInterval);
        rawSpawnCount += spawnCountIncrement;

        // Update wave data
        spawnInstruction = new WaitForSeconds(spawnInterval);
        spawnCount = Mathf.FloorToInt(rawSpawnCount);
    }

    private void UpdateSpawnPoints()
    {
        Vector2 topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        spawnPoints[0].position = topLeft + new Vector2(spawnEdgeOffset, -spawnEdgeOffset);
        spawnPoints[1].position = topRight + new Vector2(-spawnEdgeOffset, -spawnEdgeOffset);
        spawnPoints[2].position = bottomLeft + new Vector2(spawnEdgeOffset, spawnEdgeOffset);
        spawnPoints[3].position = bottomRight + new Vector2(-spawnEdgeOffset, spawnEdgeOffset);
    }
}
