using System.IO;
using Photon.Pun;
using UnityEngine;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _ball;

    void Awake()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _ball.SetActive(false);
    }
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
    public void Update()
    {
        if(_ball.activeInHierarchy)
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
        ActivateBall();
    }
}