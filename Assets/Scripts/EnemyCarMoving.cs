using UnityEngine;

public class EnemyCarMoving : MonoBehaviour
{
    public float speed = 4f;      
    public float moveSpeed = 2f;   
    public float moveRange = 0.5f; 

    private float startX;
    private int direction = 1;
    private float globalXLimit = 3.5f; 

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        float newX = transform.position.x + direction * moveSpeed * Time.deltaTime;

    
        newX = Mathf.Clamp(newX, startX - moveRange, startX + moveRange);
        newX = Mathf.Clamp(newX, -globalXLimit, globalXLimit);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

    
        if (newX <= startX - moveRange || newX >= startX + moveRange)
        {
            direction *= -1;
        }

        
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.RemoveScore(10);
 
 GameManager.Instance.currentFuel -= 70f;
        if (GameManager.Instance.currentFuel < 0)
            GameManager.Instance.currentFuel = 0;
        
            PlayerCollisionEffect effect = other.GetComponent<PlayerCollisionEffect>();
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
