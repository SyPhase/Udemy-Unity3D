using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float upwardThrust = 1f;
    [SerializeField] float rotationThrust = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(0, upwardThrust, 0);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D)))
        {
            ApplyRotation(rotationThrust);
        }
        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A)))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation to manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation to allow physics interactions
    }


    ////////// EXPERIMENT //////////
        // causes rocket to not rotate on contact
    /*
    void OnCollisionStay(Collision other) // EXPERIMENT WITH PHYSICS
    {
        rb.freezeRotation = true; // Freezing rotation to manually rotate
    }
    void OnCollisionExit(Collision other)
    {
        rb.freezeRotation = false; // Unfreezing rotation to allow physics interactions
    }
    */    
}
