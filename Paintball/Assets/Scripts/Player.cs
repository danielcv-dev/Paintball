using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private Transform head;

    
    public void TakeDamage(float damage)
    {
        health -= damage;
        //print("Players health: "+ health);
    }

    public Vector3 GetHeadPosition()
    {
        return head.position;
    }
    
}
