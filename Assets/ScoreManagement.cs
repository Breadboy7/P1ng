using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public int player1Score = 0;
    public int player2Score = 0;
    public int maxScore = 5; // Winning score

    public GameObject player1Goal; // Assign Player 1's goal collider in Inspector
    public GameObject player2Goal; // Assign Player 2's goal collider in Inspector

    public Text scoreText; // UI Text to display scores

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the ball hit Player 1's goal (Player 2 scores)
        if (other == player1Goal)
        {
            player2Score++;
            CheckWinCondition();
        }
        // Check if the ball hit Player 2's goal (Player 1 scores)
        else if (other == player2Goal)
        {
            player1Score++;
            CheckWinCondition();
        }

        // Update score display
        UpdateScoreText();
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
            scoreText.text = player1Score + " - " + player2Score;
        }
    }

    void ResetGame()
    {
        player1Score = 0;
        player2Score = 0;
        UpdateScoreText();
        // Optionally: Reset ball and paddles here
    }
}
