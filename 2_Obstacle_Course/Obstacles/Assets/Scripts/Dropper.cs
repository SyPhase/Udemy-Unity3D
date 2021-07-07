using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float dropTime = 3f;
    MeshRenderer renderer;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();

        renderer.enabled = false;
        rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > dropTime)
        {
            Debug.Log("3 Seconds has passed");
            renderer.enabled = true;
            rigidbody.useGravity = true;
        }
    }
}