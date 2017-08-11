using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public float fullHealth;
    public GameObject dieEffect;
    private float currentHealth;
    Player play = null;

    void Start()
    {
        currentHealth = fullHealth;
        play = GetComponent<Player>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth.ToString());
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (dieEffect != null)
        {
            Instantiate(dieEffect, transform.position, transform.rotation);
            //Destroy(gameObject, 0.6f);
            LevelManager.Instance.Win();
        }

    }

}
