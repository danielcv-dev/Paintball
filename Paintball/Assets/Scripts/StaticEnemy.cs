using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Transform))]
public class StaticEnemy : MonoBehaviour
{

    [SerializeField] private Transform rotationEnemy;
    [SerializeField] private float speed;
    [SerializeField] private bool startRotation = false;


    private void Start()
    {
        rotationEnemy = GetComponent<Transform>();
    }

    private void Update()
    {
        if (startRotation)
        {
            transform.rotation = Quaternion.Slerp(rotationEnemy.rotation, Quaternion.Euler(0,0,0), Time.deltaTime * speed);
            
        }
        
    }

    protected virtual void InitStaticEnemy()
    {
        startRotation = true;
    }
}
