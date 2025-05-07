using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour      // Classe que gerencia o spawn de inimigos
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;  // Array com os prefabs dos inimigos: 0 = Enemy, 1 = EnemyFast, 2 = EnemyTank

    [Header("Attributes")]
    [SerializeField] private float enemiesPerSecond = 0.25f;  // Quantos inimigos serão spawnados por segundo
    [SerializeField] private float timeBetweenWaves = 5f;     // Tempo entre as waves

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();  // Evento chamado quando um inimigo é destruído

    private int currentWave = 1;                // Número da wave atual
    private float timeSinceLastSpawn;           // Tempo desde o último inimigo spawnado
    private bool isSpawning = false;            // Flag indicando se a wave atual está ativa

    private List<int> enemiesToSpawn = new List<int>(); // Lista com os tipos de inimigos a serem spawnados
    private int totalEnemiesAlive;              // Contador de inimigos vivos na cena

    private void Awake()
    {
        // Registra o método EnemyDestroyed como ouvinte do evento onEnemyDestroy
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        // Inicia a primeira wave
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        // Se não estamos em uma wave, sai do Update
        if (!isSpawning) return;

        // Incrementa tempo desde o último spawn
        timeSinceLastSpawn += Time.deltaTime; 

        // Se passou o tempo suficiente, tenta spawnar um inimigo
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond))
        {
            bool spawned = TrySpawnEnemy();  // Tenta spawnar
            if (spawned)
            {
                // Reinicia o contador de tempo
                timeSinceLastSpawn = 0f;    
            }
        }

        // Se não há mais inimigos a serem spawnados, encerra a wave (mesmo que alguns ainda estejam vivos)
        if (enemiesToSpawn.Count == 0 && isSpawning)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        // Diminui o número de inimigos vivos
        totalEnemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        float timer = 0f;

        // Aguarda o tempo entre as waves
        while (timer < timeBetweenWaves)
        {
            timer += Time.deltaTime;
            yield return null; // Espera até o próximo frame
        }

        // Ativa o spawn
        isSpawning = true; 
        // Prepara a wave com base no número atual que está
        SetupWave(currentWave); 
    }

    // Desativa o spawn atual
    private void EndWave()
    {
        isSpawning = false;      
        // Reinicia tempo desde último spawn
        timeSinceLastSpawn = 0f;    
        // Avança para a próxima wave
        currentWave++;              
        StartCoroutine(StartWave()); // Começa nova wave com intervalo
    }

    private void SetupWave(int wave)
    {
        // Limpa lista de inimigos anteriores
        enemiesToSpawn.Clear(); 

        switch (wave)
        {
            case 1:
                // 8 inimigos normais (Enemy)
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0);
                break;
            case 2:
                // 10 inimigos normais + 5 rápidos (EnemyFast)
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(0);
                for (int i = 0; i < 5; i++) enemiesToSpawn.Add(1);
                Shuffle(enemiesToSpawn); // Embaralha os tipos
                break;
            case 3:
                // 8 normais + 8 rápidos + 8 tanques
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(0);
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(1);
                for (int i = 0; i < 8; i++) enemiesToSpawn.Add(2);
                Shuffle(enemiesToSpawn);
                break;
            default:
                // A partir da wave 4: mais inimigos de todos os tipos
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(0);
                for (int i = 0; i < 10; i++) enemiesToSpawn.Add(1);
                for (int i = 0; i < 12; i++) enemiesToSpawn.Add(2);
                Shuffle(enemiesToSpawn);
                break;
        }

        totalEnemiesAlive = enemiesToSpawn.Count; // Define quantos inimigos precisam morrer
    }

    private bool TrySpawnEnemy()
    {
        //Verifica se a lista tá vazia, se estiver ,não spawna
        if (enemiesToSpawn.Count == 0) return false;

        // Pega o tipo do próximo inimigo
        int enemyType = enemiesToSpawn[0];
        // Remove ele da lista     
        enemiesToSpawn.RemoveAt(0);                  

        Spawn(enemyPrefabs[enemyType]);              // Instancia o inimigo usando o prefab correspondente
        return true;
    }

    private void Spawn(GameObject prefab)
    {
        // Instancia o inimigo na posição de início do LevelManager
        Instantiate(prefab, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    //para embaralhar a lista de inimigos
    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            // Gera índice aleatório
            int randomIndex = Random.Range(i, list.Count); 
            // Troca os elementos
            int temp = list[i];                            
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
