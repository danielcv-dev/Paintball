using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PhysicsProjectile : Projectile
{

    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject[] splatDecalsPrefab;
    [SerializeField] private AudioClip[] clips;
    private Rigidbody rigidBody;
    
    

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Body") && !collision.collider.CompareTag("Head"))
        {
            Paint(collision);
        }
        else
        {
            GiveDamage(collision);
        }
        
        
        if (!collision.collider.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
        //var localVelocity = transform.InverseTransformDirection(rigidBody.velocity);
        //var forwardSpeed = localVelocity.z * 100 / 3.6 * Time.deltaTime;
        //print(forwardSpeed);
    }

    public override void Init(Weapon weapon)
    {
        base.Init(weapon);
        Destroy(gameObject, lifeTime);
        
    }

    public override void Launch()
    {
        base.Launch();
        rigidBody.AddRelativeForce(Vector3.forward * weapon.GetShootingForce(), ForceMode.Impulse);
        
    }

    public void Paint(Collision impact)
    {
        if (impact.collider.CompareTag("Car"))
        {
            AudioSource.PlayClipAtPoint(clips[0], impact.contacts[0].point);
        }
        else if (impact.collider.CompareTag("Person"))
        {
            AudioSource.PlayClipAtPoint(clips[1], impact.contacts[0].point);
        }
        else
        {
            AudioSource.PlayClipAtPoint(clips[2], impact.contacts[0].point);
        }

        Instantiate(splatDecalsPrefab[Random.Range(0, splatDecalsPrefab.Length)], impact.contacts[0].point, Quaternion.LookRotation(impact.contacts[0].normal, Vector3.down));

    }

    public void GiveDamage(Collision impact)
    {

        
        EnemyIA enemy = impact.collider.GetComponentInParent<EnemyIA>();
        if (enemy)
        {
            if (impact.collider.CompareTag("Body"))
            {
                enemy.ITakeDamage(1);
            }
            else if (impact.collider.CompareTag("Head"))
            {
                enemy.ITakeDamage(2);
            }
        }
    }

}
