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
    [SerializeField] private int maxWaves = 5;

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

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
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

        if (currentWave >= maxWaves && totalEnemiesAlive <= 0)
        {
            Debug.Log("Vitória! Todas as waves concluídas e inimigos derrotados.");
            LevelManager.main.Victory();
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

        isSpawning = true;
        SetupWave(currentWave);
        UpdateWaveUI();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;

        if (currentWave >= maxWaves)
        {
            Debug.Log("Todas as waves concluídas. Fim do spawn de inimigos.");
            UpdateWaveUI();
            return;
        }

        currentWave++;
        UpdateWaveUI();
        StartCoroutine(StartWave());
    }

    private void SetupWave(int wave)
    {
        enemiesToSpawn.Clear();

        switch (wave)
        {
            case 1:
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0);
                break;
            case 2:
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(0);
                for (int i = 0; i < 5; i++) enemiesToSpawn.Add(1);
                Shuffle(enemiesToSpawn);
                break;
            case 3:
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0);
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(1);
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(2);
                Shuffle(enemiesToSpawn);
                break;
            case 5:
                enemiesToSpawn.Add(3); // Ex: Boss
                break;
            default:
                int extra = wave - 3;
                int normalCount = normalEnemyCount + extra * 2;
                int fastCount = fastEnemyCount + extra * 2;
                int tankCount = tankEnemyCount + extra * 2;

                for (int i = 0; i < normalCount; i++) enemiesToSpawn.Add(0);
                for (int i = 0; i < fastCount; i++) enemiesToSpawn.Add(1);
                for (int i = 0; i < tankCount; i++) enemiesToSpawn.Add(2);
                Shuffle(enemiesToSpawn);
                break;
        }

        // Não seta mais totalEnemiesAlive aqui — corrigido
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
        totalEnemiesAlive++; // Corrigido: conta inimigos vivos no momento do spawn
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void UpdateWaveUI()
    {
        waveCounter.text = currentWave + "/" + maxWaves;
    }
}
