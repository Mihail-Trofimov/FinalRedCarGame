using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
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
        _body.transform.Rotate(0, 100.0f * Time.deltaTime, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !_action)
        {
            _action = true;
            DestroyHeal();
        }
        else if (other.tag == "CannonBall" && !_action)
        {
            _action = true;
            Destroy(other.gameObject);
            DestroyHeal();
        }
        else if (other.tag == "Player" && !_action)
        {
            _action = true;
            AudioSource _sound = Instantiate(_soundTake, transform.position, Quaternion.Euler(270f, 0f, 0f));
            _sound.GetComponent<CannonBallSound>()._time = _sound.clip.length;
            Instantiate(_effTake, transform.position, Quaternion.Euler(270f,0f,0f));
        }
    }
    void DestroyHeal()
    {
        AudioSource _sound = Instantiate(_soundDestroy, transform.position, Quaternion.Euler(270f, 0f, 0f));
        _sound.GetComponent<CannonBallSound>()._time = _sound.clip.length;
        Instantiate(_effDestroy, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(this);
    }
}
