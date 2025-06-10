using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private float enemiesPerSecond = 0.25f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int maxWaves = 9;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveCounter;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private bool isSpawning = false;

    private List<int> enemiesToSpawn = new List<int>();
    private int totalEnemiesAlive;

    private int normalEnemyCount = 10;
    private int fastEnemyCount = 10;
    private int tankEnemyCount = 12;

    private void Start()
{
    Time.timeScale = 0f;

}

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    public void OnPlayButtonPressed()
{
    if (!isSpawning && totalEnemiesAlive <= 0)
    {
        Time.timeScale = 1f;
        StartCoroutine(StartWave());
    }
}

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond))
        {
            bool spawned = TrySpawnEnemy();
            if (spawned)
            {
                timeSinceLastSpawn = 0f;
            }
        }

        if (enemiesToSpawn.Count == 0 && isSpawning)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        totalEnemiesAlive--;

        if (enemiesToSpawn.Count == 0 && totalEnemiesAlive <= 0)
        {
            if (currentWave >= maxWaves)
            {
                LevelManager.main.Victory();
            }
            else
            {
                currentWave++;
                Time.timeScale = 0f;
            }
        }
    }

    private IEnumerator StartWave()
    {
        float timer = 0f;
        while (timer < timeBetweenWaves)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SetupWave(currentWave);
        UpdateWaveUI();
        
        isSpawning = true;
        timeSinceLastSpawn = 0f;
    }

    private void EndWave()
    {
        isSpawning = false;
    }


    private void SetupWave(int wave)
    {
        enemiesToSpawn.Clear();

        if (wave == maxWaves)
        {
            enemiesToSpawn.Add(3);
        }
        else
        {
            switch (wave)
            {
                case 1:
                    for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0);
                    break;
                case 2:
                    for (int i = 0; i < 10; i++) enemiesToSpawn.Add(0);
                    for (int i = 0; i < 5; i++) enemiesToSpawn.Add(1);
                    break;
                case 3:
                    for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0);
                    for (int i = 0; i < 8; i++) enemiesToSpawn.Add(1);
                    for (int i = 0; i < 8; i++) enemiesToSpawn.Add(2);
                    break;
                default:
                    int extra = wave - 3;
                    int normalCount = normalEnemyCount + extra * 2;
                    int fastCount = fastEnemyCount + extra * 2;
                    int tankCount = tankEnemyCount + extra * 2;

                    for (int i = 0; i < normalCount; i++) enemiesToSpawn.Add(0);
                    for (int i = 0; i < fastCount; i++) enemiesToSpawn.Add(1);
                    for (int i = 0; i < tankCount; i++) enemiesToSpawn.Add(2);
                    break;
            }
        }
    }

    private bool TrySpawnEnemy()
    {
        if (enemiesToSpawn.Count == 0) return false;

        int enemyType = enemiesToSpawn[0];
        enemiesToSpawn.RemoveAt(0);

        Spawn(enemyPrefabs[enemyType]);
        return true;
    }

    private void Spawn(GameObject prefab)
    {
        Instantiate(prefab, LevelManager.main.startPoint.position, Quaternion.identity);
        totalEnemiesAlive++;
    }


    private void UpdateWaveUI()
    {
        waveCounter.text = currentWave + "/" + maxWaves;
    }
}
