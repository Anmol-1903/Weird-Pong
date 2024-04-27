using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
public class OnlineMultiplayerController : MonoBehaviour
{
    PlayerControl playerControl;
    PhotonView pv;

    float _vertical;

    public bool _Ready;
    public float _Progress, _otherProgress;

    [SerializeField] float _speed;
    [SerializeField] float _minY, _maxY;
    Image _readyImage;
    Image _otherImage;
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
            _otherImage = GameObject.FindGameObjectWithTag("P2").GetComponent<Image>();
            PhotonNetwork.NickName = "Player1";
        }
        else
        {
            _readyImage = GameObject.FindGameObjectWithTag("P2").GetComponent<Image>();
            _otherImage = GameObject.FindGameObjectWithTag("P1").GetComponent<Image>();
            PhotonNetwork.NickName = "Player2";
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
        _vertical = obj.ReadValue<float>();
    }
    [PunRPC]
    void UpdatePaddlePosition(float vertical)
    {
        _vertical = vertical;
    }
    private void Vertical_canceled(CallbackContext obj)
    {
        _vertical = 0;
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            MovePaddle();
            pv.RPC("UpdatePaddlePosition", RpcTarget.All, _vertical);

        }
    }
    void MovePaddle()
    {
        if (_Ready)
        {
            for (int i = 0; i < 2; i++)
            {
                if (PhotonNetwork.PlayerList[i].NickName != "Ready")
                {
                    return;
                }
            }
            _readyScreen.SetActive(false);

            transform.Translate(Vector3.up * _vertical * _speed * Time.deltaTime, Space.World);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minY, _maxY), transform.position.z);
        }
        else
        {
            ReadyUp();
        }
    }
    void ReadyUp()
    {
        if (!_Ready)
        {
            if (_vertical > 0)
            {
                _Progress += _vertical * Time.deltaTime / 2;
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
            Debug.Log(PhotonNetwork.NickName + " is Ready");
            PhotonNetwork.NickName = "Ready";
        }
        _readyImage.fillAmount = _Progress;
        _otherImage.fillAmount = _otherProgress;
    }
}