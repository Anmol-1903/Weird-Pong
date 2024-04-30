using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class LevelSelector : MonoBehaviour
{
    [SerializeField] Transform _container;
    [SerializeField] GameObject _loadingScreen;

    [SerializeField] float[] _positions;

    PlayerControl inputActions;

    VideoPlayer player;
    AsyncOperation async;

    public int _levelIndex;


    private void Awake()
    {
        _loadingScreen.SetActive(false);
        _levelIndex = 0;
        inputActions = new PlayerControl();
        player = _loadingScreen.GetComponent<VideoPlayer>();
    }
    public void OnEnable()
    {
        inputActions.Enable();
        inputActions.MainMenu.ChangeUp.performed += ChangeUp_performed;
        inputActions.MainMenu.ChangeDown.performed += ChangeDown_performed;
        player.loopPointReached += VideoEnded;
    }

    private void VideoEnded(VideoPlayer source)
    {
        async.allowSceneActivation = true;
        if(SceneManager.GetActiveScene().buildIndex == 3 ) 
        { 
            FindObjectOfType<LaunchManager>().JoinRandomGame();
        }  
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.MainMenu.ChangeUp.performed -= ChangeUp_performed;
        inputActions.MainMenu.ChangeDown.performed -= ChangeDown_performed;
    }

    private void ChangeDown_performed(InputAction.CallbackContext context)
    {
        NextLevel();
    }

    private void ChangeUp_performed(InputAction.CallbackContext obj)
    {
        PreviousLevel();
    }
    public void NextLevel()
    {
        _levelIndex = (_levelIndex + 1) % 3;
    }
    public void PreviousLevel()
    {
        _levelIndex--;
        if (_levelIndex < 0)
            _levelIndex = 2;
    }
    public void PlayGame()
    {
            _loadingScreen.SetActive(true);
            async = SceneManager.LoadSceneAsync(_levelIndex + 1);
            async.allowSceneActivation = false;
    }
    private void FixedUpdate()
    {
        _container.position = Vector3.Lerp(_container.position,
            new Vector3(_container.position.x, _positions[_levelIndex], _container.position.z),
            Time.deltaTime * 5);
    }
}