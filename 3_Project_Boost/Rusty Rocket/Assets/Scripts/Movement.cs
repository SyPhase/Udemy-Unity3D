using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float upwardThrust = 1f;
    [SerializeField] float rotationThrust = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
}
