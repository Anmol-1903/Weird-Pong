using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
public class LaunchManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _loadingScreen;
    private void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 10);
    }
    public void JoinRandomGame()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        _loadingScreen.SetActive(true);
    }
    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected To photon Server");
        _loadingScreen.SetActive(false);
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("Connection Failed");
        CreateAndJoin();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " Joined " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel(3);
    }

    private void CreateAndJoin()
    {
        string roomName = "RoomNo" + Random.Range(0, 10).ToString();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
}