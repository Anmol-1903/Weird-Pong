using UnityEngine;
public class Player : MonoBehaviour
{
    int score;
    [SerializeField] GameObject[] _meshes;
    void Start()
    {
        score = 0;
    }
    public void ReInitialize()
    {
        for(int i = 0; i < _meshes.Length; i++)
        {
            _meshes[i].SetActive(false);
        }
        _meshes[score].SetActive(true);
    }
    public int GetScore()
    {
        return score;
    }
}