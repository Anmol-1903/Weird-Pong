using UnityEngine;
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
        paddles[_score].SetActive(true);
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