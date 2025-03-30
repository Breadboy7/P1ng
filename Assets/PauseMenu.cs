using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; // reference to check if the game is paused
    public GameObject pauseMenuUI; // reference to the pause menu UI

    void Start(){
        Resume();
    }
    // Update is called once per frame
    void Update()
    {
        // check if the ESC key is pressed!
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // hide the pause menu
        Time.timeScale = 1f; // set the time scale to normal
        isPaused = false; // set the isPaused to false
    }

        public void Pause()
    {
        pauseMenuUI.SetActive(true); // hide the pause menu
        Time.timeScale = 0f; // set the time scale to normal
        isPaused = true; // set the isPaused to false

        Cursor.lockState = CursorLockMode.None; // unlock the cursor
        Cursor.visible = true; // make the cursor visible
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // set the time scale to normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reload the current scene 
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void ExitGame()
    {
        Time.timeScale = 1f; // set the time scale to normal
        SceneManager.LoadScene("MainMenu"); // load the main menu scene
    }
}