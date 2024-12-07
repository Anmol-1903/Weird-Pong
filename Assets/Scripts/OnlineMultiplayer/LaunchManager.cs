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
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        CreateAndJoin();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(3);
    }
    private void CreateAndJoin()
    {
        string roomName = "RoomNo" + Random.Range(0, 1000).ToString();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
}