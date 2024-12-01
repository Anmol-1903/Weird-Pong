using System;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] Transform _ball;
    [SerializeField] float _speed = 5f;
    [SerializeField] float _minY, _maxY;

    Ball ball;
    private void OnEnable()
    {
        ball = FindObjectOfType<Ball>();
        _ball = ball.transform;
    }

    private void IncreaseDifficultyPerformed(object sender, EventArgs e)
    {
        //_speed = ball.GetSpeed();
    }

    private void Update()
    {
        if (_ball != null && _ball.gameObject.activeInHierarchy)
        {
            float timeToReach = (transform.position - _ball.position).magnitude / _ball.GetComponent<Rigidbody>().velocity.magnitude;
            Vector3 velocity = _ball.GetComponent<Rigidbody>().velocity;
            Vector3 targetPosition = _ball.position + velocity * timeToReach;

            targetPosition.y = Mathf.Clamp(targetPosition.y, _minY, _maxY);

            float step = _speed * Time.deltaTime;
            if ((transform.position.x > 0 && velocity.x > 0) || (transform.position.x < 0 && velocity.x < 0))
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetPosition.y, transform.position.z), step);
        }
    }
}