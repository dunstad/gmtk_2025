using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [NonSerialized] public int currentHealth;
    public bool vulnerable;
    public UnityEvent onHurt;
    public UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        vulnerable = true;
    }

    public void Heal(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Hurt(int damage)
    {
        if (vulnerable)
        {
            currentHealth -= damage;
            onHurt.Invoke();
            if (currentHealth <= 0)
            {
                // Debug.Log("dead");
                onDeath.Invoke();
                // Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
