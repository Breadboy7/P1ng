using UnityEngine;

public class Goal : MonoBehaviour
{
    public ScoreManagement scoreManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Notify ScoreManager that a goal was scored, passing this goal's Collider
            scoreManager.OnGoalScored(gameObject);
        }
    }
}