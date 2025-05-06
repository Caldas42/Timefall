//bibliotecas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    //Array que vai de 0 a 3 sendo 0 = Enemy, 1 = EnemyFast, 2 = EnemyTank
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    //Inimigos por segundo
    [SerializeField] private float enemiesPerSecond = 0.25f;
    //Tempo entre hordas
    [SerializeField] private float timeBetweenWaves = 0f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;

    private bool isSpawning = false;

    // ✅ Nova lista que contém a ordem embaralhada dos inimigos para a wave
    private List<int> enemiesToSpawn = new List<int>();
    private int totalEnemiesAlive;

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

        if (enemiesToSpawn.Count == 0 && totalEnemiesAlive == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        totalEnemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;

        SetupWave(currentWave);
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    // ✅ Define e embaralha os inimigos das waves 2 e 3
    private void SetupWave(int wave)
    {
        enemiesToSpawn.Clear();

        switch (wave)
        {
            case 1:
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0); // Só Enemy
                break;
            case 2:
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(0); // Enemy
                for (int i = 0; i < 5; i++) enemiesToSpawn.Add(1); // EnemyFast
                Shuffle(enemiesToSpawn); // ✅ Embaralha a ordem
                break;
            case 3:
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0); // Enemy
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(1); // EnemyFast
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(2); // EnemyTank
                Shuffle(enemiesToSpawn); // ✅ Embaralha a ordem
                break;
            default:
                // Para waves posteriores, mantém aleatoriedade com mais inimigos
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(0);
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(1);
                for (int i = 0; i < 12; i++) enemiesToSpawn.Add(2);
                Shuffle(enemiesToSpawn);
                break;
        }

        totalEnemiesAlive = enemiesToSpawn.Count;
    }

    // ✅ Spawna o próximo inimigo com base na lista embaralhada
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
    }

    // ✅ Fisher-Yates Shuffle
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
}