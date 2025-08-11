using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        // Düşman aşağı doğru hareket eder
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Ekrandan çıkarsa yok et
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !GameManager.Instance.IsGameOver)
        {
            // Skoru 10 düşür
            GameManager.Instance.RemoveScore(10);

            // Skor 0 ise oyunu bitir
            if (GetScore() <= 0)
            {
                GameManager.Instance.GameOver();
            }

            // Çarpan düşmanı yok et
            Destroy(gameObject);
        }
    }

    int GetScore()
    {
        // GameManager içindeki score'a direkt erişemiyoruz çünkü private.
        // Bu yüzden sadece EnemyCar içinde geçici bir çözüm: UI'dan skor çekmek.
        // Daha iyisi GameManager'a public int Score { get; } eklemek olur.
        string scoreText = GameManager.Instance.scoreText.text.Replace("Score: ", "");
        int.TryParse(scoreText, out int currentScore);
        return currentScore;
    }
}
