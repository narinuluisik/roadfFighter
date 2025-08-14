using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    public float fuelAmount = 100f;
    public AudioClip pickupSound;  
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

            Destroy(gameObject, pickupSound.length);
        }
    }
}
