using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f; // Ball movement speed
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBall();
    }

    void LaunchBall()
    {
        float xDirection = Random.Range(0, 2) == 0 ? -1 : 1; // Random left or right
        float yDirection = Random.Range(-0.5f, 0.5f); // Small vertical component

        Vector3 direction = new Vector3(xDirection, yDirection, 0).normalized;
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Minimum vertical angle (adjust between 0.1f and 0.3f for gameplay feel)
        float minVerticalAngle = 0.05f;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Calculate bounce angle based on paddle hit position
            float hitPosition = (transform.position.y - collision.transform.position.y) /
                              collision.collider.bounds.size.y;

            Vector3 newDirection = new Vector3(
                -Mathf.Sign(rb.linearVelocity.x), // Reverse X direction
                Mathf.Clamp(hitPosition * 2f, -1f, 1f), // Ensure Y isn't too extreme
                0
            ).normalized;

            // Enforce minimum vertical movement
            if (Mathf.Abs(newDirection.y) < minVerticalAngle)
            {
                newDirection.y = minVerticalAngle * Mathf.Sign(newDirection.y);
                newDirection = newDirection.normalized;
            }

            rb.linearVelocity = newDirection * speed;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 newDirection = new Vector3(
                rb.linearVelocity.x,
                -rb.linearVelocity.y,
                0
            ).normalized;

            // Prevent pure horizontal movement after wall bounces
            if (Mathf.Abs(newDirection.y) < minVerticalAngle)
            {
                newDirection.y = minVerticalAngle * Mathf.Sign(newDirection.y);
                newDirection = newDirection.normalized;
            }

            rb.linearVelocity = newDirection * speed;
        }
        speed = speed * 1.02f;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        Invoke(nameof(LaunchBall), 1f); // Relaunch after 1 second
    }

    void FixedUpdate()
    {
        // Ensure speed never changes due to physics quirks
        if (rb.linearVelocity.magnitude != speed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }

}