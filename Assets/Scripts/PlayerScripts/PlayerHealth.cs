using System;
using Scripts.Interfaces;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerHealth : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private int playerHealth;

        private void Update()
        {
            if (playerHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(int damage)
        {
            playerHealth -= damage;
        }
    }
}