using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Time in seconds before the level is restarted")]
    [SerializeField] float loadDelay = 2; // time before restart
    [SerializeField] ParticleSystem crashVFX;

    bool isAlive = true;

    void OnTriggerEnter(Collider other)
    {
        CrashPlayer();
    }

    void CrashPlayer()
    {
        if (isAlive)
        {
            crashVFX.Play(); // adds explosion particles
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<PlayerControls>().enabled = false;
            GetComponentInChildren<Collider>().enabled = false;
            Invoke("ReloadLevel", loadDelay); // Invoke calls Method after delay
            isAlive = false;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}