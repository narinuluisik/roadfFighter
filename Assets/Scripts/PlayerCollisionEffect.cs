using UnityEngine;

public class PlayerCollisionEffect : MonoBehaviour
{
    public AudioSource crashAudioSource; // Çarpışma sesi için AudioSource
    public Camera mainCamera;
    public float shakeDuration = 0.3f;  // Sarsılma süresi
    public float shakeMagnitude = 0.1f; // Sarsılma şiddeti

    private float shakeTimeRemaining = 0f;
    private Vector3 originalCamPos;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        originalCamPos = mainCamera.transform.position;

        if (crashAudioSource == null)
            crashAudioSource = GetComponent<AudioSource>();
            
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            mainCamera.transform.position = originalCamPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            mainCamera.transform.position = originalCamPos;
        }
    }

    public void PlayCrashEffect()
    {
        if (crashAudioSource != null && crashAudioSource.clip != null)
        {
            crashAudioSource.Play();
        }

        shakeTimeRemaining = shakeDuration;
    }
}
