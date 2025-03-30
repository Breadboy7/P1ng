using UnityEngine;
using UnityEngine.SceneManagement;

# if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MatchInstance");
    }

        public void ExitGame()
    {
        Debug.Log("Exiting game...");

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}