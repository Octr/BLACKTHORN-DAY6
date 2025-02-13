using UnityEngine;

public class BulletShooter : Singleton<BulletShooter>
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float shootingCooldown = 0.5f;
    public bool isUnlocked = false;

    private float shootingTimer = 0f;


    private void Update()
    {
        if ((!isUnlocked))
        {
            return;
        }

        // Check if the shooting cooldown is over
        if (shootingTimer <= 0f)
        {
            // Check for shooting input (left mouse button or any other input you prefer)
            if (Input.GetButton("Fire2"))
            {
                // Shoot the bullet
                Shoot();
                // Reset the shooting timer
                shootingTimer = shootingCooldown;
            }
        }
        else
        {
            // Reduce the shooting timer
            shootingTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        // Create a new bullet at the bullet spawn point's position and rotation
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }

        // Play the shooting sound effect
        AudioManager.Instance.Play2DSoundEffect(SoundEffect.sfx_laser2, 1f, 0.8f, 1.2f);
    }
}
