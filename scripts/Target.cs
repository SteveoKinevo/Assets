using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public float health = 50f;
    public float damageToPlayer = 5f;
    public float playerDamage = 10f;

    public GameObject dieEffect;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (dieEffect != null)
        {
            Instantiate(dieEffect, transform.position, transform.rotation);
            Destroy(gameObject,0.6f);
        }

        
    }


}
