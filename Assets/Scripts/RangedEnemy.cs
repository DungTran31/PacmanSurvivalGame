using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] private float health;
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;

    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public float fireRate;
    private float timeToFire;
    public Transform firingPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeToFire = fireRate;
    }

    private void Update()
    {
        //Get the target
        if(!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }

        if(target != null &&Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(timeToFire <= 0f)
        {
            Debug.Log("Shoot");
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(target != null)
        {
            if(Vector2.Distance(target.position, transform.position) >= distanceToStop)
            {
                rb.velocity = transform.up * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }
    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            LevelManager.manager.GameOver();
            Destroy(collision.gameObject);
            target = null;
        } 
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            LevelManager.manager.IncreaseScore(3);
            Destroy(gameObject);
            Debug.Log("Enemy died");
        }
    }
}
