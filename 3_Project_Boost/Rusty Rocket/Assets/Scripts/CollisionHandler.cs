using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //// PARAMATERS - for tuning, typically set in the editor
    [SerializeField] public float restartDelay = 2f;
    [SerializeField] AudioClip winLanding;
    [SerializeField] AudioClip crashExplosion;

    [SerializeField] ParticleSystem winLandingParticles;
    [SerializeField] ParticleSystem crashExplosionParticles;

    //// CACHE - references for readability or speed
    AudioSource audioSource;

    //// STATE - private instance (member) variables
    bool isAlive = true;
    bool collisionsActive = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        UseDebugKeys(); // debug/cheats
    }

    private void UseDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) // "L" load next level
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C)) // "c" disable/enable collisions
        {
            collisionsActive = !collisionsActive;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!isAlive || !collisionsActive) { return; }

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
        isAlive = false;
        audioSource.Stop();
        audioSource.PlayOneShot(winLanding); // adds party blower SFX on landing
        winLandingParticles.Play(); // adds confetti particles
        DisablePlayerControls();
        Invoke("LoadNextLevel", restartDelay);
    }

    void StartCrashSequence()
    {
        isAlive = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashExplosion); // adds explosion SFX on crash
        crashExplosionParticles.Play(); // adds explosion particles
        DisablePlayerControls();
        Invoke("ReloadLevel", restartDelay); // Invoke calls Method after delay
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
