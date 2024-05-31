using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Header("General Settings")]
    [SerializeField] private float bulletAliveTime;
    [SerializeField] private bool isPlayer;

    [Header("Spread Settings")]
    [SerializeField] private int bulletsPerShot = 1;
    [SerializeField] private int spreadAngle;
    [SerializeField] private float inaccuracyAngle;

    [Header("Burst Settings")]
    [SerializeField] private int shotsPerClick = 1;
    [SerializeField] private float timeBetweenShots;

    [Header("Reload Settings")]
    [SerializeField] private int ammoPerReload;
    [SerializeField] private float reloadTime;
    
    public int ammoLeft;
    public bool isReloading = false;
    public bool canShoot = true;

    private void Awake()
    {
        ammoLeft = ammoPerReload;
    }

    public void Shoot()
    {
        if (ammoLeft == 0 && !isReloading)
        {
            isReloading = true;
            Invoke(nameof(FinishReloading), reloadTime);
        }
        if (canShoot)
            StartCoroutine(ShootRoutine());
    }

    private void FinishReloading()
    {
        isReloading = false;
        canShoot = true;
        ammoLeft = ammoPerReload;
    }

    private IEnumerator ShootRoutine()
    {
        canShoot = false;

        for (int i = 0; i < shotsPerClick; i++)
        {
            for (int j = 0; j < bulletsPerShot; j++)
            {
                float angle = spreadAngle * (j - (bulletsPerShot - 1) / 2.0f) / bulletsPerShot;
                Quaternion rot = Quaternion.Euler(0, transform.rotation.eulerAngles.y + angle + Random.Range(-inaccuracyAngle, inaccuracyAngle), 0);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
                bullet.GetComponent<Bullet>().SetAliveTime(bulletAliveTime);
                bullet.GetComponent<DamageSourceTrigger>().SetIsPlayer(isPlayer);
            }

            ammoLeft--;

            if (ammoLeft == 0)
                break;

            yield return new WaitForSeconds(timeBetweenShots);
        }

        if (ammoLeft > 0)
            canShoot = true;
    }
}