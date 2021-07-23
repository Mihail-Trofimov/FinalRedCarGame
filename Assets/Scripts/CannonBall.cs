using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private AudioClip _audioClipSand;
    [SerializeField] private AudioClip _audioClipObject;
    [SerializeField] private AudioClip _audioClipEasterCakes;
    [SerializeField] private GameObject _effDestroy;
    [SerializeField] private GameObject _effSand;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level" || other.tag == "Object" || other.tag == "EasterCakes" || other.tag == "Enemy" || other.tag == "Truck" || other.tag == "Wall")
        {
            GameObject _obj = new GameObject();
            _obj.transform.position = gameObject.transform.position;
            AudioSource _sandAudio = _obj.AddComponent<AudioSource>();
            _sandAudio.outputAudioMixerGroup = _audioMixer;
            if (other.tag == "Level")
            {
                _sandAudio.clip = _audioClipSand;
                
                Instantiate(_effSand, transform.position, Quaternion.Euler(-90f, 0f, 0f));

            }
            else if (other.tag == "Object" || other.tag == "Enemy" || other.tag == "Truck" || other.tag == "Wall")
            {
                _sandAudio.clip = _audioClipObject;
            }
            else if (other.tag == "EasterCakes")
            {
                _sandAudio.clip = _audioClipEasterCakes;
            }
            _sandAudio.minDistance = 1f;
            _sandAudio.maxDistance = 100f;
            _sandAudio.volume = 0.8f;
            _sandAudio.spatialBlend = 1f;
            _sandAudio.spread = 180f;
            _sandAudio.rolloffMode = AudioRolloffMode.Linear;
            CannonBallSound _tDel = _obj.AddComponent<CannonBallSound>();
            _sandAudio.Play();
            _tDel._time = _sandAudio.clip.length;
            Instantiate(_effDestroy, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(this);
        }
        else if (other.tag == "Player")
        {
            Instantiate(_effDestroy, transform.position, transform.rotation);
        }
    }
}
