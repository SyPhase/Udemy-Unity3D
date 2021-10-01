using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadLevel()
    {
        SceneManager.LoadScene(1); // if multiple levels, add variable to remember what level
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
        Time.timeScale = 1; // might be useless, test removing this then quitting from standalone build
    }
}