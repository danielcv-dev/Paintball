using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistol : Weapon {

    public Projectile bulletPrefab;

    protected override void StartShooting(XRBaseInteractor interactor)
    {
        base.StartShooting(interactor);
        Shoot();
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
    }
}
