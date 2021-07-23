using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterCakes : MonoBehaviour
{
    [SerializeField] private GameObject _effDestroy;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CannonBall")
        {
            Instantiate(_effDestroy, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.Euler(270f, 0f, 0f));
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
