using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float maxSpeed = 650f;     
    public float acceleration = 80f;   
    public float deceleration = 50f;  
    public float xLimit = 3.5f;
    public float sideSpeedMultiplier = 0.05f; 

    private float currentSpeed = 0f;
    private float moveX = 0f;

    void Update()
    {
        
        currentSpeed += acceleration * Time.deltaTime;
        if (currentSpeed > maxSpeed) currentSpeed = maxSpeed;

        float sideSpeed = currentSpeed * sideSpeedMultiplier;

        
        moveX = Input.GetAxis("Horizontal") * sideSpeed * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x > Screen.width / 2)
                moveX = sideSpeed * Time.deltaTime;
            else
                moveX = -sideSpeed * Time.deltaTime;
        }

        transform.Translate(moveX, 0, 0);
        float clampedX = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        
        GameManager.Instance.SetSpeed(currentSpeed);

          Debug.Log("Player_Movement currentSpeed: " + currentSpeed);
          Debug.Log("GameManager CurrentSpeed: " + GameManager.Instance.CurrentSpeed);
    }
}
