using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 5f;     // Hareket hızı
    public float xLimit = 3.5f;  // Sağ-sol sınır

void Update()
{
    float moveX = 0f;

    // Klavye ile kontrol
    moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

    // Mouse ile kontrol
    if (Input.GetMouseButton(0)) // Sol mouse tuşu basılıysa
    {
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.x > Screen.width / 2)
        {
            // Ekranın sağ tarafı: sağa git
            moveX = speed * Time.deltaTime;
        }
        else
        {
            // Ekranın sol tarafı: sola git
            moveX = -speed * Time.deltaTime;
        }
    }

    // Aracı hareket ettir
    transform.Translate(moveX, 0, 0);

    // X pozisyonunu sınırla
    float clampedX = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
    transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
}
}
