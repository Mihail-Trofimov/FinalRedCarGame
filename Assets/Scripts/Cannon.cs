using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private bool isSeeYou;
    private bool isDeadZone;
    private Transform _player;
    private Vector3 _x;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private ParticleSystem _smoke;

    private float speedT;
    private float disT;
    private float agleT;

    private AudioSource _audioShot;
    void Start()
    {
        _audioShot = GetComponent<AudioSource>();
        isSeeYou = false;
        StartCoroutine(Shot());
    }
    void FixedUpdate()
    {
        if (!isSeeYou || isDeadZone)
        {
            transform.Rotate(0f, 20f * Time.deltaTime, 0f);
            if (isDeadZone)
            {
                if (Vector3.Distance(transform.position, _player.position) > 20)
                {
                    isDeadZone = false;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, _player.position) > 20)
            {
                isDeadZone = false;
                _x = new Vector3(_player.position.x, transform.position.y, _player.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_x - transform.position), Time.deltaTime * 8);
            }
            else
            {
                isDeadZone = true;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            }
        }
    }
    IEnumerator Shot()
    {

        while (this != null)
        {
            yield return new WaitForSeconds(5.0f);
            if (!isDeadZone && isSeeYou)
            {
                disT = Vector3.Distance(transform.position, _player.position);
                speedT = disT * 80f;
                agleT = AgleBalistic(disT, speedT);
                GameObject _newBall = Instantiate(_prefab, _shotPoint.position, Quaternion.Euler(_shotPoint.rotation.eulerAngles.x, agleT, _shotPoint.rotation.eulerAngles.z));
                _newBall.GetComponent<Rigidbody>().AddForce(transform.forward * speedT);
                _audioShot.Play();
                _smoke.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(5.0f);
        }
    }
    float AgleBalistic(float distance, float speedBullet)
    {
        //Находим велечину гравитации
        float gravity = Physics.gravity.magnitude;

        float discr = Mathf.Pow(speedBullet, 4) - 4 * (-gravity * gravity / 4) * (-distance * distance);
        //Время полёта
        float t = ((-speedBullet * speedBullet) - Mathf.Sqrt(discr)) / (-gravity * gravity / 2);
        t = Mathf.Sqrt(t);
        float th = gravity * t * t / 8;
        //Угол пушки
        float agle = 180 * (Mathf.Atan(4 * th / distance) / Mathf.PI);

        //Возрощаем угол
        return (agle);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isSeeYou = true;
            _player = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isSeeYou = false;
        }
    }
}