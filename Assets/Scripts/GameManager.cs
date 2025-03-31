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
        //Destroy multiball powerup instances
        DestroyObjects("Ball");

        ball.GetComponent<BallController>().ResetBall();

        // Reset paddle positions if needed
        GameObject[] paddles = { GameObject.FindGameObjectWithTag("AIPaddle"), GameObject.FindGameObjectWithTag("PlayerPaddle") };
        foreach (GameObject paddle in paddles)
        {
            //Reset each paddles scale and position after scoring
            paddle.transform.localScale = new Vector3(0.25f, 2, 1);
            paddle.GetComponent<PaddleController>().ResetPosition();
        }

        //Destroy all powerups
        DestroyObjects("PowerUp");

        //Reset power up spawner coroutine
        gameObject.GetComponent<PowerUpSpawner>().resetSpawn();

        if (aiScore >= 5 || playerScore >= 5)
            gameOver();
    }

    //Helper method to destory all gameobjects with specified tag
    void DestroyObjects(string s)
    {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag(s);
        if (s.Equals("Ball"))
        {
            if (powerups.Length > 1)
                for (int i = 1; i < powerups.Length; i++)
                    Destroy(powerups[i]);
        }
        else {
            foreach (GameObject power in powerups)
                Destroy(power);
        }
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