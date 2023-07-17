using System.Collections;
using System.Collections.Generic;
using Scripts.Interfaces;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int health;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
