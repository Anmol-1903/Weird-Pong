using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
public class LaunchManager : MonoBehaviourPunCallbacks
{
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
    }
    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected To photon Server");
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