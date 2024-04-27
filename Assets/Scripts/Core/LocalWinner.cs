using UnityEngine;
using UnityEngine.SceneManagement;
public class LocalWinner : MonoBehaviour
{
    public static LocalWinner Instance;

    [SerializeField] private GameObject winPanelP1, winPanelP2;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        winPanelP1.SetActive(false);
        winPanelP2.SetActive(false);
    }
    public void Player1Win()
    {
        winPanelP1.SetActive(true);
        StopGame();
    }
    public void Player2Win()
    {
        winPanelP2.SetActive(true);
        StopGame();
    }
    void StopGame()
    {
        FindObjectOfType<Ball>()?.gameObject.SetActive(false);
        FindObjectOfType<SinglePlayerController>()?.gameObject.SetActive(false);
        FindObjectOfType<LocalMultiplayerController>()?.gameObject.SetActive(false);
    }
    public void LoadLevel(int num)
    {
        SceneManager.LoadScene(num);
    }
}