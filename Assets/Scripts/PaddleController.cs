using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 10f;
    public float yLimit = 4f;
    public bool isPlayer = true;
    private GameObject ball;

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
            ball = FindClosestBall();

            if (ball != null)
            {
                // Add random offset to target position (10% of play area)
                float randomOffset = Random.Range(-yLimit * 0.1f, yLimit * 0.1f);
                float targetY = Mathf.Clamp(ball.transform.position.y + randomOffset, -yLimit, yLimit);

                float currentY = transform.position.y;
                float newY = Mathf.MoveTowards(currentY, targetY, speed * 0.8f * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }

    // Current method to find the closest ball
    private GameObject FindClosestBall()
    {
        // Find all balls in the scene
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        if (balls.Length == 0) return null;
        if (balls.Length == 1) return balls[0];

        GameObject closestBall = null;
        float closestDistance = Mathf.Infinity;
        Vector3 paddlePosition = transform.position;

        foreach (GameObject ball in balls)
        {
            float distance = Vector3.Distance(ball.transform.position, paddlePosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestBall = ball;
            }
        }

        return closestBall;
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
        else if (gameObject.CompareTag("AIPaddle"))
        {
            // Use Up/Down arrows for AI paddle (or second player)
            if (Input.GetKey(KeyCode.UpArrow)) moveInput = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) moveInput = -1f;
        }

        Vector3 newPosition = transform.position + Vector3.up * moveInput * speed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, -yLimit, yLimit);
        transform.position = newPosition;
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}