using System.IO;
using Photon.Pun;
using UnityEngine;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    Ball ball;
    PhotonView _pv;

    void Awake()
    {
        ball = GetComponent<Ball>();
        _pv = GetComponent<PhotonView>();
        ball.enabled = false;
    }

    [PunRPC]
    public void ActivateBall()
    {
        ball.enabled = true;
        Debug.Log("Match Started");
    }

    private void Start()
    {
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
    public void Update()
    {
        if(PhotonNetwork.CountOfPlayers < 2)
        {
            return;
        }
        for(int i=0; i < 2; i++)
        {
            if(PhotonNetwork.PlayerList[i].NickName != "Ready")
            {
                return;
            }
        }
        _pv.RPC("ActivateBall", RpcTarget.All);
    }
}