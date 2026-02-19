using UnityEngine;
using System.Collections;

public enum PowerupType
{
    Simple,
    Smash
}

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
    [SerializeField] private float smashJumpForce = 12f;
    [SerializeField] private float smashDownForce = 25f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;

    private bool hasPowerup;
    private bool hasSmash;
    private bool isSmashing;
    private bool isGrounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameManager.Instance.isGameRunning || GameManager.Instance.isPaused)
            return;

        Move();
        UpdateIndicator();

        // Smash trigger (Space)
        if (hasSmash && isGrounded && !isSmashing && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SmashRoutine());
        }
    }

    void Move()
    {
        float v = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.forward * v * moveForce * Time.deltaTime);
    }

    void UpdateIndicator()
    {
        if (powerupIndicator)
            powerupIndicator.transform.position = transform.position + Vector3.down * 0.6f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            ActivatePowerup(false, other.gameObject);
            GameManager.Instance.gamePlayUI.ActivePowerup(PowerupType.Simple);
        }
        else if (other.CompareTag("SmashPowerup"))
        {
            ActivatePowerup(true, other.gameObject);
            GameManager.Instance.gamePlayUI.ActivePowerup(PowerupType.Smash);
        }
    }

    void ActivatePowerup(bool smash, GameObject obj)
    {
        Destroy(obj);

        hasPowerup = !smash;
        hasSmash = smash;

        if (powerupIndicator) powerupIndicator.SetActive(true);

        StartCoroutine(PowerupTimer());
    }

    IEnumerator PowerupTimer()
    {
        float timer = powerupDuration;

        while (timer > 0)
        {
            if (hasPowerup)
                GameManager.Instance.gamePlayUI.UpdateSimplePowerup(timer / powerupDuration);

            if (hasSmash)
                GameManager.Instance.gamePlayUI.UpdateSmashPowerup(timer / powerupDuration);

            timer -= Time.deltaTime;
            yield return null;
        }

        hasPowerup = false;
        hasSmash = false;

        GameManager.Instance.gamePlayUI.HidePowerup();
        GameManager.Instance.gamePlayUI.HideSmashPowerup();

        if (powerupIndicator) powerupIndicator.SetActive(false);
    }

    void OnCollisionEnter(Collision col)
    {
        // Ground detection
        if (((1 << col.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;

            // Landing after smash
            if (isSmashing)
            {
                ApplySmash();
                isSmashing = false;
            }
        }

        if (!col.gameObject.CompareTag("Enemy")) return;

        // Normal / powerup hit
        if (!hasSmash)
        {
            Rigidbody enemyRb = col.rigidbody;
            Vector3 dir = (col.transform.position - transform.position).normalized;

            float force = hasPowerup ? powerupStrength : normalStrength;
            enemyRb.AddForce(dir * force, ForceMode.Impulse);
        }
    }

    IEnumerator SmashRoutine()
    {
        isSmashing = true;
        isGrounded = false;

        // Jump up
        rb.AddForce(Vector3.up * smashJumpForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.25f);

        // Slam down
        rb.AddForce(Vector3.down * smashDownForce, ForceMode.Impulse);
    }

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
