using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 10f;  // Movement speed
    public float boundaryY = 4f; // Limits movement on Y-axis

    public bool isPlayerOne; // Check if this is Player 1

    void Update()
    {
        float move = 0f;

        // Player 1 (W/S)
        if (isPlayerOne)
        {
            if (Input.GetKey(KeyCode.W)) move = 1f;
            if (Input.GetKey(KeyCode.S)) move = -1f;
        }
        // Player 2 (Arrow Keys)
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) move = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) move = -1f;
        }

        // Move paddle and clamp position
        Vector3 newPosition = transform.position + new Vector3(0, move * speed * Time.deltaTime, 0);
        newPosition.y = Mathf.Clamp(newPosition.y, -boundaryY, boundaryY);
        transform.position = newPosition;
    }
}

