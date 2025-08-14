using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float baseSpeed = 6f;
    public float speedIncreaseRate = 0.1f; 
    private float currentSpeed;
    private bool scoreGiven = false;

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        currentSpeed += speedIncreaseRate * Time.deltaTime;

        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

        if (!scoreGiven && transform.position.y < GameObject.FindGameObjectWithTag("Player").transform.position.y)
        {
            GameManager.Instance.AddScore(20);
            scoreGiven = true;
        }

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.RemoveScore(10);
            
        GameManager.Instance.currentFuel -= 60f;
        if (GameManager.Instance.currentFuel < 0)
            GameManager.Instance.currentFuel = 0;

            PlayerCollisionEffect effect = collision.GetComponent<PlayerCollisionEffect>();
            if (effect != null)
            {
                effect.PlayCrashEffect();
            }

            if (GameManager.Instance.Score <= 0)
                GameManager.Instance.GameOver();

           GameManager.Instance.RegisterHit();
            Destroy(gameObject);
        }
    }
}
