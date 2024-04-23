using System;
using System.IO;
using Photon.Pun;
using UnityEngine;
public class PhotonManager : MonoBehaviour
{
    [SerializeField] GameObject _ball;
    bool gameStarted = false;

    private void ActivateBall()
    {
        _ball.SetActive(true);
    }

    private void Start()
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
    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && !gameStarted)
        {
            for (int i = 0; i < PhotonNetwork.CountOfPlayersInRooms; i++)
            {
                if (PhotonNetwork.PlayerList[i].NickName != "Ready")
                {
                    break;
                }
                gameStarted = true;
            }
            ActivateBall();
        }
    }
}