using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] Material _ownMaterial, _trailMaterial;
   
    public Material GetMaterial() { return _ownMaterial; }
    public Material GetTrailMaterial() { return _trailMaterial; }

    public int _score;

    [SerializeField] GameObject[] paddles;

    private void Start()
    {
        for (int i = 0; i < paddles.Length; i++)
        {
            paddles[i].SetActive(false);
        }
        paddles[0].SetActive(true);
    }
    public void IncrementScore()
    {
        _score++;
        UpdatePaddle();
    }
    private void UpdatePaddle()
    {
        for (int i = 0; i < paddles.Length; i++)
        {
            paddles[i].SetActive(false);
        }
        if (_score < 2)
        {
            paddles[_score].SetActive(true);
        }
        else
        {
            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                OnlineWinnerController.Instance.EndGame();
                return;
            }
            if(gameObject.name == "Player1")
            {
                LocalWinner.Instance.Player1Win();
            }
            else if(gameObject.name == "Player2")
            {
                LocalWinner.Instance.Player2Win();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            IncrementScore();
            other.GetComponent<Ball>().ResetBall();
        }
    }
}