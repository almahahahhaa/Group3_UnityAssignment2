using UnityEngine;
using System.Collections;

public class PlayerControllerX : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveForce = 500f;
    [SerializeField] private Transform focalPoint;

    [Header("Powerups")]
    [SerializeField] private float powerupDuration = 5f;
    [SerializeField] private GameObject powerupIndicator;

    [Header("Hit Strength")]
    [SerializeField] private float normalStrength = 10f;
    [SerializeField] private float powerupStrength = 25f;

    [Header("Smash")]
    [SerializeField] private float smashForce = 40f;
    [SerializeField] private float smashRadius = 5f;

    private Rigidbody rb;
    private bool hasPowerup;
    private bool hasSmash;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!GameManager.Instance.isGameRunning || GameManager.Instance.isPaused) { return; }
        Move();
        UpdateIndicator();
    }
    // Applies a forward force based on player input and the focal point's forward direction
    void Move()
    {
        float v = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.forward * v * moveForce * Time.deltaTime);
    }
    // Updates the position of the powerup indicator to be below the player
    void UpdateIndicator()
    {
        if (powerupIndicator)
            powerupIndicator.transform.position = transform.position + Vector3.down * 0.6f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
            ActivatePowerup(false, other.gameObject);

        if (other.CompareTag("SmashPowerup"))
            ActivatePowerup(true, other.gameObject);
    }
    // Activates the appropriate powerup based on the type and starts the timer
    void ActivatePowerup(bool smash, GameObject obj)
    {
        Destroy(obj);

        hasPowerup = !smash;
        hasSmash = smash;

        if (powerupIndicator) powerupIndicator.SetActive(true);

        StartCoroutine(PowerupTimer());
    }
    // Waits for the powerup duration to expire and then resets the powerup states
    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(powerupDuration);

        hasPowerup = false;
        hasSmash = false;

        if (powerupIndicator) powerupIndicator.SetActive(false);
    }

    void OnCollisionEnter(Collision col)
    {
        if (!col.gameObject.CompareTag("Enemy")) return;
        // If the smash powerup is active, apply the smash effect instead of a normal hit
        if (hasSmash)
        {
            ApplySmash();
            return;
        }

        Rigidbody enemyRb = col.rigidbody;
        Vector3 dir = (col.transform.position - transform.position).normalized;

        float force = hasPowerup ? powerupStrength : normalStrength;
        enemyRb.AddForce(dir * force, ForceMode.Impulse);
    }
    // Applies an explosive force to all enemies within the smash radius, with strength based on distance
    void ApplySmash()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, smashRadius);

        foreach (var h in hits)
        {
            if (!h.CompareTag("Enemy")) continue;

            Rigidbody e = h.attachedRigidbody;
            Vector3 dir = h.transform.position - transform.position;
            float dist = dir.magnitude;

            float factor = 1f - dist / smashRadius;
            e.AddForce(dir.normalized * smashForce * factor, ForceMode.Impulse);
        }
    }
}
