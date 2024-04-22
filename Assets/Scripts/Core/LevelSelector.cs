using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    [SerializeField] Transform _container;

    [SerializeField] float[] _positions;

    /*
    0 - single
    1 - local
    2 - online
     */

    public int _levelIndex;
    private void Awake()
    {
        _levelIndex = 0;
    }
    public void NextLevel()
    {
        _levelIndex = (_levelIndex + 1) % 3;
    }
    public void PreviousLevel()
    {
        _levelIndex--;
        if (_levelIndex < 0 )
            _levelIndex = 2;
    }
    public void PlayGame()
    {
        if (_levelIndex != 2)
        {
            SceneManager.LoadScene(_levelIndex + 1);
        }
        else
        {
            FindObjectOfType<LaunchManager>().JoinRandomGame();
        }
    }
    private void FixedUpdate()
    {
        _container.position = Vector3.Lerp(_container.position, 
            new Vector3(_container.position.x, _positions[_levelIndex], _container.position.z), 
            Time.deltaTime * 5);
    }
}