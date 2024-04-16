using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
public class PlayerController : MonoBehaviour
{
    PlayerControl playerControl;

    float _vertical;
    
    [SerializeField] float _speed;
    [SerializeField] float _minY, _maxY;

    private void Awake()
    {
        playerControl = new PlayerControl();
    }
    private void OnEnable()
    {
        playerControl.Enable();
        playerControl.Game.Vertical.performed += Vertical_performed;
        playerControl.Game.Vertical.canceled += Vertical_canceled; ;
    }

    private void Vertical_canceled(CallbackContext obj)
    {
        _vertical = 0;
    }

    private void OnDisable()
    {
        playerControl.Disable();
        playerControl.Game.Vertical.performed -= Vertical_performed;
    }

    private void Vertical_performed(CallbackContext obj)
    {
        _vertical = obj.ReadValue<float>();
    }
    private void Update()
    {
        transform.Translate(0, _vertical * _speed * Time.deltaTime, 0);
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y, _minY, _maxY) , transform.position.z);
    }
}