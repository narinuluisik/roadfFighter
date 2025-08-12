using UnityEngine;

public class EnemyCarMoving : MonoBehaviour
{
    public float speed = 4f;       // Aşağı hareket hızı
    public float moveSpeed = 2f;   // Sağa sola hareket hızı
    public float moveRange = 0.5f; // Maksimum sağa/sola kayma mesafesi

    private float startX;
    private int direction = 1;
    private float globalXLimit = 3.5f; // Global x sınırı

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        // Aşağı doğru hareket
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Sağa sola hareket
        float newX = transform.position.x + direction * moveSpeed * Time.deltaTime;

        // Önce startX ± moveRange ile sınırla
        newX = Mathf.Clamp(newX, startX - moveRange, startX + moveRange);

        // Sonra global sınırla (-3.5 ile 3.5 arasında olsun)
        newX = Mathf.Clamp(newX, -globalXLimit, globalXLimit);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Eğer sınırdaysa yön değiştir
        if (newX <= startX - moveRange || newX >= startX + moveRange)
        {
            direction *= -1;
        }

        // Ekrandan çıkarsa yok et
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
            // Çarpışma efektini tetikle
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
