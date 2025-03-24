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
        float yDirection = Random.Range(-1f, 1f); // Random vertical component

        Vector3 direction = new Vector3(xDirection, yDirection, 0).normalized;
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Paddle")
        {
            Debug.Log("hit paddle");
            rb.linearVelocity.Set(-rb.linearVelocity.x, rb.linearVelocity.y, 0);
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Debug.Log("hit wall");
            rb.linearVelocity.Set(rb.linearVelocity.x, -rb.linearVelocity.y, 0);
        }
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        Invoke(nameof(LaunchBall), 1f); // Relaunch after 1 second
    }
}
