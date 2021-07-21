using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallSound : MonoBehaviour
{
    public float _time = -1;

    IEnumerator Start()
    {
        yield return new WaitWhile(() => _time < 0);
        StartCoroutine(StartIE());
    }
    IEnumerator StartIE()
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
        Destroy(this);
    }
}