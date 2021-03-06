using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;
    [SerializeField] private Transform _btnObj;
    private bool isPressed;
    private bool btnDown;
    private float _o;
    void Start()
    {
        isPressed = false;
        btnDown = false;
        _o = _btnObj.localPosition.y + 0f;
        _sphere.SetActive(false);
    }
    void Update()
    {
        if (isPressed)
        {
            if ((_o - 0.2f) < _btnObj.localPosition.y && !btnDown)
            {
                _btnObj.transform.localPosition = new Vector3(_btnObj.localPosition.x, _btnObj.localPosition.y - 1f * Time.deltaTime, _btnObj.localPosition.z);
            }
            else if (_o >= _btnObj.localPosition.y)
            {
                btnDown = true;
                _btnObj.transform.localPosition = new Vector3(_btnObj.localPosition.x, _btnObj.localPosition.y + 1f * Time.deltaTime, _btnObj.localPosition.z);
            }
            else
            {
                btnDown = false;
                isPressed = false;
                _sphere.SetActive(true);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isPressed)
        {
            Debug.Log("Start tetris press");
            isPressed = true;
        }
    }
}
