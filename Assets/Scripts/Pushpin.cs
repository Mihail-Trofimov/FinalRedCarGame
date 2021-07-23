using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushpin : MonoBehaviour
{
    [SerializeField] private AudioSource _trupAudio;
    private bool _active;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CannonBall")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Destroy(this);
        }
        else if (other.tag == "Enemy" && !_active || other.tag == "Player" && !_active)
        {
            _active = true;
            AudioSource _sound = Instantiate(_trupAudio, transform.position, Quaternion.Euler(0f, 0f, 0f));
            _sound.GetComponent<CannonBallSound>()._time = _sound.clip.length;
        }
    }
}
