using System;
using UnityEngine;
public class Ball : MonoBehaviour
{
    [SerializeField] GameObject[] player1Meshes;
    [SerializeField] GameObject[] player2Meshes;

    [SerializeField] MeshRenderer outline;
    
    public event EventHandler IncreaseDifficulty;

    TrailRenderer trail;

    int player1Score = 0;
    int player2Score = 0;

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
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized;
        randomDirection.z = 0;
        rb.velocity = randomDirection * speed;
        rb.angularVelocity = UnityEngine.Random.onUnitSphere.normalized * speed * 100 * Mathf.Deg2Rad;
        ChangePlayerMesh(player1Meshes, 0);
        ChangePlayerMesh(player2Meshes, 0);
    }
    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * (speed + collisions);
        Mathf.Clamp(speed + collisions, 1, 50);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1BackWall"))
        {
            player2Score++;
            ChangePlayerMesh(player2Meshes, player2Score);
            ResetBall();
        }
        else if (other.gameObject.CompareTag("Player2BackWall"))
        {
            player1Score++;
            ChangePlayerMesh(player1Meshes, player1Score);
            ResetBall();
        }
    }
    void ChangePlayerMesh(GameObject[] meshes, int score)
    {
        for(int i = 0; i < meshes.Length; i++)
        {
            meshes[i].SetActive(false);
        }
        meshes[score].SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            collisions += 0.5f;
            Player player = collision.gameObject.GetComponentInParent<Player>();
            outline.material = player.GetMaterial();
            trail.material = player.GetTrailMaterial();
            IncreaseDifficulty?.Invoke(collision.gameObject, EventArgs.Empty);
        }
    }
    public float GetSpeed()
    {
        return speed + (collisions/2.5f);
    }
    void ResetBall()
    {
        transform.position = Vector3.zero;
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere.normalized;
        randomDirection.z = 0;
        collisions = 0;
        rb.velocity = randomDirection * speed;
        rb.angularVelocity = UnityEngine.Random.onUnitSphere.normalized * speed * 100 * Mathf.Deg2Rad;
        IncreaseDifficulty?.Invoke(gameObject, EventArgs.Empty);
    }
}