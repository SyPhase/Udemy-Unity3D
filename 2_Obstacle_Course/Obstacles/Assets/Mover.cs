using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    [SerializeField] float moveSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xMovementSpeed = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zMovementSpeed = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(xMovementSpeed, 0, zMovementSpeed);
    }
}
