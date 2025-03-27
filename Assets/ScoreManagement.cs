using UnityEngine;
using TMPro;

public class ScoreManagement : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;
    public int maxScore = 5; // Winning score

    public TextMeshProUGUI scoreText; // TMPro UI Text to display scores
    public GameObject ball; // Reference to the ball (assign in Inspector)

    // Assign these in the Inspector to the goal colliders
    public GameObject player1Goal;
    public GameObject player2Goal;

    private void Start()
    {
        UpdateScoreText();
    }

    // Called when the ball enters a goal trigger
    public void OnGoalScored(GameObject goal)
    {
        if (goal.tag.Equals("Goal1"))
        {
            player2Score++; // Player 2 scores when ball enters Player 1's goal
        }
        else if (goal.tag.Equals("Goal2"))
        {
            player1Score++; // Player 1 scores when ball enters Player 2's goal
        }

        UpdateScoreText();
        CheckWinCondition();

        // Reset ball and paddles
        if (ball != null)
        {
            ball.GetComponent<BallMovement>().ResetBall();
        }
        ResetPaddles();
    }

    void CheckWinCondition()
    {
        if (player1Score >= maxScore)
        {
            Debug.Log("Player 1 Wins!");
            ResetGame();
        }
        else if (player2Score >= maxScore)
        {
            Debug.Log("Player 2 Wins!");
            ResetGame();
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{player1Score} - {player2Score}";
        }
    }

    void ResetGame()
    {
        player1Score = 0;
        player2Score = 0;
        UpdateScoreText();
    }

    void ResetPaddles()
    {
        GameObject[] paddles = GameObject.FindGameObjectsWithTag("Paddle");
        foreach (GameObject paddle in paddles)
        {
            paddle.transform.position = new Vector3(paddle.transform.position.x, 0, 0);
        }
    }
}