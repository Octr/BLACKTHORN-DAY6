using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float movementSpeed = 5f;
    public GameObject deathEffectPrefab;

    private Transform spaceship;

    private void Start()
    {
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").transform;
    }

    private void Update()
    {
        MoveTowardsSpaceship();
    }

    private void MoveTowardsSpaceship()
    {
        if (spaceship != null)
        {
            // Calculate the direction to the spaceship
            Vector3 directionToSpaceship = (spaceship.position - transform.position).normalized;

            // Move the enemy towards the spaceship
            transform.position += directionToSpaceship * movementSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spaceship"))
        {
            GameLogic.Instance.RetryLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Destroy the bullet on collision
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        AudioManager.Instance.Play2DSoundEffect(SoundEffect.sfx_zap, 1f, 0.8f, 1.2f);

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        EnemyTracker.Instance.RemoveEnemy(gameObject);
    }
}
