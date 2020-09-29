using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.collider.GetComponentInParent<Player>();
        if (player)
        {
            player.TakeDamage(1);
        }
        Destroy(this.gameObject);
    }


}
