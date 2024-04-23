using System;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] Transform _ball;
    [SerializeField] float _speed = 5f;

    Ball ball;
    private void Awake()
    {
        ball = _ball.GetComponent<Ball>();
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
            float step = _speed * Time.deltaTime;
            if (velocity.x > 0)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetPosition.y, transform.position.z), step);
        }
    }
}