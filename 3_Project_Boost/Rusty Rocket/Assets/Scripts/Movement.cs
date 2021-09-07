using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //// PARAMATERS - for tuning, typically set in the editor
    [SerializeField] float upwardThrust = 2f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip thrustEngine;

    [SerializeField] ParticleSystem centerThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    //// CACHE - references for readability or speed
    Rigidbody rb;
    AudioSource audioSource;

    //// STATE - private instance (member) variables
    // none currently: eg bool isAlive;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(0, upwardThrust, 0);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustEngine); // references the Thrust SFX only when thrusting
        }
        if (!centerThrustParticles.isPlaying)
        {
            centerThrustParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        centerThrustParticles.Stop();
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.D)))
        {
            StartRotatingLeft();
        }
        else if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.A)))
        {
            StartRotatingRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartRotatingLeft()
    {
        ApplyRotation(rotationThrust);

        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    private void StartRotatingRight()
    {
        ApplyRotation(-rotationThrust);

        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    private void StopRotating()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
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
