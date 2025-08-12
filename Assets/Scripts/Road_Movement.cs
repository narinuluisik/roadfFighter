using UnityEngine;

public class Road_Movement : MonoBehaviour
{
    public Renderer meshRenderer;
    public float baseSpeed = 0.5f;
    public float speedMultiplier = 0.03f;

    void Update()
    {
        float playerSpeed = GameManager.Instance != null ? GameManager.Instance.CurrentSpeed : 0f;

        float moveSpeed = baseSpeed + playerSpeed * speedMultiplier;

        meshRenderer.material.mainTextureOffset += new Vector2(0, moveSpeed * Time.deltaTime);
    }
}
