using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("New Input System")]
    [SerializeField] InputAction movement; // holds input methods (in inspector) (eg. axis, mouse button, etc)
    [SerializeField] InputAction fire;

    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves (up, down, left, right) based on user input")]
        [SerializeField] float moveSensitivity = 30f;
    [Tooltip("Max horizontal distance for player bounds (x-axis, semetrical from center)")]
        [SerializeField] float xRange = 15f;
    [Tooltip("Max vertical distance for player bounds (y-axis, semetrical from center)")]
        [SerializeField] float yRange = 10f;
    
    [Header("Ship position based tuning")]
    [Tooltip("ship pitch factor depending on position on screen")]
        [SerializeField] float positionPitchFactor = 1f;
    [Tooltip("ship yaw factor depending on position on screen")]
        [SerializeField] float positionYawFactor = 1f;

    [Header("Player movement based tuning")]
    [Tooltip("ship pitch factor depending on player movement")]
        [SerializeField] float movementPitchFactor = -30f;
    [Tooltip("ship roll factor depending on player movement")]
        [SerializeField] float movementRollFactor = -30f;

    [Header("Array: All Laser Guns")]
    [Tooltip("holds all laser's Partical Effects on ship for shooting")]
        [SerializeField] GameObject[] lasers; // new array of Game Objects (set in inspector to be laser right and laser left)

    float xRaw = 0f;
    float yRaw = 0f;

    private void OnEnable() // enables new input system
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable() // disables new input system
    {
        movement.Disable();
        fire.Disable();
    }

    void Update()
    {
        ProcessTranslation(); // user input moves ship horizontally and vertically
        ProcessRotation(); // user input and ship position rotate ship (pitch, yaw, roll)
        ProcessFiring(); // user input starts and stops firing process
    }

    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5) // when fire button is pressed (more than 50%) then fire
        {
            SetLasersActive(true); // passes in true
        }
        else
        {
            SetLasersActive(false); // passes in false
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers) // goes through each item in array "lasers," of type GameObject, with each instance called "laser"
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission; // calls "emission" under "particle system" under "laser"
            emissionModule.enabled = isActive; // sets emission to isActive (passed in)
        }
    }

    private void ProcessRotation()
    {
        float positionPitch = transform.localPosition.y * -positionPitchFactor; // product y position and pitch (tuning) factor
        float movementPitch = yRaw * movementPitchFactor; // product y input and pitch (tuning) factor

        float positionYaw = transform.localPosition.x * positionYawFactor; // yaw based on position on x-axis

        float pitch = positionPitch + movementPitch; // ship's pitch sum
        float yaw = positionYaw; // ship yaw
        float roll = xRaw * movementRollFactor; // ship's roll product
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xRaw = movement.ReadValue<Vector2>().x; // reads x-axis input from WASD (AD)
        yRaw = movement.ReadValue<Vector2>().y; // reads y-axis input from WASD (WS)

        float xOffset = xRaw * moveSensitivity * Time.deltaTime; // amount to be moved by (offset)
        float rawXPos = transform.localPosition.x + xOffset; // current position + offset
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); // clamped to screen bounds

        float yOffset = yRaw * moveSensitivity * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z); // set new position
    }
}
