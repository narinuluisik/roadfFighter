using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text scoreText;
    public TMP_Text speedText;
    public GameObject gameOverPanel;

    [Header("Yakıt Sistemi")]
    public Slider fuelSlider;         
    public float maxFuel = 800f;
    public float currentFuel;
    public float fuelConsumptionRate = 20f; 

    [Header("Çarpma Sistemi")]
    public int maxConsecutiveHits = 3; 
    public float hitResetTime = 3f;    
    private int currentHitCount = 0;
    private float lastHitTime = -999f;

    public GameObject fuelPrefab; 
    public float fuelSpawnInterval = 5f; 
    private float fuelSpawnTimer = 0f;

    public bool IsGameOver { get; private set; } = false;

    private int score = 0;
    private float scoreTimer = 0f;

    private float currentSpeed = 0f;
    private float maxSpeed = 650f;

    public int Score => score;
    public float CurrentSpeed => currentSpeed;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;
        IsGameOver = false;

        score = 0;
        currentSpeed = 60f;

        currentFuel = maxFuel;
        if (fuelSlider != null)
        {
            fuelSlider.maxValue = maxFuel;
            fuelSlider.value = currentFuel;
        }

        UpdateUI();

        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!IsGameOver)
        {
           
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 1f)
            {
                AddScore(5);
                scoreTimer = 0f;
            }

            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

            if (fuelSlider != null)
                fuelSlider.value = currentFuel;

            if (currentFuel <= 0f)
            {
                GameOver();
            }

            fuelSpawnTimer += Time.deltaTime;
            if (fuelSpawnTimer >= fuelSpawnInterval)
            {
                SpawnFuel();
                fuelSpawnTimer = 0f;
            }

            UpdateUI();
        }
    }

    void SpawnFuel()
    {
        float randomX = Random.Range(-2f, 2f);
        Vector3 spawnPos = new Vector3(randomX, 5f, 0f);
        Instantiate(fuelPrefab, spawnPos, Quaternion.identity);
    }

    public void AddScore(int amount)
    {
        if (IsGameOver) return;
        score += amount;
        UpdateUI();
    }

    public void RemoveScore(int amount)
    {
        if (IsGameOver) return;
        score -= amount;
        if (score < 0) score = 0;
        UpdateUI();
    }

    public void SetSpeed(float speed)
    {
        currentSpeed = Mathf.Clamp(speed, 0f, maxSpeed);
    }

    public void AddFuel(float amount)
    {
        if (IsGameOver) return;
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
        if (fuelSlider != null)
            fuelSlider.value = currentFuel;
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString("D6");
        if (speedText != null)
            speedText.text = Mathf.RoundToInt(currentSpeed) + " km/h";
    }

    public void GameOver()
    {
        if (IsGameOver) return;
        IsGameOver = true;
          AudioManager.Instance.StopMusic();
        Time.timeScale = 0f;
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void RegisterHit()
    {
        float currentTime = Time.time;

      
        if (currentTime - lastHitTime > hitResetTime)
        {
            currentHitCount = 0;
        }

        currentHitCount++;
        lastHitTime = currentTime;

        Debug.Log("Üst üste çarpma sayısı: " + currentHitCount);

      
        if (currentHitCount >= maxConsecutiveHits)
        {
            GameOver();
        }
    }
}
