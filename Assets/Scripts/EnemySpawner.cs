using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.5f;
    private float timer;

    // Şerit X pozisyonları (4 şerit)
    float[] lanes = { -3f, -1f, 1f, 3f };

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return; // Oyun bittiyse spawn etme

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int randomLane = Random.Range(0, lanes.Length);
        Vector3 spawnPos = new Vector3(lanes[randomLane], 6f, 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
