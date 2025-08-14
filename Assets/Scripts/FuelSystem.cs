using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    public static FuelSystem Instance;

    [Header("Fuel Settings")]
    public float maxFuel = 600f;
    public float currentFuel;
    public float fuelDecreaseRate = 1.5f;

    [Header("UI")]
    public Slider fuelSlider; 

    [Header("Optional Settings")]
    public bool scaleWithSpeed = true; 
    public float baseSpeedForScale = 120f; 
    public float extraDrainPer100Speed = 0.6f;

    [Range(0.1f, 2f)]
    public float drainMultiplier = 0.6f; 
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentFuel = maxFuel;
        UpdateUI();
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        float speedFactor = 1f;
        if (scaleWithSpeed && GameManager.Instance != null)
        {
            float speedDelta = Mathf.Max(0f, GameManager.Instance.CurrentSpeed - baseSpeedForScale);
            speedFactor += (speedDelta / 100f) * extraDrainPer100Speed;
        }

        currentFuel -= fuelDecreaseRate * speedFactor * drainMultiplier * Time.deltaTime;

        if (currentFuel <= 0f)
        {
            currentFuel = 0f;
            UpdateUI();
       
            if (GameManager.Instance != null)
                GameManager.Instance.GameOver();
            return;
        }

        UpdateUI();
    }

    public void AddFuel(float amount)
    {
        currentFuel = Mathf.Clamp(currentFuel + amount, 0f, maxFuel);
        UpdateUI();
    }

    public void ReduceFuel(float amount)
    {
        currentFuel = Mathf.Clamp(currentFuel - amount, 0f, maxFuel);
        if (currentFuel <= 0f && GameManager.Instance != null)
            GameManager.Instance.GameOver();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (fuelSlider != null)
            fuelSlider.value = currentFuel / maxFuel;
    }
}
