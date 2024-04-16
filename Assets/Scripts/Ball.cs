using UnityEngine;
public class Ball : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        randomDirection.z = 0;
        rb.velocity = randomDirection * speed;
        rb.angularVelocity = Random.onUnitSphere.normalized * speed * Mathf.Deg2Rad;
    }
    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
        Mathf.Clamp(speed, 1, 50);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            speed += 1f;
        }
    }
}