using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Time in seconds before the level is restarted")]
    [SerializeField] float loadDelay = 2; // time before restart
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " -- triggered by -- " + other.gameObject.name);
        Invoke("ReloadLevel", loadDelay); // Invoke calls Method after delay
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}