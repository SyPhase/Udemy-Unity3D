using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 60f;
    [SerializeField] float intensityAmount = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FlashlightSystem flashlight = other.GetComponentInChildren<FlashlightSystem>();
            flashlight.RestoreLightAngle(restoreAngle);
            flashlight.RestoreLightIntensity(intensityAmount);
            Destroy(gameObject);
        }
    }
}