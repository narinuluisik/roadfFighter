using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    public float fuelAmount = 300f;
    public AudioClip pickupSound;  // Inspector'dan atanacak
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = pickupSound;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddFuel(fuelAmount);
            audioSource.Play();

            // Ses çalarken objeyi hemen yok etmemek için destroy'i geciktiriyoruz
            Destroy(gameObject, pickupSound.length);
        }
    }
}
