using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private float enemiesPerSecond = 0.25f;
    [SerializeField] private int maxWaves = 9;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveCounter;
    [SerializeField] private Toggle autoStartToggle;
    [SerializeField] private Image startWaveButtonImage;
    [SerializeField] private Sprite startWaveButtonOnSprite;
    [SerializeField] private Sprite startWaveButtonOffSprite;
    [SerializeField] private Image autoStartToggleImage;
    [SerializeField] private Sprite autoStartToggleOnSprite;
    [SerializeField] private Sprite autoStartToggleOffSprite;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private bool isSpawning = false;
    private bool isInitializingToggle = false;

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
        if (LevelManager.main.GetRemainingLives() <= 0)
        {
            Time.timeScale = 0f;
            LevelManager.main.getLevelController().OpenGameOverPanel();
            return;
        }

        Time.timeScale = 0f;

        autoStartToggle.onValueChanged.RemoveListener(OnAutoStartToggleChanged);

        bool autoStartEnabled = PlayerPrefs.GetInt("AutoStartEnabled", 0) == 1;

        autoStartToggle.isOn = autoStartEnabled;

        if (autoStartEnabled)
        {
            autoStartToggleImage.sprite = autoStartToggleOnSprite;
        }
        else
        {
            autoStartToggleImage.sprite = autoStartToggleOffSprite;
        }

        autoStartToggle.onValueChanged.AddListener(OnAutoStartToggleChanged);


        if (autoStartEnabled && currentWave > 1)
        {
            StartCoroutine(StartAutoSpawn());
        }
        
        UpdatePlayButtonSprite();
    }

    private void Update()
    {
        if (!isSpawning && currentWave > 1 && autoStartToggle != null && autoStartToggle.isOn && totalEnemiesAlive <= 0)
        {
            Time.timeScale = FindAnyObjectByType<LevelController>().GetGameSpeed();
            StartCoroutine(StartWave());
        }

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

    public void OnPlayButtonPressed()
    {
        if (!isSpawning && totalEnemiesAlive <= 0)
        {
            Time.timeScale = FindAnyObjectByType<LevelController>().GetGameSpeed();
            StartCoroutine(StartWave());
        }
    }

    private void UpdatePlayButtonSprite()
    {
        startWaveButtonImage.sprite = isSpawning
            ? startWaveButtonOffSprite 
            : startWaveButtonOnSprite;
    }

    private IEnumerator StartWave()
    {
        SetupWave(currentWave);
        UpdateWaveUI();
        isSpawning = true;
        UpdatePlayButtonSprite();
        timeSinceLastSpawn = 0f;
        yield return null;
    }

    private IEnumerator StartAutoSpawn()
    {
        yield return new WaitForSeconds(0f);
        Time.timeScale = FindAnyObjectByType<LevelController>().GetGameSpeed();
        StartCoroutine(StartWave());
    }

    public void OnAutoStartToggleChanged(bool isOn)
    {
        if (isInitializingToggle) return;

        PlayerPrefs.SetInt("AutoStartEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();

        if (isOn)
        {
            autoStartToggleImage.sprite = autoStartToggleOnSprite;
        }
        else
        {
            autoStartToggleImage.sprite = autoStartToggleOffSprite;
        }

        if (isOn && currentWave > 1 && !isSpawning && totalEnemiesAlive <= 0)
        {
            StartCoroutine(StartAutoSpawn());
        }
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

    private void EndWave()
    {
        isSpawning = false;
        UpdatePlayButtonSprite();
    }

    private void UpdateWaveUI()
    {
        waveCounter.text = currentWave + "/" + maxWaves;
    }
}
