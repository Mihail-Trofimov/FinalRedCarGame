using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone2End : MonoBehaviour
{

    [SerializeField] private Transform _lattice;
    [SerializeField] private Zone2Blue _blueScript;
    [SerializeField] private Zone2Green _greenScript;
    [SerializeField] private AudioSource _soundDoor;
    float _posUp;
    float _posDown;
    public bool _end;
    void Start()
    {
        _end = false;
        _posUp = _lattice.transform.localPosition.y;
        _posDown = _lattice.transform.localPosition.y - 20;
        StartCoroutine(Down());
    }

    IEnumerator Down()
    {
        yield return new WaitUntil(() => _blueScript.blueBox && _greenScript.greenBox);
        _soundDoor.Play();
        while (_posDown < _lattice.transform.localPosition.y)
        {
            _lattice.transform.localPosition = new Vector3(_lattice.transform.localPosition.x, _lattice.transform.localPosition.y - 5f * Time.deltaTime, _lattice.transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Up()
    {
        _end = true;
        _soundDoor.Play();
        while (_posUp > _lattice.transform.localPosition.y)
        {
            _lattice.transform.localPosition = new Vector3(_lattice.transform.localPosition.x, _lattice.transform.localPosition.y + 5f * Time.deltaTime, _lattice.transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
        Destroy(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Up());
        }
    }

}
