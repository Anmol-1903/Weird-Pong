using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class OnlineMultiplayerController : MonoBehaviourPun
{
    PlayerControl playerControl;
    PhotonView pv;

    float _vertical;

    public bool _Ready;
    public float _Progress;

    [SerializeField] float _speed;
    [SerializeField] float _minY, _maxY;
    [SerializeField] Image player1Slider;
    [SerializeField] Image player2Slider;
    GameObject _readyScreen;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        playerControl = new PlayerControl();
        _Progress = 0;
        _Ready = false;
        _readyScreen = GameObject.FindGameObjectWithTag("BG");

        // Find sliders for both players
        player1Slider = GameObject.FindGameObjectWithTag("P1").GetComponent<Image>();
        player2Slider = GameObject.FindGameObjectWithTag("P2").GetComponent<Image>();
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
            playerControl.SinglePlayer.Vertical.canceled -= Vertical_canceled;
        }
    }

    private void Vertical_performed(CallbackContext obj)
    {
        _vertical = obj.ReadValue<float>();
    }

    private void Vertical_canceled(CallbackContext obj)
    {
        _vertical = 0;
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            if (PhotonManager.Instance.gameBegun)
            {
                float updatedY = transform.position.y + (_speed * _vertical * Time.deltaTime);
                updatedY = Mathf.Clamp(updatedY, _minY, _maxY);

                transform.position = new Vector3(transform.position.x, updatedY, transform.position.z);

                // Update the player's position in CustomProperties
                UpdatePositionCustomProperty();
            }
            else
            {
                UpdateProgress();

                // Sync progress to all clients
                pv.RPC("SyncProgress", RpcTarget.All, PhotonNetwork.IsMasterClient, _Progress);

                // Update custom property for ready status
                if (_Ready)
                {
                    PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", _Ready } });
                }
            }
        }
    }

    void UpdateProgress()
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
            _Progress = Mathf.Clamp01(_Progress);
        }

        if (_Progress >= 1 && !_Ready)
        {
            _Ready = true;
        }
    }

    private void UpdatePositionCustomProperty()
    {
        // Save the player's current position in the custom properties
        Vector3 currentPosition = transform.position;
        int currentScore = GetComponent<Player>().GetScore();

        // Use the actor number as the key for storing the position
        string positionKey = $"LastPlayerPosition_{PhotonNetwork.LocalPlayer.ActorNumber}";
        string scoreKey = $"LastPlayerScore_{PhotonNetwork.LocalPlayer.ActorNumber}";

        PhotonNetwork.LocalPlayer.SetCustomProperties(
            new ExitGames.Client.Photon.Hashtable { { positionKey, currentPosition } });
        PhotonNetwork.LocalPlayer.SetCustomProperties(
            new ExitGames.Client.Photon.Hashtable { { scoreKey, currentScore } });

    }

    [PunRPC]
    public void CloseReadyScreen()
    {
        _readyScreen.SetActive(false);
    }

    [PunRPC]
    void SyncProgress(bool isPlayer1, float progress)
    {
        // Update the correct slider based on player
        if (isPlayer1)
        {
            player1Slider.fillAmount = progress;
        }
        else
        {
            player2Slider.fillAmount = progress;
        }
    }
}
