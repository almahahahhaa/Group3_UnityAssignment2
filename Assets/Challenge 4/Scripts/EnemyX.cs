using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public enum EnemyType
    {
        Aggressive,
        Defensive,
        Evasive
    }

    public EnemyType enemyType;

    public float baseSpeed = 8f;
    private float speed;

    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private GameObject player;

    private int waveLevel = 1;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        if (playerGoal == null)
        {
            playerGoal = GameObject.Find("Player Goal");
        }

        player = GameObject.FindGameObjectWithTag("Player");

        // Speed increases per wave
        speed = baseSpeed + (waveLevel * 0.5f);

        // Set color based on enemy type
        Renderer r = GetComponent<Renderer>();

        if (enemyType == EnemyType.Aggressive) r.material.color = Color.red;
        if (enemyType == EnemyType.Defensive) r.material.color = Color.blue;
        if (enemyType == EnemyType.Evasive) r.material.color = Color.yellow;

    }

    void Update()
    {
        Vector3 moveDir = Vector3.zero;

        switch (enemyType)
        {
            case EnemyType.Aggressive:
                // Direct rush to goal
                moveDir = (playerGoal.transform.position - transform.position).normalized;
                break;

            case EnemyType.Defensive:
                // Move toward goal but avoid player
                Vector3 toGoal = (playerGoal.transform.position - transform.position).normalized;
                Vector3 awayFromPlayer = (transform.position - player.transform.position).normalized;
                moveDir = (toGoal + awayFromPlayer).normalized;
                break;

            case EnemyType.Evasive:
                // Zig-zag movement
                Vector3 goalDir = (playerGoal.transform.position - transform.position).normalized;
                Vector3 side = Vector3.Cross(goalDir, Vector3.up);
                moveDir = (goalDir + side * Mathf.Sin(Time.time * 3f)).normalized;
                break;
        }

        enemyRb.AddForce(moveDir * speed * Time.deltaTime);
    }

    public void Initialize(int wave, EnemyType type)
    {
        waveLevel = wave;
        enemyType = type;

        speed = baseSpeed + (waveLevel * 0.5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Enemy Goal" ||
            other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }

}
