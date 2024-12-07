using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ball : MonoBehaviour
{
    [SerializeField] MeshRenderer outline;
    [SerializeField] AudioClip[] clips;

    TrailRenderer trail;

    float speed = 5f;
    float collisions = 0;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }
    void Start()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        randomDirection.z = 0;
        rb.velocity = randomDirection * speed;
        rb.angularVelocity = Random.onUnitSphere.normalized * speed * 100 * Mathf.Deg2Rad;
    }
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
            if (!PhotonNetwork.IsMasterClient)
                return;
        rb.velocity = rb.velocity.normalized * (speed + collisions);
        Mathf.Clamp(speed + collisions, 1, 50);
    }
    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlaySFX(clips[Random.Range(0, clips.Length)]);

        if (collision.gameObject.CompareTag("Paddle"))
        {
            collisions += 0.5f;
            Player player = collision.gameObject.GetComponentInParent<Player>();
            outline.material = player.GetTrailMaterial();
            trail.material = player.GetTrailMaterial();
        }
    }
    public float GetSpeed()
    {
        return speed + (collisions / 2.5f);
    }
    public void ResetBall()
    {
        trail.enabled = false;
        transform.position = Vector3.zero;
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        randomDirection.z = 0;
        collisions = 0;
        rb.velocity = randomDirection * speed;
        rb.angularVelocity = Random.onUnitSphere.normalized * speed * 100 * Mathf.Deg2Rad;
        trail.enabled = true;
    }
}