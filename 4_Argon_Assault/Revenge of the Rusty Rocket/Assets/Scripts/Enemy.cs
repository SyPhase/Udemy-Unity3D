using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"{this.name} Hit by {other.gameObject.name}");
        Destroy(this.gameObject);
    }
}