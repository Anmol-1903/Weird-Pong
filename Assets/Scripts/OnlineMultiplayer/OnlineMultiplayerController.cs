using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
public class OnlineMultiplayerController : MonoBehaviour
{
    PlayerControl playerControl;
    PhotonView pv;

    float _vertica1;

    public bool _Ready;
    public float _Progress;

    [SerializeField] float _speed;
    [SerializeField] float _minY, _maxY;
    Image _readyImage;
    GameObject _readyScreen;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        playerControl = new PlayerControl();
        _Progress = 0;
        _Ready = false;
        _readyScreen = GameObject.FindGameObjectWithTag("BG");
        if (PhotonNetwork.IsMasterClient)
        {
            _readyImage = GameObject.FindGameObjectWithTag("P1").GetComponent<Image>();
        }
        else
        {
            _readyImage = GameObject.FindGameObjectWithTag("P2").GetComponent<Image>();
        }
    }
    private void OnEnable()
    {
        if (pv.IsMine)
        {
            playerControl.Enable();

            playerControl.SinglePlayer.Vertical.performed += Vertical_performed;
            playerControl.SinglePlayer.Vertical.canceled += Vertical_canceled;
        }
    }

    private void OnDisable()
    {
        if (pv.IsMine)
        {
            playerControl.Disable();

            playerControl.SinglePlayer.Vertical.performed -= Vertical_performed;
        }
    }
    private void Vertical_performed(CallbackContext obj)
    {
        _vertica1 = obj.ReadValue<float>();
    }
    private void Vertical_canceled(CallbackContext obj)
    {
        _vertica1 = 0;
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            if (_Ready)
            {
                _readyScreen.SetActive(false);

                transform.Translate(0, _vertica1 * _speed * Time.deltaTime, 0);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minY, _maxY), transform.position.z);
            }
            else
            {
                ReadyUp();
            }
        }
    }
    void ReadyUp()
    {
        if (!_Ready)
        {
            if (_vertica1 > 0)
            {
                _Progress += _vertica1 * Time.deltaTime / 2;
            }
            else if (_Progress > 0)
            {
                _Progress -= Time.deltaTime / 2;
            }
            Mathf.Clamp01(_Progress);
        }
        if (_Progress >= 1)
        {
            _Ready = true;
            PhotonNetwork.NickName = "Ready";
        }
        _readyImage.fillAmount = _Progress;
    }
}