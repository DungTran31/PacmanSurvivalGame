using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float meleeSpeed;
    [SerializeField] private float damage;

    float timeUntilMelee;

    private void Update()
    {
        if(timeUntilMelee <= 0f)
        {
            if(Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("Attack");
                timeUntilMelee = meleeSpeed;
            }
        } 
        else
        {
            timeUntilMelee -= Time.deltaTime;
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("Enemy hit");
        }
        if (collision.tag == "RangedEnemy")
        {
            collision.GetComponent<RangedEnemy>().TakeDamage(damage);
            Debug.Log("RangedEnemy hit");
        }
    }
}
