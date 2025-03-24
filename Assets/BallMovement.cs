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
        float minAngle = 0.1f; // Minimum movement angle (adjust as needed)

        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Paddle reflection logic (existing code)
            float hitPosition = (transform.position.y - collision.transform.position.y) /
                               collision.collider.bounds.size.y;

            Vector3 newDirection = new Vector3(
                -Mathf.Sign(rb.linearVelocity.x),
                hitPosition * 2f,
                0
            ).normalized;

            // Enforce minimum angles
            if (Mathf.Abs(newDirection.x) < minAngle)
                newDirection.x = minAngle * Mathf.Sign(newDirection.x);
            if (Mathf.Abs(newDirection.y) < minAngle)
                newDirection.y = minAngle * Mathf.Sign(newDirection.y);

            rb.linearVelocity = newDirection.normalized * speed;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Wall reflection logic (updated)
            Vector3 newDirection = new Vector3(
                rb.linearVelocity.x,
                -rb.linearVelocity.y,
                0
            ).normalized;

            // Enforce minimum angles
            if (Mathf.Abs(newDirection.x) < minAngle)
                newDirection.x = minAngle * Mathf.Sign(newDirection.x);
            if (Mathf.Abs(newDirection.y) < minAngle)
                newDirection.y = minAngle * Mathf.Sign(newDirection.y);

            rb.linearVelocity = newDirection.normalized * speed;
        }
        speed = speed * 1.05f;
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