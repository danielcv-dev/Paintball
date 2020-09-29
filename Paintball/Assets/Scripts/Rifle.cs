using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Rifle : Weapon
{

    [SerializeField] private float fireRate;
    [SerializeField] private Projectile bulletPrefab;

    [SerializeField] private WaitForSeconds wait;


    private void Start()
    {
        wait = new WaitForSeconds(1 / fireRate);
    }

    protected override void StartShooting(XRBaseInteractor interactor)
    {
        base.StartShooting(interactor);
        StartCoroutine(ShootingCO());
    }

    private IEnumerator ShootingCO()
    {
        while (true)
        {
            Shoot();
            yield return wait;
        }
    }

    protected override void Shoot()
    {
        base.Shoot();
        Projectile projectileInstance = Instantiate(bulletPrefab, bulletSpawns[0].position, bulletSpawns[0].rotation);
        projectileInstance.Init(this);
        projectileInstance.Launch();
    }

    protected override void StopShooting(XRBaseInteractor interactor)
    {
        base.StopShooting(interactor);
        StopAllCoroutines();
    }

}
