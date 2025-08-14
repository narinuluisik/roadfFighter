using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject normalEnemyPrefab;
    public GameObject movingEnemyPrefab;

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 4f;
    public float spawnHeight = 6f;

    public float minDistanceBetweenCars = 4f;  
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

    float carLength = 4; 
    List<int> safeLanes = new List<int>();

    for (int i = 0; i < lanes.Length; i++)
    {
        bool isSafe = true;
        Vector3 spawnPos = new Vector3(lanes[i], spawnHeight, 0f);

 
        if (player != null)
        {
            float px = Mathf.Abs(player.transform.position.x - spawnPos.x);
            float py = Mathf.Abs(player.transform.position.y - spawnPos.y);
            if (px <= 1.5f && py < minDistanceBetweenCars)
            {
                isSafe = false;
            }
        }

     
        foreach (var enemy in enemies)
        {
            float xDiff = Mathf.Abs(enemy.transform.position.x - spawnPos.x);
            float yDiff = enemy.transform.position.y - spawnPos.y;

            
            if (xDiff <= 3.5f) 
            {
        
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

     if (safeLanes.Count == 0) return;

    
    int laneIndex = safeLanes[Random.Range(0, safeLanes.Count)];
    Vector3 finalSpawnPos = new Vector3(lanes[laneIndex], spawnHeight, 0f);


    GameObject prefabToSpawn = (Random.value < 0.2f) ? movingEnemyPrefab : normalEnemyPrefab;

    Instantiate(prefabToSpawn, finalSpawnPos, Quaternion.identity).tag = "Enemy";
}


}
