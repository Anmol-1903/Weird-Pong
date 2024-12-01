using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonManager Instance;

    public bool gameBegun = false;

    Ball ball;
    PhotonView _pv;
    OnlineMultiplayerController[] Players;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        ball = GetComponent<Ball>();
        _pv = GetComponent<PhotonView>();
        ball.enabled = false;
    }

    [PunRPC]
    public void ActivateBall()
    {
        Players = FindObjectsOfType<OnlineMultiplayerController>();
        foreach (var p in Players)
        {
            p.CloseReadyScreen();
            Debug.Log($"Match Started: {p.name}");
        }
        ball.enabled = true;
        gameBegun = true;
    }

    private void Start()
    {
        // Set "Ready" property to false initially
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", false } });

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(Path.Combine("Player", "OnlinePlayerBlue"), new Vector3(-9, 0, 0), Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(Path.Combine("Player", "OnlinePlayerRed"), new Vector3(9, 0, 0), Quaternion.identity);
            }
        }
    }

    public void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey("Ready"))
        {
            Debug.Log($"{targetPlayer.NickName} Ready Status: {(bool)changedProps["Ready"]}");

            // Check if all players are ready
            CheckReadyStatus();
        }
    }

    private void CheckReadyStatus()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            Debug.Log("Not enough players.");
            return;
        }

        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if ((!player.CustomProperties.ContainsKey("Ready") || !(bool)player.CustomProperties["Ready"]) && !ball.enabled)
            {
                Debug.Log("Both not ready");
                return;
            }
        }

        Debug.Log("Both Ready");
        _pv.RPC("ActivateBall", RpcTarget.All);
    }

    public void SetReadyStatus(bool isReady)
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Ready", isReady } });
    }

    // Empty implementations of IInRoomCallbacks methods
    public void OnPlayerEnteredRoom(Player newPlayer) { }
    public void OnPlayerLeftRoom(Player otherPlayer) { }
    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged) { }
    public void OnMasterClientSwitched(Player newMasterClient) { }
}
