using Photon.Pun;
using UnityEngine;

public class OnlineWinnerController : MonoBehaviourPun
{
    public static OnlineWinnerController Instance;

    [SerializeField] GameObject victoryPanel, defeatPanel;
    [SerializeField] private AudioClip win, lose;

    private void Awake()
    {
        Instance = this;
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    public void EndGame()
    {
        // Determine if this client is the one calling EndGame
        bool isWinner = PhotonNetwork.IsMasterClient; // Change this logic if not based on MasterClient

        // Notify all players with the result
        photonView.RPC("SyncDisplayResult", RpcTarget.All, isWinner);
    }

    [PunRPC]
    void SyncDisplayResult(bool isWinner)
    {
        FindObjectOfType<Ball>()?.gameObject.SetActive(false);
        if (isWinner)
        {
            victoryPanel.SetActive(true);
            defeatPanel.SetActive(false);
            AudioManager.instance.PlaySFX(win);
        }
        else
        {
            victoryPanel.SetActive(false);
            defeatPanel.SetActive(true);
            AudioManager.instance.PlaySFX(lose);
        }
    }
}
