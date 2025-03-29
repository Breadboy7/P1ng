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
        float moveInput = Input.GetAxis("Vertical");
        Vector3 newPosition = transform.position + Vector3.up * moveInput * speed * Time.deltaTime;

        // Clamp Y position
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