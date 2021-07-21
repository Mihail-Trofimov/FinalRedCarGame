using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterCakes : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CannonBall")
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
