using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyIA : MonoBehaviour
{
    const string RUN_TRIGGER = "Run";
    const string CROUCH_TRIGGER = "Crouch";
    const string SHOOT_TRIGGER = "Shoot";

    [SerializeField] private int startingHealth;
    [SerializeField] private float minTimeUnderCover;
    [SerializeField] private float maxTimeUnderCover;
    [SerializeField] private int minShootsToTake;
    [SerializeField] private int maxShootsToTake;
    [SerializeField] private float rotationSpeed;
    [Range(0, 100)]
    [SerializeField] private float shootingAccuracy;
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private GameObject bulletPrefab;

    private bool isShooting;
    private int currentShotsTaken;
    private int currentMaxShootsToTake;
    private NavMeshAgent agent;
    private Player playerPos;
    private Transform occupiedCoverSpot;
    private Animator animator;

    private int health;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.SetTrigger(RUN_TRIGGER);
    }

    public void Init(Player _playerPos, Transform _coverSpot)
    {
        health = startingHealth;
        occupiedCoverSpot = _coverSpot;
        playerPos = _playerPos;
        GetCover();
    }

    private void GetCover()
    {
        agent.isStopped = false;
        agent.SetDestination(occupiedCoverSpot.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isStopped == false && (transform.position - occupiedCoverSpot.position).sqrMagnitude <= 0.1f)
        {
            agent.isStopped = true;
            StartCoroutine(InitializeShootingCO());
        }
        if (isShooting)
        {
            RotateTowardsPlayer();
        }
    }

    private IEnumerator InitializeShootingCO()
    {
        HideBehindCover();
        yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeUnderCover, maxTimeUnderCover));
        StartShooting();
    }

    private void HideBehindCover()
    {
        animator.SetTrigger(CROUCH_TRIGGER);
    }

    private void StartShooting()
    {
        isShooting = true;
        currentMaxShootsToTake = UnityEngine.Random.Range(minShootsToTake, maxShootsToTake);
        currentShotsTaken = 0;
        animator.SetTrigger(SHOOT_TRIGGER);
    }

    public void Shoot()
    {


        bool hitPlayer = UnityEngine.Random.Range(0 , 100) < shootingAccuracy;
        if (hitPlayer)
        {
            
            Vector3 direction = playerPos.GetHeadPosition() - shootingPosition.position;

            //If you want a raycast instead the bullet

            //RaycastHit hit;
            //if (Physics.Raycast(shootingPosition.position, direction, out hit))
            //{
            //    Debug.DrawRay(shootingPosition.position, direction, Color.green);
            //    Player player = hit.collider.GetComponentInParent<Player>();
            //    if (player)
            //    {
            //        player.TakeDamage(1);
            //    }
            //}



            shootingPosition.rotation = Quaternion.LookRotation(direction);
            GameObject bullet = Instantiate(bulletPrefab, shootingPosition.position, shootingPosition.rotation);
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 0.5f, ForceMode.Impulse);


        }
        currentShotsTaken++;
        if (currentShotsTaken >= currentMaxShootsToTake)
        {
            StartCoroutine(InitializeShootingCO());
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = playerPos.GetHeadPosition() - transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }

    public void ITakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
