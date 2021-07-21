using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private WheelCollider[] whellCols;
    [SerializeField] private Transform[] whellMeshs;
    [SerializeField] private Slider slider;
    [SerializeField] public Text hpText;
    [SerializeField] private AudioSource _beepAudio;
    [SerializeField] private AudioSource _cannonBallAudio;
    [SerializeField] private GameMenu _menuScript;

    public int plHP = 5;
    public float nitro = 100;
    public bool beepBeep;

    private bool _beep;
    private bool beepDown;
    private bool isNitroPressed;
    private bool isJumpPressed;
    private bool flying;
    private bool itsAtrap;
    private bool isStops;

    private bool inBonus;
    private bool inHeal;
    private bool inPushpin;
    private bool inEnemy;
    private bool inCannonBall;

    void Start()
    {
        inBonus = false;
        inHeal = false;
        inPushpin = false;
        inEnemy = false;
        inCannonBall = false;

        _beep = false;
        beepBeep = false;
        beepDown = false;

        isStops = false;
        flying = false;
        itsAtrap = false;
        _rb = GetComponent<Rigidbody>();

        slider.value = nitro;
        hpText.text = plHP.ToString();
    }
    void Update()
    {
        if (!_menuScript._menu)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            //if (Input.GetAxis("Jump") > 0)
            {
                isJumpPressed = true;
            }
            if (Input.GetAxis("Fire3") > 0)
            {
                isNitroPressed = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                beepDown = true;
                _beepAudio.Play();
                if (!_beep)
                {
                    StartCoroutine(BeepBeep());
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                beepDown = false;
                _beepAudio.Stop();

            }
        }
        else
        {
            beepDown = false;
            _beepAudio.Stop();
        }
    }

    void FixedUpdate()
    {
        if (!_menuScript._menu)
        {
            whellCols[0].steerAngle = 30f * Input.GetAxis("Horizontal");
            whellCols[1].steerAngle = 30f * Input.GetAxis("Horizontal");

            whellMeshs[0].rotation = Quaternion.Euler(whellMeshs[2].rotation.eulerAngles.x, whellMeshs[2].rotation.eulerAngles.y, whellMeshs[2].rotation.eulerAngles.z - 30f * Input.GetAxis("Horizontal"));
            whellMeshs[1].rotation = Quaternion.Euler(whellMeshs[2].rotation.eulerAngles.x, whellMeshs[2].rotation.eulerAngles.y, whellMeshs[2].rotation.eulerAngles.z - 30f * Input.GetAxis("Horizontal"));


            if (isJumpPressed && !flying && !itsAtrap)
            {
                _rb.AddForce(Vector3.up * 200000f * Time.deltaTime);
                _rb.MoveRotation(Quaternion.Euler(_rb.rotation.eulerAngles.x, _rb.rotation.eulerAngles.y, 0));
                isJumpPressed = false;
            }
            if (isNitroPressed && !flying && !itsAtrap && !isStops)
            {
                if (nitro > 0)
                {
                    _rb.AddForce(transform.forward * 10000f * Time.deltaTime * Input.GetAxis("Vertical"));
                    nitro -= 20 * Time.deltaTime;
                    slider.value = nitro;
                }
                isNitroPressed = false;
            }
            if (Input.GetAxis("Vertical") > 0 && !itsAtrap || Input.GetAxis("Vertical") < 0 && !itsAtrap && !flying)
            {
                for (int i = 0; i < whellCols.Length; i++)
                {
                    isStops = false;
                    whellCols[i].brakeTorque = 0;
                    whellCols[i].motorTorque = 50f * Input.GetAxis("Vertical") * Time.deltaTime;
                    _rb.AddForce(transform.forward * 1000f * Time.deltaTime * Input.GetAxis("Vertical"));
                }
            }
            else
            {
                isStops = true;
                for (int i = 0; i < whellCols.Length; i++)
                {
                    whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 1000f;
                }
            }
        }
    }
    IEnumerator BeepBeep()
    {
        _beep = true;
        int _time = 0;
        while (beepDown && _time <= 150)
        {
            _time += 1;
            yield return new WaitForSeconds(0.01f);
        }
        if (_time < 40 || _time > 150)
        {
            _beep = false;
            yield break;
        }
        _time = 0;
        while (!beepDown && _time <= 60)
        {
            _time += 1;
            yield return new WaitForSeconds(0.01f);
        }
        if (_time < 5 || _time > 60)
        {
            _beep = false;
            yield break;
        }
        _time = 0;
        while (beepDown && _time <= 150)
        {
            _time += 1;
            yield return new WaitForSeconds(0.01f);
        }
        if (_time < 40 || _time > 150)
        {
            _beep = false;
            yield break;
        }
        beepBeep = true;
        yield return new WaitForSeconds(1f);
        if (beepBeep)
        {
            beepBeep = false;
        }
        _beep = false;
    }
    IEnumerator Trap()
    {
        _rb.AddForce(Vector3.up * 50000f * Time.deltaTime);
        _rb.MoveRotation(Quaternion.Euler(_rb.rotation.eulerAngles.x, _rb.rotation.eulerAngles.y, 0));
        itsAtrap = true;
        while (_rb.velocity.magnitude>1f)
        {
            for (int i = 0; i < whellCols.Length; i++)
            {
                whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 10000f * Time.deltaTime;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < whellCols.Length; i++)
        {
            whellCols[i].brakeTorque = 0;
        }
        flying = false;
        itsAtrap = false;
    }
    IEnumerator boolBonus()
    {
        yield return new WaitForFixedUpdate();
        inBonus = false;
    }
    IEnumerator boolHeal()
    {
        yield return new WaitForFixedUpdate();
        inHeal = false;
    }
    IEnumerator boolPushpin()
    {
        yield return new WaitForFixedUpdate();
        inPushpin = false;
    }
    IEnumerator boolEnemy()
    {
        yield return new WaitForFixedUpdate();
        inEnemy = false;
    }
    IEnumerator boolCannonBall()
    {
        yield return new WaitForFixedUpdate();
        inCannonBall = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bonus" && !inBonus)
        {
            inBonus = true;
            Destroy(other.gameObject);
            nitro += 200;
            slider.value = nitro;
            StartCoroutine(boolBonus());
        }
        else if (other.tag == "Heal" && !inHeal)
        {
            inHeal = true;
            Destroy(other.gameObject);
            plHP++;
            hpText.text = plHP.ToString();
            StartCoroutine(boolHeal());
        }
        else if (other.tag == "Pushpin" && !inPushpin)
        {
            inPushpin = true;
            Destroy(other.gameObject);
            StartCoroutine(Trap());
            StartCoroutine(boolPushpin());
        }
        if (other.tag == "Enemy" && !inEnemy)
        {
            inEnemy = true;
            _rb.GetComponent<Rigidbody>().AddForce(Vector3.up * 70f);
            _rb.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(transform.position, other.transform.position, 20f) * -100f);
            StartCoroutine(boolEnemy());
        }
        if (other.tag == "CannonBall" && !inCannonBall)
        {
            inCannonBall = true;
            _cannonBallAudio.Play();
            Destroy(other.gameObject);
            plHP--;
            hpText.text = plHP.ToString();
            StartCoroutine(boolCannonBall());
        } 
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Level")
        {
            flying = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Level")
        {
            flying = true;
        }
    }

}