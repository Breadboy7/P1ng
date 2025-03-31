using UnityEngine;

public enum PowerUpType
{
    PaddleSizeIncrease,
    PaddleSpeedBoost,
    BallSpeedDecrease,
    MultiBall
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float duration = 5f;
    public float sizeIncreaseAmount = 0.5f;
    public float speedBoostAmount = 3f;
    public float ballSpeedDecreaseAmount = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
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
                StartCoroutine(BallSpeedDecrease(isPlayer));
                break;
            case PowerUpType.MultiBall:
                CreateMultiBall(isPlayer);
                break;
        }
    }

    private System.Collections.IEnumerator PaddleSizeIncrease(GameObject paddle)
    {
        Vector3 originalScale = paddle.transform.localScale;
        paddle.transform.localScale = new Vector3(
            originalScale.x,
            originalScale.y + sizeIncreaseAmount,
            originalScale.z);

        yield return new WaitForSeconds(duration);
        paddle.transform.localScale = originalScale;
    }

    private System.Collections.IEnumerator PaddleSpeedBoost(GameObject paddle)
    {
        PaddleController controller = paddle.GetComponent<PaddleController>();
        float originalSpeed = controller.speed;
        controller.speed += speedBoostAmount;

        yield return new WaitForSeconds(duration);
        controller.speed = originalSpeed;
    }

    private System.Collections.IEnumerator BallSpeedDecrease(bool isPlayer)
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
                    Mathf.Max(originalVelocity.magnitude - ballSpeedDecreaseAmount, 3f);

                yield return new WaitForSeconds(duration);
                rb.linearVelocity = originalVelocity;
            }
        }
    }

    private void CreateMultiBall(bool isPlayer)
    {
        GameObject originalBall = GameObject.FindGameObjectWithTag("Ball");
        for (int i = 0; i < 2; i++) // Create 2 extra balls
        {
            GameObject newBall = Instantiate(originalBall, originalBall.transform.position, Quaternion.identity);
            Rigidbody rb = newBall.GetComponent<Rigidbody>();

            // Give the new balls slightly different directions
            Vector3 direction = new Vector3(
                isPlayer ? 1 : -1,
                Random.Range(-0.5f, 0.5f),
                0).normalized;

            rb.linearVelocity = direction * originalBall.GetComponent<Rigidbody>().linearVelocity.magnitude;
        }
    }
}