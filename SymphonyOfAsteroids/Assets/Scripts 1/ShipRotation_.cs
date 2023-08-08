using UnityEngine;

public class ShipRotation_ : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private Quaternion targetRotation;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetRotation = Quaternion.Euler(0f, 0f, 40);
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            targetRotation = Quaternion.Euler(0f, 0f, 90f);
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            targetRotation = Quaternion.Euler(0f, 0f, -220);
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            targetRotation = Quaternion.Euler(0f, 0f, -40f);
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            targetRotation = Quaternion.Euler(0f, 0f, -90f);
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            targetRotation = Quaternion.Euler(0f, 0f, 220f);
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        target.rotation = targetRotation;
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            Vector3 direction = (target.rotation * Vector3.up).normalized;

            BulletController bulletController = newBullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetDirection(direction);
            }
        }
    }
}
