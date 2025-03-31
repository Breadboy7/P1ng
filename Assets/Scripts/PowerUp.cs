using UnityEngine;
using System.Collections;
public enum PowerUpType
{
    PaddleSizeIncrease,
    PaddleSpeedBoost,
    BallSpeedDecrease,
    BallSpeedIncrease,
    MultiBall,
    ReverseDirection
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float duration = 5f;
    public float sizeIncreaseAmount = 0.5f;
    public float speedBoostAmount = 3f;
    public float ballSpeedDecreaseAmount = 3f;

    public GameObject collectionParticlePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            //Get ball audio source and play it
            other.GetComponent<AudioSource>().Play();

            // Spawn collection particles
            if (collectionParticlePrefab != null)
            {
                Instantiate(collectionParticlePrefab, transform.position, Quaternion.identity);
            }

            // Determine who gets the power-up based on ball direction
            bool isPlayerPowerUp = other.GetComponent<Rigidbody>().linearVelocity.x > 0;

            ApplyPowerUp(isPlayerPowerUp);
            Destroy(gameObject);
        }
    }

    private void ApplyPowerUp(bool isPlayer)
    {
        GameObject paddle = isPlayer ?
            GameObject.FindGameObjectWithTag("PlayerPaddle") :
            GameObject.FindGameObjectWithTag("AIPaddle");

        switch (type)
        {
            case PowerUpType.PaddleSizeIncrease:
                StartCoroutine(PaddleSizeIncrease(paddle));
                break;
            case PowerUpType.PaddleSpeedBoost:
                StartCoroutine(PaddleSpeedBoost(paddle));
                break;
            case PowerUpType.BallSpeedDecrease:
                BallSpeedDecrease();
                break;
            case PowerUpType.MultiBall:
                CreateMultiBall(isPlayer);
                break;
            case PowerUpType.ReverseDirection:
                ReverseDirection();
                break;
            case PowerUpType.BallSpeedIncrease:
                BallSpeedIncrease(isPlayer);
                break;
        }
    }

    private void ReverseDirection()
    {
        GameObject originalBall = GameObject.FindGameObjectWithTag("Ball");
        if (originalBall != null)
        {
            Rigidbody rb = originalBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Reverse the X direction
                Vector3 currentVelocity = rb.linearVelocity;
                rb.linearVelocity = new Vector3(-currentVelocity.x, currentVelocity.y, currentVelocity.z);

                StartCoroutine(FlashBall(originalBall));
            }
        }
    }

    private IEnumerator FlashBall(GameObject ball)
    {
        Renderer ballRenderer = ball.GetComponent<Renderer>();
        Color originalColor = ballRenderer.material.color;

        // Flash red briefly
        ballRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        ballRenderer.material.color = originalColor;
    }

    private IEnumerator PaddleSizeIncrease(GameObject paddle)
    {
        Vector3 originalScale = paddle.transform.localScale;
        paddle.transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y + sizeIncreaseAmount,
            originalScale.z);

        yield return new WaitForSeconds(duration);
        paddle.transform.localScale = originalScale;
    }

    private IEnumerator PaddleSpeedBoost(GameObject paddle)
    {
        PaddleController controller = paddle.GetComponent<PaddleController>();
        float originalSpeed = controller.speed;
        controller.speed += speedBoostAmount;

        yield return new WaitForSeconds(duration);
        controller.speed = originalSpeed;
    }

    private void BallSpeedDecrease()
    {
        // Find all balls in play
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.linearVelocity = rb.linearVelocity/2;
            
        }
    }

    private IEnumerator BallSpeedIncrease(bool isPlayer)
    {
        // Find all balls in play
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if ((rb.linearVelocity.x > 0 && !isPlayer) || (rb.linearVelocity.x < 0 && isPlayer))
            {
                Vector3 originalVelocity = rb.linearVelocity;
                rb.linearVelocity = originalVelocity.normalized *
                    Mathf.Max(originalVelocity.magnitude + ballSpeedDecreaseAmount, 3f);

                yield return new WaitForSeconds(duration);
                rb.linearVelocity = originalVelocity;
            }
        }
    }

    private void CreateMultiBall(bool isPlayer)
    {
        GameObject originalBall = GameObject.FindGameObjectWithTag("Ball");
        if (originalBall == null) return;

        // Create new ball
        GameObject newBall = Instantiate(originalBall, originalBall.transform.position, Quaternion.identity);

    }
}