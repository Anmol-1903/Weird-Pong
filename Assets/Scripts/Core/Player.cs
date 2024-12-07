using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    [SerializeField] Material _trailMaterial;
    public Material GetTrailMaterial() { return _trailMaterial; }

    public int _score = 0, scene;

    [SerializeField] GameObject[] paddles;

    private void OnEnable()
    {
        UpdatePaddle();
        scene = SceneManager.GetActiveScene().buildIndex;
    }

    public void SetScore(int score)
    {
        _score = score;
        UpdatePaddle();
    }

    public void IncrementScore()
    {
        if (scene == 1)
            AudioManager.instance.PlayerScoreSFX();

        else if (scene == 2)
            if (name == "Player1")
                AudioManager.instance.PlayerScoreSFX();
            else
                AudioManager.instance.EnemyScoreSFX();

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
            if(scene == 3)
            {
                OnlineWinnerController.Instance.EndGame();
                return;
            }
            if(name == "Player1")
            {
                LocalWinner.Instance.Player1Win();
            }
            else if(name == "Player2")
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