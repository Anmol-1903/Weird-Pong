using Photon.Pun;
using UnityEngine;
public class OnlineWinnerController : MonoBehaviour
{
    public static OnlineWinnerController Instance;

    [SerializeField] GameObject victoryPanel, defeatPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }
    public void DisplayResult(bool isVictory)
    {
        if (isVictory)
        {
            victoryPanel.SetActive(true);
            defeatPanel.SetActive(false);
        }
        else
        {
            victoryPanel.SetActive(false);
            defeatPanel.SetActive(true);
        }
    }
    [PunRPC]
    void SyncDisplayResult(bool isVictory)
    {
        DisplayResult(isVictory);
    }
    public void EndGame()
    {
        //syncronization
    }
}