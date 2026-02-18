using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;
    [SerializeField] private GameObject smashPowerupPrefab;

    [Header("Spawn Area")]
    [SerializeField] private float spawnRangeX = 10f;
    [SerializeField] private float spawnZMin = 15f;
    [SerializeField] private float spawnZMax = 25f;
    [SerializeField] private Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // Offset to spawn powerups closer to the player

    [Header("Wave Settings")]
    [SerializeField] private int startingEnemies = 2;

    [Header("References")]
    [SerializeField] private GameObject player;

    private int waveCount = 1;
    private int aliveEnemies;

    void Update()
    {
        if (aliveEnemies == 0)
        {
            SpawnWave();
        }
    }
    // Spawns a new wave of enemies and a powerup
    void SpawnWave()
    {
        SpawnPowerup();
        SpawnEnemies(waveCount + startingEnemies - 1);
        GameManager.Instance.NextWave(waveCount);
        waveCount++;
        ResetPlayer();
    }
    // Spawns a powerup at a random position within the spawn area
    void SpawnPowerup()
    {
        Vector3 pos = GenerateSpawnPosition() + powerupSpawnOffset;
        GameObject prefab = Random.value > 0.5f ? smashPowerupPrefab : powerupPrefab;
        Instantiate(prefab, pos, prefab.transform.rotation);
    }
    // Spawns a specified number of enemies with random types
    void SpawnEnemies(int count)
    {
        aliveEnemies = count;

        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);

            EnemyX enemyScript = enemy.GetComponent<EnemyX>();
            EnemyX.EnemyType type = (EnemyX.EnemyType)Random.Range(0, 3);

            enemyScript.Initialize(waveCount, type, this);
        }
    }

    public void OnEnemyDestroyed()
    {
        aliveEnemies--;
    }

    Vector3 GenerateSpawnPosition()
    {
        float x = Random.Range(-spawnRangeX, spawnRangeX);
        float z = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(x, 0, z);
    }

    void ResetPlayer()
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        player.transform.position = new Vector3(0, 1, -7);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
