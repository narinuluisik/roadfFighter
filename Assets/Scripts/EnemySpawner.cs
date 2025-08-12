using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject normalEnemyPrefab;
    public GameObject movingEnemyPrefab;

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 4f;
    public float spawnHeight = 6f;

    public float minDistanceBetweenCars = 4f;  // Minimum araçlar arası mesafe
    public float[] lanes = new float[] { -3f, -1f, 1f, 3f };

    private float spawnTimer;

    void Start()
    {
        ResetSpawnTimer();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            TrySpawnEnemy();
            ResetSpawnTimer();
        }
    }

    void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

void TrySpawnEnemy()
{
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject player = GameObject.FindGameObjectWithTag("Player");

    float carLength = 4; // Araç yüksekliği kadar mesafe
    List<int> safeLanes = new List<int>();

    for (int i = 0; i < lanes.Length; i++)
    {
        bool isSafe = true;
        Vector3 spawnPos = new Vector3(lanes[i], spawnHeight, 0f);

        // 1. Player ile mesafe kontrolü
        if (player != null)
        {
            float px = Mathf.Abs(player.transform.position.x - spawnPos.x);
            float py = Mathf.Abs(player.transform.position.y - spawnPos.y);
            if (px <= 1.5f && py < minDistanceBetweenCars)
            {
                isSafe = false;
            }
        }

        // 2. Tüm araçlarla mesafe kontrolü
        foreach (var enemy in enemies)
        {
            float xDiff = Mathf.Abs(enemy.transform.position.x - spawnPos.x);
            float yDiff = enemy.transform.position.y - spawnPos.y;

            // Aynı şerit veya yan şerit fark etmez
            if (xDiff <= 3.5f) // aynı veya yan şerit
            {
                // Eğer yeni araç, mevcut aracın çok yakınına spawn oluyorsa engelle
                if (Mathf.Abs(yDiff) < (carLength + 0.5f))
                {
                    isSafe = false;
                    break;
                }
            }
        }

        if (isSafe)
            safeLanes.Add(i);
    }

    // Hiç güvenli şerit yoksa spawn iptal
    if (safeLanes.Count == 0) return;

    // Güvenli şeritlerden rastgele birini seç
    int laneIndex = safeLanes[Random.Range(0, safeLanes.Count)];
    Vector3 finalSpawnPos = new Vector3(lanes[laneIndex], spawnHeight, 0f);

    // Prefab seç
    GameObject prefabToSpawn = (Random.value < 0.2f) ? movingEnemyPrefab : normalEnemyPrefab;

    Instantiate(prefabToSpawn, finalSpawnPos, Quaternion.identity).tag = "Enemy";
}


}
