using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float yLimit = 4f;
    public bool isPlayer = true;
    public GameObject ball;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isPlayer)
        {
            PlayerControl();
        }
        else
        {
            AIControl();
        }
    }

    void PlayerControl()
    {
        float moveInput = 0f;

        if (gameObject.CompareTag("PlayerPaddle"))
        {
            // Use W/S keys for player paddle
            if (Input.GetKey(KeyCode.W)) moveInput = 1f;
            if (Input.GetKey(KeyCode.S)) moveInput = -1f;
        }
        else if (gameObject.CompareTag("Player2"))
        {
            // Use Up/Down arrows for AI paddle (or second player)
            if (Input.GetKey(KeyCode.UpArrow)) moveInput = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) moveInput = -1f;
        }

        Vector3 newPosition = transform.position + Vector3.up * moveInput * speed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -yLimit, yLimit);
        transform.position = newPosition;
    }

    void AIControl()
    {
        if (ball != null)
        {
            // Simple AI that follows the ball
            float targetY = Mathf.Clamp(ball.transform.position.y, -yLimit, yLimit);
            float currentY = transform.position.y;

            // Move towards ball but not too precisely to make it beatable
            float newY = Mathf.MoveTowards(currentY, targetY, speed * 0.8f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}