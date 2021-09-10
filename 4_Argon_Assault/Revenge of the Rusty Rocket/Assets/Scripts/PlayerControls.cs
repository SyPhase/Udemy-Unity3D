using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float moveSensitivity = 30f;
    [SerializeField] float xRange = 15f;
    [SerializeField] float yRange = 10f;

    [SerializeField] float positionPitchFactor = 0f;
    [SerializeField] float movementPitchFactor = -30f;
    [SerializeField] float movementRollFactor = -30f;

    float xRaw = 0f;
    float yRaw = 0f;

    private void OnEnable() // enables new input system
    {
        movement.Enable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float positionPitch = transform.localPosition.y * positionPitchFactor;
        float movementPitch = yRaw * movementPitchFactor;

        float pitch = positionPitch + movementPitch;
        float yaw = 0f;
        float roll = xRaw * movementRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xRaw = movement.ReadValue<Vector2>().x; // reads x-axis input from WASD (AD)
        yRaw = movement.ReadValue<Vector2>().y; // reads y-axis input from WASD (WS)

        float xOffset = xRaw * moveSensitivity * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yRaw * moveSensitivity * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
