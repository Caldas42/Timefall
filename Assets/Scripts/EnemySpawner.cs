using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    // Array com os prefabs dos inimigos: 0 = Enemy, 1 = EnemyFast, 2 = EnemyTank
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    // Quantos inimigos serão instanciados por segundo
    [SerializeField] private float enemiesPerSecond = 0.25f;

    // Tempo (em segundos) entre uma wave e outra
    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private bool isSpawning = false;

    // Lista com a ordem de inimigos a serem spawnados
    private List<int> enemiesToSpawn = new List<int>();
    private int totalEnemiesAlive;

    private void Awake()
    {
        // Registra o método EnemyDestroyed no evento global
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        // Contador de tempo para o próximo spawn
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond))
        {
            bool spawned = TrySpawnEnemy();
            if (spawned)
            {
                timeSinceLastSpawn = 0f;
            }
        }

        // Agora, a próxima wave começa mesmo que ainda existam inimigos vivos
        if (enemiesToSpawn.Count == 0 && isSpawning)
        {
            EndWave();
        }
    }

    // Reduz o contador de inimigos vivos quando um inimigo é destruído
    private void EnemyDestroyed()
    {
        totalEnemiesAlive--;
    }

    // Inicia uma nova wave com tempo dinâmico (pode ser alterado em tempo real)
    private IEnumerator StartWave()
    {
        float timer = 0f;

        // Espera o tempo entre as waves, permitindo que seja alterado em tempo real
        while (timer < timeBetweenWaves)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        isSpawning = true;
        SetupWave(currentWave);
    }

    // Finaliza a wave atual e agenda a próxima
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    // Define os inimigos que serão spawnados em cada wave
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
                Shuffle(enemiesToSpawn); // Embaralha a ordem
                break;
            case 3:
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0); // Enemy
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(1); // EnemyFast
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(2); // EnemyTank
                Shuffle(enemiesToSpawn);
                break;
            default:
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(0);
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(1);
                for (int i = 0; i < 12; i++) enemiesToSpawn.Add(2);
                Shuffle(enemiesToSpawn);
                break;
        }

        totalEnemiesAlive = enemiesToSpawn.Count;
    }

    // Tenta spawnar o próximo inimigo da fila
    private bool TrySpawnEnemy()
    {
        if (enemiesToSpawn.Count == 0) return false;

        int enemyType = enemiesToSpawn[0];
        enemiesToSpawn.RemoveAt(0);

        Spawn(enemyPrefabs[enemyType]);
        return true;
    }

    // Instancia o inimigo no ponto inicial
    private void Spawn(GameObject prefab)
    {
        Instantiate(prefab, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    // Embaralha a lista de inimigos usando o algoritmo Fisher-Yates
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
