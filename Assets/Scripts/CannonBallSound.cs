using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallSound : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        Destroy(this);
    }
}
