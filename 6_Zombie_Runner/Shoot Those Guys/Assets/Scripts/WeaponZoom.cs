using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    //[SerializeField] Camera camera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 25f;
    [SerializeField] float zoomedOutSense = 2f;
    [SerializeField] float zoomedInSense = 1f;

    RigidbodyFirstPersonController fpsController;
    Camera camera;

    bool isZoomedToggle = false;

    void Start()
    {
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
        camera = GetComponentInParent<Camera>();
    }

    void OnDisable()
    {
        ZoomOut();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (isZoomedToggle == false)
            {
                isZoomedToggle = true;
                ZoomIn();
            }
        }
        else
        {
            if (isZoomedToggle == true)
            {
                isZoomedToggle = false;
                ZoomOut();
            }
        }
    }

    void ZoomIn()
    {
        camera.fieldOfView = zoomedInFOV;
        fpsController.mouseLook.XSensitivity = zoomedInSense;
        fpsController.mouseLook.YSensitivity = zoomedInSense;
    }

    void ZoomOut()
    {
        camera.fieldOfView = zoomedOutFOV;
        fpsController.mouseLook.XSensitivity = zoomedOutSense;
        fpsController.mouseLook.YSensitivity = zoomedOutSense;
    }
}