using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Transform _body;
    [SerializeField] private GameObject _effDestroy;
    [SerializeField] private GameObject _effTake;
    [SerializeField] private AudioSource _soundTake;
    [SerializeField] private AudioSource _soundDestroy;
    private bool _action;
    void Start()
    {
        _action = false;
        _body = transform.Find("Body").transform;
    }
    void FixedUpdate()
    {
        transform.Rotate(0f, 100f * Time.deltaTime, 0f);
        //_body.transform.localRotation = Quaternion.Euler(30f, _body.transform.localRotation.y + 100f * Time.deltaTime, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CannonBall")
        {
            _action = true;
            Destroy(other.gameObject);
            AudioSource _sound = Instantiate(_soundDestroy, transform.position, Quaternion.Euler(270f, 0f, 0f));
            _sound.GetComponent<CannonBallSound>()._time = _sound.clip.length;
            Instantiate(_effDestroy, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
            Destroy(gameObject);
            Destroy(this);
        }
        else if (other.tag == "Enemy" && !_action || other.tag == "Player" && !_action)
        {
            _action = true;
            AudioSource _sound = Instantiate(_soundTake, transform.position, Quaternion.Euler(270f, 0f, 0f));
            _sound.GetComponent<CannonBallSound>()._time = _sound.clip.length;
            Instantiate(_effTake, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.Euler(270f, 0f, 0f));
            Destroy(this);
        }
    }
}
