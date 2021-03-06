using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BlueCar : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _loopWPSObj;
    [SerializeField] private AudioSource _crashAudio;

    private PlayerMovements _playerScript;
    private Transform[] _loopWPS;
    private NavMeshAgent _enemy;
    private Rigidbody _rb;
    private bool stop;
    private int currentWP;
    private Vector3 _target;
    private float _speed;
    private Animator animator;

    public bool _atack;
    void Start()
    {
        _atack = false;
        _playerScript = _player.GetComponent<PlayerMovements>();
        animator = GetComponent<Animator>();
        animator.SetBool("isAttack", false);
        animator.SetBool("isDead", false);

        _loopWPS = new Transform[_loopWPSObj.childCount];
        for (int i = 0; i < _loopWPS.Length; i++)
        {
            _loopWPS[i] = _loopWPSObj.GetChild(i);
        }
        currentWP = 0;
        stop = false;
        _enemy = GetComponent<NavMeshAgent>();
        _target = _loopWPS[currentWP].position;
        _rb = GetComponent<Rigidbody>();
        _speed = 6f;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) >= 30f && !stop)
        {
            _atack = false;
            animator.SetBool("isAttack", false);
            animator.SetBool("isDead", false);
            _enemy.speed = _speed;
            _target = _loopWPS[currentWP].position;
            if (!_enemy.hasPath)
            {
                currentWP += 1;
                if (currentWP >= _loopWPS.Length)
                {
                    currentWP = 0;
                }
            }
        }
        else if(Vector3.Distance(transform.position, _player.transform.position) < 30f && !stop)
        {
            _atack = true;
            animator.SetBool("isAttack", true);
            animator.SetBool("isDead", false);
            _enemy.speed = _speed + 30f / Vector3.Distance(transform.position, _player.transform.position);
            _target = _player.transform.position;
        }
        else if (stop)
        {
            animator.SetBool("isDead", true);
            _enemy.speed = 0f;
        }
        _enemy.SetDestination(_target);
    }

    IEnumerator AtackIE()
    {
        _crashAudio.Play();
        stop = true;
        yield return new WaitForSeconds(7f);
        stop = false;
    }
    IEnumerator NitroIE()
    {
        _speed += 20f;
        yield return new WaitForSeconds(8f);
        _speed -= 20f;
    }
    IEnumerator Trap()
    {
        stop = true;
        yield return new WaitForSeconds(6f);
        stop = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!stop)
            {
                _playerScript.plHP -= 1;
                _playerScript.hpText.text = _playerScript.plHP.ToString();
                StartCoroutine(AtackIE());
            }
        }
        else if (other.tag == "Bonus")
        {
            StartCoroutine(NitroIE());
            Destroy(other.gameObject);
        }
        else if (other.tag == "Pushpin")
        {
            Destroy(other.gameObject);
            StartCoroutine(Trap());
        }
        else if (other.tag == "CannonBall")
        {
            StartCoroutine(Trap());
        }
    }
}
