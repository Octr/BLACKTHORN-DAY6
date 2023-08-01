using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float selfDestructTimer = 2f;
    public ParticleSystem destroyParticlePrefab;
    public LayerMask allowedCollisionLayers;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;
        rb.gravityScale = 0f;
        StartCoroutine(SelfDestruct());
        AudioManager.Instance.Play2DSoundEffect(SoundEffect.sfx_laser2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((allowedCollisionLayers.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (destroyParticlePrefab != null)
            {
                Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructTimer);

        if (destroyParticlePrefab != null)
        {
            Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
