using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    // Smash powerup variables
    [Header("Smash Powerup")]
    public bool hasSmashPowerup;
    public float smashForce = 40f;
    public float smashRadius = 5f;
    public float jumpForce = 15f;

    private bool isSmashing;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown()); // start powerup countdown
        }
        // If Player collides with smash powerup, activate smash powerup
        if (other.gameObject.CompareTag("SmashPowerup"))
        {
            Destroy(other.gameObject);
            hasSmashPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(SmashCooldown()); // start smash powerup countdown
        }

    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // Coroutine to perform smash attack
    IEnumerator SmashCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasSmashPowerup = false;
        powerupIndicator.SetActive(false);
    }
    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.transform.position - transform.position;

            // Smash powerup has highest priority
            if (hasSmashPowerup)
            {
                ApplySmashEffect();
            }
            else if (hasPowerup)
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }


    void ApplySmashEffect()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, smashRadius);

        foreach (Collider col in hitEnemies)
        {
            if (col.CompareTag("Enemy"))
            {
                Rigidbody enemyRb = col.GetComponent<Rigidbody>();

                Vector3 dir = col.transform.position - transform.position;
                float distance = dir.magnitude;

                float forceMultiplier = 1 - (distance / smashRadius);
                float finalForce = smashForce * forceMultiplier;

                enemyRb.AddForce(dir.normalized * finalForce, ForceMode.Impulse);
            }
        }
    }

}
