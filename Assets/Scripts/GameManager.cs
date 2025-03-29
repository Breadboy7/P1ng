using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    public GameObject ball;

    private int playerScore = 0;
    private int aiScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayerScores()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
        ResetAfterScore();
    }

    public void AIScores()
    {
        aiScore++;
        aiScoreText.text = aiScore.ToString();
        ResetAfterScore();
    }

    void ResetAfterScore()
    {
        ball.GetComponent<BallController>().ResetBall();

        // Reset paddle positions if needed
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        foreach (GameObject paddle in paddles)
        {
            paddle.GetComponent<PaddleController>().ResetPosition();
        }

        if (aiScore >= 5 || playerScore >= 5)
            gameOver();
    }

    void gameOver()
    {
        string winner;

        if (playerScore > aiScore)
            winner = "Player wins!";
        else
            winner = "Computer Wins!";

        Debug.Log(winner);
    }
}