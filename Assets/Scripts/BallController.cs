using UnityEngine;

public class BallController : MonoBehaviour
{
    public float initialSpeed = 8f;
    public float maxSpeed = 15f;
    public float speedIncrease = 0.5f;

    private Rigidbody rb;
    private Vector3 initialPosition;
    private float currentSpeed;
    private bool gameStarted = false;

    public GameObject collisionParticlePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        ResetBall();
    }

    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        LaunchBall();
    }

    void LaunchBall()
    {
        currentSpeed = initialSpeed;

        // Random direction but always towards a player
        float randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 direction = new Vector3(randomDirection, Random.Range(-0.5f, 0.5f), 0).normalized;

        rb.linearVelocity = direction * currentSpeed;
    }

    public void ResetBall()
    {
        gameStarted = false;
        rb.linearVelocity = Vector3.zero;
        transform.position = initialPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Spawn particles at collision point
        if (collisionParticlePrefab != null)
        {
            ContactPoint contact = collision.contacts[0];
            Instantiate(collisionParticlePrefab, contact.point, Quaternion.identity);
        }

        //Check if a paddle is hit
        if (collision.gameObject.CompareTag("PlayerPaddle") || collision.gameObject.CompareTag("AIPaddle") || collision.gameObject.CompareTag("Player2"))
        {
            // Calculate bounce angle based on where the ball hits the paddle
            float hitFactor = (transform.position.y - collision.transform.position.y) /
                              (collision.collider.bounds.size.y / 2);

            // Calculate direction and speed
            Vector3 dir = new Vector3(
                collision.gameObject.transform.position.x < 0 ? 1 : -1, // Direction based on which paddle
                hitFactor,
                0).normalized;

            // Increase speed slightly with each hit
            currentSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);
            rb.linearVelocity = dir * currentSpeed;
        }
        gameObject.GetComponent<AudioSource>().Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            ResetBall();
        }
    }
}