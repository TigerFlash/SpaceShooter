using UnityEngine;

public class DuoGun : Weapon
{
    [SerializeField]
    private Transform[] firePoint;
    protected override void Shoot()
    {
       GameObject bullet1 = Instantiate(bulletPrefab, firePoint[0].position, Quaternion.Euler(0, 0, 0));
        bullet1.GetComponent<Bullet>().playerController = playerController;
       GameObject bullet2 = Instantiate(bulletPrefab, firePoint[1].position, Quaternion.Euler(0, 0, 0));
        bullet2.GetComponent<Bullet>().playerController = playerController;
    }
}
