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

    VideoPlayer videoPlayer;

    float canvasScale;

    PlayerControl inputActions;

    public int _levelIndex;


    private void Awake()
    {
        _loadingScreen.SetActive(false);
        _levelIndex = 0;
        videoPlayer = GetComponent<VideoPlayer>();
        inputActions = new PlayerControl();
    }
    public void OnEnable()
    {
        inputActions.Enable();
        inputActions.MainMenu.ChangeUp.performed += ChangeUp_performed;
        inputActions.MainMenu.ChangeDown.performed += ChangeDown_performed;

        canvasScale = GetComponentInParent<RectTransform>().lossyScale.x;
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
        if (_levelIndex < 2)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(_levelIndex + 1);
            StartCoroutine(LoadInBG(op));
        }
        else
        {
            FindObjectOfType<LaunchManager>().JoinRandomGame();
        }
    }

    IEnumerator LoadInBG(AsyncOperation op)
    {
        op.allowSceneActivation = false;
        while(videoPlayer.time / videoPlayer.length < .9f)
        {
            yield return null;
        }
        op.allowSceneActivation = true;
    }

    private void FixedUpdate()
    {
        _container.position = Vector3.Lerp(_container.position,
            new Vector3(_container.position.x, _positions[_levelIndex] * canvasScale, _container.position.z),
            Time.deltaTime * 5);
    }
}