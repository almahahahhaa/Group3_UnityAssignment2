using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public enum EnemyType { Aggressive, Defensive, Evasive }

    [Header("Settings")]
    [SerializeField] private float baseSpeed = 8f;
    [SerializeField] private float waveSpeedMultiplier = 0.5f;

    [Header("Debug Colors")]
    [SerializeField] private Color aggressiveColor = Color.red;
    [SerializeField] private Color defensiveColor = Color.blue;
    [SerializeField] private Color evasiveColor = Color.yellow;

    private Rigidbody rb;
    private Transform playerGoal;
    private Transform player;
    private SpawnManagerX spawnManager;

    private EnemyType type;
    private float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Initializes the enemy with its type and speed based on the current wave
    public void Initialize(int wave, EnemyType enemyType, SpawnManagerX manager)
    {
        spawnManager = manager;
        type = enemyType;
        speed = baseSpeed + wave * waveSpeedMultiplier;

        ApplyColor();
    }

    void FixedUpdate()
    {
        Vector3 dir = GetMovementDirection();
        rb.AddForce(dir * speed, ForceMode.Force);
    }
    // Determines the movement direction based on the enemy type
    Vector3 GetMovementDirection()
    {
        Vector3 goalDir = (playerGoal.position - transform.position).normalized;

        switch (type)
        {
            case EnemyType.Aggressive:
                return goalDir;

            case EnemyType.Defensive:
                Vector3 away = (transform.position - player.position).normalized;
                return (goalDir + away).normalized;

            case EnemyType.Evasive:
                Vector3 side = Vector3.Cross(goalDir, Vector3.up);
                return (goalDir + side * Mathf.Sin(Time.time * 3f)).normalized;
        }

        return goalDir;
    }
    // Applies the appropriate color to the enemy based on its type for visual debugging
    void ApplyColor()
    {
        Renderer r = GetComponent<Renderer>();

        if (type == EnemyType.Aggressive) r.material.color = aggressiveColor;
        if (type == EnemyType.Defensive) r.material.color = defensiveColor;
        if (type == EnemyType.Evasive) r.material.color = evasiveColor;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Contains("Goal"))
        {
            spawnManager?.OnEnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
