using UnityEngine;
using UnityEngine.SceneManagement;
public class LocalWinner : MonoBehaviour
{
    public static LocalWinner Instance;

    [SerializeField] private GameObject winPanelP1, winPanelP2;
    [SerializeField] private AudioClip win, lose;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        winPanelP1?.SetActive(false);
        winPanelP2?.SetActive(false);
    }
    public void Player1Win()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            AudioManager.instance.PlaySFX(win);

        winPanelP1?.SetActive(true);
        StopGame();
    }
    public void Player2Win()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
            AudioManager.instance.PlaySFX(lose);

        winPanelP2?.SetActive(true);
        StopGame();
    }
    void StopGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            AudioManager.instance.PlaySFX(win);

        FindObjectOfType<Ball>()?.gameObject.SetActive(false);
        FindObjectOfType<SinglePlayerController>()?.gameObject.SetActive(false);
        FindObjectOfType<LocalMultiplayerController>()?.gameObject.SetActive(false);
    }
    public void LoadLevel(int num)
    {
        SceneManager.LoadScene(num);
    }
}