using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] Material _ownMaterial, _trailMaterial;
   
    public Material GetMaterial() { return _ownMaterial; }
    public Material GetTrailMaterial() { return _trailMaterial; }

    public int _score = 0;

    [SerializeField] GameObject[] paddles;

    private void Start()
    {
        UpdatePaddle();
    }

    public void SetScore(int score)
    {
        _score = score;
        UpdatePaddle();
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
        if (_score < 10)         // MAX SCORE
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
    public int GetScore()
    {
        return _score;
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