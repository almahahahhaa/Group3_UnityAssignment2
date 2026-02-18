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

        // If have powerup, count down powerup duration
        if (hasSmashPowerup && Input.GetKeyDown(KeyCode.Space) && !isSmashing)
        {
            StartCoroutine(SmashAttack());
        }

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
        }
        // If Player collides with smash powerup, activate smash powerup
        if (other.gameObject.CompareTag("SmashPowerup"))
        {
            Destroy(other.gameObject);
            hasSmashPowerup = true;
        }

    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  transform.position - other.gameObject.transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }

    IEnumerator SmashAttack()
    {
        isSmashing = true;

        // Jump up
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.4f); // airtime

        // Slam down
        playerRb.AddForce(Vector3.down * jumpForce * 2, ForceMode.Impulse);

        yield return new WaitForSeconds(0.2f);

        // Detect nearby enemies
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

        hasSmashPowerup = false;
        isSmashing = false;
    }


}
