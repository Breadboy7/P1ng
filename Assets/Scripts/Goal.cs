using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayerGoal = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (isPlayerGoal)
            {
                GameManager.Instance.AIScores();
            }
            else
            {
                GameManager.Instance.PlayerScores();
            }

            //Play sound effect
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}