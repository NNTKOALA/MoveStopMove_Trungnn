using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> levelPrefab;
    public int currentLevel;
    private GameObject currentLevelInstance;

    public delegate Bot OnSpawnEnemy();
    public static event OnSpawnEnemy onEnemySpawn;

    [SerializeField] Player player;
    public Player MainPlayer => player;

    [SerializeField] int initialAmountEnemy = 5;
    [SerializeField] float spawnCooldown = 5f;
    [SerializeField] int maxAmountPerSpawn = 8;
    [SerializeField] int minAmountPerSpawn = 3;

    [SerializeField] private List<Bot> enemiesOnScreen = new List<Bot>();
    private float spawnTimer;
    private bool isPlaying;

    [SerializeField] private int playerKillCount;
    public int PlayerKillCount => playerKillCount;

    private string playerName;
    public string PlayerName => playerName;

    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentLevel = 0;
        currentLevelInstance = Instantiate(levelPrefab[currentLevel]);
        PauseGame();
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = -1;
    }

    private void Update()
    {
        if (!isPlaying) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0f)
        {
            spawnTimer = float.MaxValue;
            int randomAmt = UnityEngine.Random.Range(minAmountPerSpawn, maxAmountPerSpawn);
            StartCoroutine(SpawnEnemy(randomAmt));
        }
    }

    private IEnumerator SpawnEnemy(int amount)
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < amount; i++)
        {
            enemiesOnScreen.Add(onEnemySpawn?.Invoke());
            yield return new WaitForSeconds(1f);
        }

        spawnTimer = spawnCooldown;
    }

    public void StartNewGame()
    {
        StartCoroutine(SpawnEnemy(initialAmountEnemy));
        spawnTimer = spawnCooldown;
        isPlaying = true;
        ResumeGame();
        player.OnNewGame();
        playerKillCount = 0;
    }

    public void IncreaseKillCount()
    {
        playerKillCount++;
        if (playerKillCount >= 10)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        UIManager.Instance.SwitchToWinPanel();
        ReturnAllEnemy();
        PauseGame();
    }

    public void PauseGame()
    {
        isPlaying = false;
        foreach (var enemy in enemiesOnScreen)
        {
            enemy.IsPause = true;
        }
    }

    public void ResumeGame()
    {
        isPlaying = true;
        foreach (var enemy in enemiesOnScreen)
        {
            enemy.IsPause = false;
        }
    }

    public void ReturnAllEnemy()
    {
        isPlaying = false;
        foreach (var enemy in enemiesOnScreen)
        {
            enemy.ReleaseSelf();
        }
    }

    public void NextLevel()
    {
        Debug.Log("Next Level");
        currentLevel = ++currentLevel % levelPrefab.Count;
        Destroy(currentLevelInstance);
        currentLevelInstance = Instantiate(levelPrefab[currentLevel]);
        StartNewGame();
        UIManager.Instance.SwitchToIngameUI();
    }
}
