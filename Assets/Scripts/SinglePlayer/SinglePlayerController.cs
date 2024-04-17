using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
public class SinglePlayerController : MonoBehaviour
{
    PlayerControl playerControl;
    Ball ball;

    float _vertical1;

    bool _p1Ready, _p2Ready;
    public float _p1Progress, _p2Progress;

    [SerializeField] float _speed;
    [SerializeField] float _minY, _maxY;
    [SerializeField] Transform[] _players;
    [SerializeField] Image _readyImageBlue, _readyImageRed;
    [SerializeField] GameObject _readyScreen, _ball;

    private void Awake()
    {
        ball = FindObjectOfType<Ball>();
        playerControl = new PlayerControl();
        _p1Progress = 0;
        _p2Progress = 0;
        _p1Ready = false;
        _p2Ready = false;
        _ball.SetActive(false);
    }
    private void OnEnable()
    {
        playerControl.Enable();

        playerControl.LocalMultiplayer.VerticalP1.performed += VerticalP1_performed;
        playerControl.LocalMultiplayer.VerticalP1.canceled += VerticalP1_canceled;

        ball.IncreaseDifficulty += IncreaseDifficultyPerformed;
    }

    private void IncreaseDifficultyPerformed(object sender, EventArgs e)
    {
        _speed = ball.GetSpeed();
    }

    private void OnDisable()
    {
        playerControl.Disable();

        playerControl.LocalMultiplayer.VerticalP1.performed -= VerticalP1_performed;

        ball.IncreaseDifficulty -= IncreaseDifficultyPerformed;
    }
    private void VerticalP1_performed(CallbackContext obj)
    {
        _vertical1 = obj.ReadValue<float>();
    }
    private void VerticalP1_canceled(CallbackContext obj)
    {
        _vertical1 = 0;
    }

    private void Update()
    {
        if (_p1Ready && _p2Ready)
        {
            _readyScreen.SetActive(false);
            _ball.SetActive(true);

            //Player 1
            _players[0].Translate(0, _vertical1 * _speed * Time.deltaTime, 0);
            _players[0].position = new Vector3(_players[0].position.x, Mathf.Clamp(_players[0].position.y, _minY, _maxY), _players[0].position.z);
            _players[1].position = new Vector3(_players[1].position.x, Mathf.Clamp(_players[1].position.y, _minY, _maxY), _players[1].position.z);
        }
        else
        {
            ReadyUp();
        }
    }
    void ReadyUp()
    {
        if (!_p1Ready)
        {
            if (_vertical1 > 0)
            {
                _p1Progress += _vertical1 * Time.deltaTime / 2;
            }
            else if (_p1Progress > 0)
            {
                _p1Progress -= Time.deltaTime / 2;
            }
            Mathf.Clamp01(_p1Progress);
        }
        if (!_p2Ready)
        {
            _p2Progress += Time.deltaTime / 2;
            Mathf.Clamp01(_p2Progress);
        }
        if (_p1Progress >= 1)
        {
            _p1Ready = true;
        }
        if (_p2Progress >= 1)
        {
            _p2Ready = true;
        }
        _readyImageBlue.fillAmount = _p1Progress;
        _readyImageRed.fillAmount = _p2Progress;
    }
}