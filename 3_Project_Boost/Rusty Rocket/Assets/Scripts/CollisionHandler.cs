using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //// PARAMATERS - for tuning, typically set in the editor
    [SerializeField] public float restartDelay = 2f;
    [SerializeField] AudioClip winLanding;
    [SerializeField] AudioClip crashExplosion;

    //// CACHE - references for readability or speed
    AudioSource audioSource;

    //// STATE - private instance (member) variables
    bool isAlive = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                // Debug.Log("Collision: Friendly");
                break;
            case "Finish":
                StartLandingSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartLandingSequence()
    {
        if (isAlive)
        {
            isAlive = false;
            // TODO add party blower SFX on landing
            audioSource.PlayOneShot(winLanding);
            // TODO add confetti particle effect on landing
            DisablePlayerControls();
            Invoke("LoadNextLevel", restartDelay);
        }
    }

    void StartCrashSequence()
    {
        if (isAlive)
        {
            isAlive = false;
            // TODO add explosion SFX on crash
            audioSource.PlayOneShot(crashExplosion);
            // TODO add explosion particle effect on crash
            DisablePlayerControls();
            Invoke("ReloadLevel", restartDelay); // Invoke calls Method after delay
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void DisablePlayerControls()
    {
        GetComponent<Movement>().enabled = false;
    }
}
