using System;
using PlayerScripts;
using UnityEngine;

namespace EnemyScripts
{
    
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] private int damage;
   
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
